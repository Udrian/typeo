using SDL2;
using System;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore.Content;
using Typedeaf.TypeOCore.Graphics;
using SDL_Font = System.IntPtr;
using Typedeaf.TypeOSDL.Graphics;
using Typedeaf.TypeOSDL.Content;

namespace Typedeaf.TypeOSDL
{
    namespace Content
    {
        public class SDLFont : Font
        {
            public SDLCanvas Canvas { get; private set; }
            public SDL_Font SDL_Font { get; private set; }
            public int FontSize { get; protected set; }
            //public SDL_Image SDLImage { get; set; }
            /// <summary>
            /// Do not call directly, use Game.Content.LoadTexture instead
            /// </summary>
            public SDLFont(string path, int fontSize, SDLCanvas canvas) : base(path)
            {
                Canvas = canvas;
                SDL_Font = SDL_ttf.TTF_OpenFont(path, fontSize);
                FontSize = fontSize;
                //TODO: Error handling
                /*if (SDLFont == null)
                {
                    Console.WriteLine("IMG_LoadTexture Error: " + SDL.SDL_GetError());
                    return (SDL_Texture)0;
                }

                return SDLFont;*/
            }

            public override Vec2 MeasureString(string text)
            {
                var fontSur = SDL_ttf.TTF_RenderText_Solid(SDL_Font, text, new SDL.SDL_Color());
                var fontTex = SDL.SDL_CreateTextureFromSurface(Canvas.SDLRenderer, fontSur);

                SDL.SDL_QueryTexture(fontTex, out _, out _, out int w, out int h);
                return new Vec2(w, h);
            }
        }

        public partial class SDLContentLoader : ContentLoader
        {
            public SDLFont LoadFont(string path, int fontSize)
            {
                return LoadFont<SDLFont>(path, fontSize, Canvas);
            }
        }
    }

    namespace Graphics
    {
        public partial class SDLCanvas : Canvas
        {
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

                var sdlFont = font as SDLFont;
                //TODO: Error handling

                if (color == null)
                    color = Color.White;
                var sdlColor = new SDL.SDL_Color();
                sdlColor.r = (byte)color.R;
                sdlColor.g = (byte)color.G;
                sdlColor.b = (byte)color.B;
                sdlColor.a = (byte)color.A;
                var fontSur = SDL_ttf.TTF_RenderText_Solid(sdlFont.SDL_Font, text, sdlColor);
                var fontTex = SDL.SDL_CreateTextureFromSurface(this.SDLRenderer, fontSur);

                SDL.SDL_QueryTexture(fontTex, out _, out _, out int w, out int h);
                var fontSize = new Vec2(w, h);
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