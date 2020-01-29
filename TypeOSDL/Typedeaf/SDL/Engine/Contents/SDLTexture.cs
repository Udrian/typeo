using SDL2;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.SDL.Engine.Contents;
using TypeOEngine.Typedeaf.SDL.Engine.Graphics;
using SDL_Image = System.IntPtr;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Contents
    {
        public class SDLTexture : Texture
        {
            public SDL_Image SDL_Image { get; set; }

            public SDLTexture() : base() { }

            public override void Load(string path, ContentLoader contentLoader)
            {
                var sdlCanvas = contentLoader.Canvas as SDLCanvas;
                FilePath = path;

                SDL_Image = SDL_image.IMG_LoadTexture(sdlCanvas.SDLRenderer, FilePath);
                SDL2.SDL.SDL_QueryTexture(SDL_Image, out _, out _, out int w, out int h);
                Size = new Vec2(w, h);
                //TODO: Error handling
                /*if (tex == null)
                {
                    Console.WriteLine("IMG_LoadTexture Error: " + SDL.SDL_GetError());
                    return (SDL_Texture)0;
                }

                return tex;*/
            }
        }
    }

    namespace Engine.Graphics
    {
        partial class SDLCanvas
        {
            public override void DrawImage(Texture texture, Vec2 pos)
            {
                DrawImage(texture, pos, null);
            }

            public override void DrawImage(Texture texture, Vec2 pos, Vec2 scale = null, double rotation = 0, Vec2 origin = null, Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null)
            {
                InternalDrawImage(texture, pos, scale ?? new Vec2(1), rotation, origin ?? new Vec2(0), color, flipped, source);

            }

            private void InternalDrawImage(Texture texture, Vec2 pos, Vec2 scale, double rotation, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source)
            {
                const double degreeToRadianConst = 57.2957795131;

                var sdltexture = texture as SDLTexture;
                //TODO: Error handling

                var drect = new SDL2.SDL.SDL_Rect
                {
                    x = (int)(pos.X - origin.X),
                    y = (int)(pos.Y - origin.Y),
                    w = (int)(texture.Size.X * scale.X),
                    h = (int)(texture.Size.Y * scale.Y)
                };

                var sdlPoint = new SDL2.SDL.SDL_Point
                {
                    x = (int)origin.X,
                    y = (int)origin.Y
                };

                if (color == null)
                    color = Color.White;
                SDL2.SDL.SDL_SetTextureColorMod(sdltexture.SDL_Image, (byte)color.R, (byte)color.G, (byte)color.B);
                SDL2.SDL.SDL_SetTextureAlphaMod(sdltexture.SDL_Image, (byte)color.A);

                var sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                if (flipped == Texture.Flipped.Horizontal)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                else if (flipped == Texture.Flipped.Vertical)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
                else if (flipped == Texture.Flipped.Both)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL2.SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;

                if(source == null)
                    SDL2.SDL.SDL_RenderCopyEx(this.SDLRenderer, sdltexture.SDL_Image, (SDL_Image)null, ref drect, rotation * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                else
                {
                    SDL2.SDL.SDL_Rect srect = new SDL2.SDL.SDL_Rect
                    {
                        x = (int)source.Pos.X,
                        y = (int)source.Pos.Y,
                        w = (int)source.Size.X,
                        h = (int)source.Size.Y
                    };

                    SDL2.SDL.SDL_RenderCopyEx(this.SDLRenderer, sdltexture.SDL_Image, ref srect, ref drect, rotation * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                }
            }
        }
    }
}