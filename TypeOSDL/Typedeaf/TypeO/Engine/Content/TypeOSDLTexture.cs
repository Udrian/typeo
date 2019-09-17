using SDL2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Graphics;
using SDL_Image = System.IntPtr;

namespace Typedeaf.TypeO.Engine
{
    namespace Content
    {
        public class TypeOSDLTexture : Texture
        {
            public SDL_Image SDLImage { get; set; }
            /// <summary>
            /// Do not call directly, use Game.Content.LoadTexture instead
            /// </summary>
            public TypeOSDLTexture(TypeOSDLCanvas canvas, Core.TypeO typeO, string path) : base(typeO, path)
            {
                SDLImage = SDL_image.IMG_LoadTexture(canvas.SDLRenderer, path);
                SDL.SDL_QueryTexture(SDLImage, out _, out _, out int w, out int h);
                Size = new Vec2(w, h);
                //TODO: Error handling
                /*if (tex == null)
                {
                    Console.WriteLine("IMG_LoadTexture Error: " + SDL.SDL_GetError());
                    return (SDL_Texture)0;
                }

                return tex;*/
            }

            public override Vec2 Size { get; protected set; }

            /*public abstract void SetColor(Vec2 pos, Color color);
            public abstract Color GetColor(Vec2 pos);

            public abstract ColorMap ColorMap { get; set; }
            public abstract Texture Cut(Rectangle rectangle);*/
        }
    }

    namespace Graphics
    {
        public partial class TypeOSDLCanvas : Canvas
        {
            public override Texture LoadTexture(string path)
            {
                return new TypeOSDLTexture(this, TypeO, path);
            }

            public override void DrawImage(Texture texture, Vec2 pos)
            {
                DrawImage(texture, pos, null);
            }

            public override void DrawImage(Texture texture, Vec2 pos, Vec2 scale, double rotate, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source)
            {
                InternalDrawImage(texture, pos, scale, rotate, origin, color, flipped, source);
            }

            public override void DrawImage(Texture texture, Vec2 pos, Vec2? scale = null, double rotate = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null)
            {
                InternalDrawImage(texture, pos, scale??new Vec2(1), rotate, origin, color, flipped, source);

            }

            private void InternalDrawImage(Texture texture, Vec2 pos, Vec2 scale, double rotate, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source)
            {
                const double degreeToRadianConst = 57.2957795131;

                var sdltexture = texture as TypeOSDLTexture;
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
                SDL.SDL_SetTextureColorMod(sdltexture.SDLImage, (byte)color.R, (byte)color.G, (byte)color.B);
                SDL.SDL_SetTextureAlphaMod(sdltexture.SDLImage, (byte)color.A);

                var sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                if (flipped == Texture.Flipped.Horizontal)
                    sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                else if (flipped == Texture.Flipped.Vertical)
                    sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
                else if (flipped == Texture.Flipped.Both)
                    sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;

                if(source == null)
                    SDL.SDL_RenderCopyEx(this.SDLRenderer, sdltexture.SDLImage, (IntPtr)null, ref drect, rotate * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                else
                {
                    SDL.SDL_Rect srect = new SDL.SDL_Rect();
                    srect.x = (int)source.Pos.X;
                    srect.y = (int)source.Pos.Y;
                    srect.w = (int)source.Size.X;
                    srect.h = (int)source.Size.Y;

                    SDL.SDL_RenderCopyEx(this.SDLRenderer, sdltexture.SDLImage, ref srect, ref drect, rotate * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                }
            }
        }
    }
}