using SDL2;
using Typedeaf.Common;
using SDL_Font = System.IntPtr;
using TypeOEngine.Typedeaf.SDL.Contents;
using TypeOEngine.Typedeaf.Core.Contents;
using TypeOEngine.Typedeaf.SDL.Engine.Graphics;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Contents
    {
        public class SDLFont : Font
        {
            public SDLCanvas Canvas { get; private set; }
            public SDL_Font SDL_Font { get; private set; }

            public SDLFont() : base() { }

            public override Vec2 MeasureString(string text)
            {
                SDL_ttf.TTF_SizeText(SDL_Font, text, out int w, out int h);
                return new Vec2(w, h);
            }

            public override void Load(string path, ContentLoader contentLoader)
            {
                Canvas = contentLoader.Canvas as SDLCanvas;
                FilePath = path;

                SDL_Font = SDL_ttf.TTF_OpenFont(FilePath, FontSize);
                FontSize = FontSize;
                //TODO: Error handling
                /*if (SDLFont == null)
                {
                    Console.WriteLine("IMG_LoadTexture Error: " + SDL.SDL_GetError());
                    return (SDL_Texture)0;
                }

                return SDLFont;*/
            }

            public override int FontSize { 
                get => base.FontSize;
                set {
                    base.FontSize = value;

                    SDL_Font = SDL_ttf.TTF_OpenFont(FilePath, FontSize);
                }
            }
        }
    }

    namespace Engine.Graphics
    {
        partial class SDLCanvas
        {
            public override void DrawText(Font font, string text, Vec2 pos)
            {
                DrawText(font, text, pos, null);
            }

            public override void DrawText(Font font, string text, Vec2 pos, Vec2 scale = null, double rotate = 0, Vec2 origin = null, Color color = null, Texture.Flipped flipped = Texture.Flipped.None, Rectangle source = null)
            {
                InternalDrawText(font, text, pos, scale ?? new Vec2(1), rotate, origin ?? new Vec2(0), color, flipped, source);

            }

            private void InternalDrawText(Font font, string text, Vec2 pos, Vec2 scale, double rotate, Vec2 origin, Color color, Texture.Flipped flipped, Rectangle source)
            {
                const double degreeToRadianConst = 57.2957795131;

                var sdlFont = font as SDLFont;
                //TODO: Error handling

                if (color == null)
                    color = Color.White;
                var sdlColor = new SDL2.SDL.SDL_Color
                {
                    r = (byte)color.R,
                    g = (byte)color.G,
                    b = (byte)color.B,
                    a = (byte)color.A
                };
                var fontSur = SDL_ttf.TTF_RenderText_Solid(sdlFont.SDL_Font, text, sdlColor);
                var fontTex = SDL2.SDL.SDL_CreateTextureFromSurface(this.SDLRenderer, fontSur);

                SDL2.SDL.SDL_QueryTexture(fontTex, out _, out _, out int w, out int h);
                var fontSize = new Vec2(w, h);
                var drect = new SDL2.SDL.SDL_Rect
                {
                    x = (int)(pos.X - origin.X),
                    y = (int)(pos.Y - origin.Y),
                    w = (int)(fontSize.X * scale.X),
                    h = (int)(fontSize.Y * scale.Y)
                };

                var sdlPoint = new SDL2.SDL.SDL_Point
                {
                    x = (int)origin.X,
                    y = (int)origin.Y
                };

                var sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_NONE;
                if (flipped == Texture.Flipped.Horizontal)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                else if (flipped == Texture.Flipped.Vertical)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
                else if (flipped == Texture.Flipped.Both)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL2.SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;

                if(source == null)
                    SDL2.SDL.SDL_RenderCopyEx(this.SDLRenderer, fontTex, (SDL_Font)null, ref drect, rotate * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                else
                {
                    SDL2.SDL.SDL_Rect srect = new SDL2.SDL.SDL_Rect
                    {
                        x = (int)source.Pos.X,
                        y = (int)source.Pos.Y,
                        w = (int)source.Size.X,
                        h = (int)source.Size.Y
                    };

                    SDL2.SDL.SDL_RenderCopyEx(this.SDLRenderer, fontTex, ref srect, ref drect, rotate * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                }
            }
        }
    }
}