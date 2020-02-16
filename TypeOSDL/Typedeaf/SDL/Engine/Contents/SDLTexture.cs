using SDL2;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.SDL.Engine.Contents;
using TypeOEngine.Typedeaf.SDL.Engine.Graphics;
using SDL_Image = System.IntPtr;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Contents
    {
        public class SDLTexture : Texture
        {
            public ILogger Logger { get; set; }
            public SDL_Image SDL_Image { get; set; }

            public SDLTexture() : base() { }

            public override void Load(string path, ContentLoader contentLoader)
            {
                var sdlCanvas = contentLoader.Canvas as SDLCanvas;
                FilePath = path;

                SDL_Image = SDL_image.IMG_LoadTexture(sdlCanvas.SDLRenderer, FilePath);
                if (SDL_Image == SDL_Image.Zero)
                {
                    Logger.Log(LogLevel.Error, $"Error loading SDLTexture '{path}' with error : {SDL2.SDL.SDL_GetError()}");
                }

                SDL2.SDL.SDL_QueryTexture(SDL_Image, out _, out _, out int w, out int h);
                Size = new Vec2(w, h);
            }
        }
    }

    namespace Engine.Graphics
    {
        partial class SDLCanvas
        {
            public override void DrawImage(Texture texture, Vec2 pos, Entity2d entity = null)
            {
                DrawImage(texture, pos, null, entity: entity);
            }

            public override void DrawImage(Texture texture, Vec2 pos, Vec2 scale = null, double rotation = 0, Vec2 origin = null, Color color = null, Flipped flipped = Flipped.None, Rectangle source = null, Entity2d entity = null)
            {
                InternalDrawImage(texture, pos, scale ?? new Vec2(1), rotation, origin ?? new Vec2(0), color, flipped, source, entity);

            }

            private void InternalDrawImage(Texture texture, Vec2 pos, Vec2 scale, double rotation, Vec2 origin, Color color, Flipped flipped, Rectangle source, Entity2d entity)
            {
                const double degreeToRadianConst = 57.2957795131;

                if (!(texture is SDLTexture sdltexture))
                {
                    Logger.Log(LogLevel.Warning, "Texture is not of type SDLTexture");
                    return;
                }

                pos      += entity?.DrawBounds.Pos ?? Vec2.Zero;
                scale    *= entity?.Scale          ?? Vec2.One;
                rotation += entity?.Rotation       ?? 0;
                origin   += entity?.Origin         ?? Vec2.Zero;
                //TODO: Blend color and flip entity

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
                if (flipped == Flipped.Horizontal)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                else if (flipped == Flipped.Vertical)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
                else if (flipped == Flipped.Both)
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