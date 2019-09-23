using SDL2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Content;
using Typedeaf.TypeO.Engine.Graphics;
using SDL_Image = System.IntPtr;
using SDL_Font = System.IntPtr;

namespace Typedeaf.TypeO.Engine
{
    namespace Content
    {
        public class TypeOSDLFont : Font
        {
            public SDL_Font SDLFont { get; set; }
            //public SDL_Image SDLImage { get; set; }
            /// <summary>
            /// Do not call directly, use Game.Content.LoadTexture instead
            /// </summary>
            public TypeOSDLFont(TypeOSDLCanvas canvas, Core.TypeO typeO, string path, int fontSize) : base(typeO, path, fontSize)
            {
                //TODO: Error handling
                /*if (tex == null)
                {
                    Console.WriteLine("IMG_LoadTexture Error: " + SDL.SDL_GetError());
                    return (SDL_Texture)0;
                }

                return tex;*/




                SDLFont = SDL_ttf.TTF_OpenFont(path, fontSize);
            }

            public override Vec2 MeasureString(string text)
            {
                //TODO: Implement
                return new Vec2();
            }
        }
    }

    namespace Graphics
    {
        public partial class TypeOSDLCanvas : Canvas
        {
            public override Font LoadFont(string path, int fontSize)
            {
                return new TypeOSDLFont(this, TypeO, path, fontSize);
            }

            public override void DrawText(Font font, string text, Vec2 pos)
            {
                DrawText(font, text, pos, null);
            }

            public override void DrawText(Font font, string text, Vec2 pos, Vec2 scale, double rotate, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source)
            {
                InternalDrawText(font, text, pos, scale, rotate, origin, color, flipped, source);
            }

            public override void DrawText(Font font, string text, Vec2 pos, Vec2? scale = null, double rotate = 0, Vec2 origin = new Vec2(), Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null)
            {
                InternalDrawText(font, text, pos, scale??new Vec2(1), rotate, origin, color, flipped, source);

            }

            private void InternalDrawText(Font font, string text, Vec2 pos, Vec2 scale, double rotate, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source)
            {
                const double degreeToRadianConst = 57.2957795131;

                var sdlFont = font as TypeOSDLFont;
                //TODO: Error handling

                if (color == null)
                    color = Color.White;
                var sdlColor = new SDL.SDL_Color();
                sdlColor.r = (byte)color.R;
                sdlColor.g = (byte)color.G;
                sdlColor.b = (byte)color.B;
                sdlColor.a = (byte)color.A;
                var fontSur = SDL_ttf.TTF_RenderText_Solid(sdlFont.SDLFont, text, sdlColor);
                var fontTex = SDL.SDL_CreateTextureFromSurface(this.SDLRenderer, fontSur);

                var fontSize = font.MeasureString(text);
                var drect = new SDL.SDL_Rect();
                drect.x = (int)(pos.X - origin.X);
                drect.y = (int)(pos.Y - origin.Y);
                drect.w = (int)(fontSize.X * scale.X);
                drect.h = (int)(fontSize.Y * scale.Y);

                var sdlPoint = new SDL.SDL_Point();
                sdlPoint.x = (int)origin.X;
                sdlPoint.y = (int)origin.Y;

                var sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                if (flipped == Texture.Flipped.Horizontal)
                    sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                else if (flipped == Texture.Flipped.Vertical)
                    sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
                else if (flipped == Texture.Flipped.Both)
                    sdlRenderFlip = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;

                if(source == null)
                    SDL.SDL_RenderCopyEx(this.SDLRenderer, fontTex, (IntPtr)null, ref drect, rotate * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                else
                {
                    SDL.SDL_Rect srect = new SDL.SDL_Rect();
                    srect.x = (int)source.Pos.X;
                    srect.y = (int)source.Pos.Y;
                    srect.w = (int)source.Size.X;
                    srect.h = (int)source.Size.Y;

                    SDL.SDL_RenderCopyEx(this.SDLRenderer, fontTex, ref srect, ref drect, rotate * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                }
            }
        }
    }
}