using SDL2;
using System;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Graphics;
using Typedeaf.TypeOSDL.Content;
using Typedeaf.TypeOSDL.Graphics;
using SDL_Image = System.IntPtr;

namespace Typedeaf.TypeOSDL
{
    namespace Content
    {
        public class SDLTexture : Texture
        {
            public SDL_Image SDL_Image { get; set; }
            /// <summary>
            /// Do not call directly, use Game.Content.LoadTexture instead
            /// </summary>
            public SDLTexture(TypeO typeO, string path, SDLCanvas canvas) : base(typeO, path)
            {
                SDL_Image = SDL_image.IMG_LoadTexture(canvas.SDL_Renderer, path);
                SDL.SDL_QueryTexture(SDL_Image, out _, out _, out int w, out int h);
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

        public partial class SDLContentLoader : ContentLoader
        {
            public SDLTexture LoadTexture(string path)
            {
                return LoadTexture<SDLTexture>(path, Canvas);
            }
        }
    }

    namespace Graphics
    {
        public partial class SDLCanvas : Canvas
        {
            public override void DrawImage(Texture texture, Vec2 pos)
            {
                DrawImage(texture, pos, null);
            }

            public override void DrawImage(Texture texture, Vec2 pos, Vec2 scale, double rotation, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source)
            {
                InternalDrawImage(texture, pos, scale, rotation, origin, color, flipped, source);
            }

            public override void DrawImage(Texture texture, Vec2 pos, Vec2? scale = null, double rotation = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null)
            {
                InternalDrawImage(texture, pos, scale??new Vec2(1), rotation, origin, color, flipped, source);

            }

            private void InternalDrawImage(Texture texture, Vec2 pos, Vec2 scale, double rotation, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source)
            {
                const double degreeToRadianConst = 57.2957795131;

                var sdltexture = texture as SDLTexture;
                //TODO: Error handling

                var drect = new SDL.SDL_Rect();
                drect.x = (int)(pos.X - origin.X);
                drect.y = (int)(pos.Y - origin.Y);
                drect.w = (int)(texture.Size.X * scale.X);
                drect.h = (int)(texture.Size.Y * scale.Y);

                var sdlPoint = new SDL.SDL_Point();
                sdlPoint.x = (int)origin.X;
                sdlPoint.y = (int)origin.Y;

                if (color == null)
                    color = Color.White;
                SDL.SDL_SetTextureColorMod(sdltexture.SDL_Image, (byte)color.R, (byte)color.G, (byte)color.B);
                SDL.SDL_SetTextureAlphaMod(sdltexture.SDL_Image, (byte)color.A);

                var sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                if (flipped == Texture.Flipped.Horizontal)
                    sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                else if (flipped == Texture.Flipped.Vertical)
                    sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
                else if (flipped == Texture.Flipped.Both)
                    sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;

                if(source == null)
                    SDL.SDL_RenderCopyEx(this.SDL_Renderer, sdltexture.SDL_Image, (IntPtr)null, ref drect, rotation * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                else
                {
                    SDL.SDL_Rect srect = new SDL.SDL_Rect();
                    srect.x = (int)source.Pos.X;
                    srect.y = (int)source.Pos.Y;
                    srect.w = (int)source.Size.X;
                    srect.h = (int)source.Size.Y;

                    SDL.SDL_RenderCopyEx(this.SDL_Renderer, sdltexture.SDL_Image, ref srect, ref drect, rotation * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                }
            }
        }
    }
}