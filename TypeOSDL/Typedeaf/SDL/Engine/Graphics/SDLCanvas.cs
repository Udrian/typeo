using SDL2;
using System;
using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.SDL.Engine.Contents;
using SDL_Renderer = System.IntPtr;
using SDL_Surface = System.IntPtr;
using SDL_Texture = System.IntPtr;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Graphics
    {
        public class SDLCanvas : Canvas
        {
            public ILogger Logger { get; set; }
            public SDL_Renderer SDLRenderer { get; private set; }

            public override void Initialize()
            {
                if (Window != null && Window is SDLWindow)
                {
                    var sdlWindow = (Window as SDLWindow).SDL_Window;

                    SDLRenderer = SDL2.SDL.SDL_CreateRenderer(sdlWindow, -1, SDL2.SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL2.SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
                    if (SDLRenderer == SDL_Renderer.Zero)
                    {
                        var message = $"Error creating SDLRenderer with error: {SDL2.SDL.SDL_GetError()}";
                        Logger.Log(LogLevel.Fatal, message);
                        //TODO: Maybe call Game.Exit() instead
                        SDL2.SDL.SDL_DestroyWindow(sdlWindow);
                        SDL2.SDL.SDL_Quit();
                        throw new InvalidOperationException(message);
                    }
                }
            }

            public override void Cleanup()
            {
                SDL2.SDL.SDL_DestroyRenderer(SDLRenderer);
            }

            public override void Clear(Color clearColor)
            {
                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)clearColor.R, (byte)clearColor.G, (byte)clearColor.B, (byte)clearColor.A);
                SDL2.SDL.SDL_RenderClear(SDLRenderer);
            }

            public override void DrawLine(Vec2 from, Vec2 size, Color color, Entity2d entity = null)
            {
                from += entity?.DrawBounds.Pos ?? Vec2.Zero;

                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                SDL2.SDL.SDL_RenderDrawLine(SDLRenderer, (int)from.X, (int)from.Y, (int)size.X, (int)size.Y);
            }

            public override void DrawLineE(Vec2 from, Vec2 to, Color color, Entity2d entity)
            {
                DrawLine(from, to - from, color, entity);
            }

            public override void DrawLines(List<Vec2> points, Color color, Entity2d entity = null)
            {
                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);

                var sdlpoints = new SDL2.SDL.SDL_Point[points.Count];
                int i = 0;
                foreach (var point in points)
                {
                    var tpoint = point + (entity?.DrawBounds.Pos ?? Vec2.Zero);

                    sdlpoints[i] = new SDL2.SDL.SDL_Point
                    {
                        x = (int)tpoint.X,
                        y = (int)tpoint.Y
                    };
                    i++;
                }

                SDL2.SDL.SDL_RenderDrawLines(SDLRenderer, sdlpoints, points.Count);
            }

            public override void DrawPixel(Vec2 point, Color color, Entity2d entity = null)
            {
                point += entity?.DrawBounds.Pos ?? Vec2.Zero;
                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                SDL2.SDL.SDL_RenderDrawPoint(SDLRenderer, (int)point.X, (int)point.Y);
            }

            public override void DrawPixels(List<Vec2> points, Color color, Entity2d entity = null)
            {
                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                var sdlpoints = new SDL2.SDL.SDL_Point[points.Count];
                int i = 0;
                foreach (var point in points)
                {
                    var tpoint = point + (entity?.DrawBounds.Pos ?? Vec2.Zero);
                    sdlpoints[i] = new SDL2.SDL.SDL_Point
                    {
                        x = (int)tpoint.X,
                        y = (int)tpoint.Y
                    };
                    i++;
                }
                SDL2.SDL.SDL_RenderDrawPoints(SDLRenderer, sdlpoints, points.Count);
            }

            public override void DrawRectangle(Rectangle rectangle, bool filled, Color color, Entity2d entity = null)
            {
                DrawRectangle(rectangle.Pos, rectangle.Size, filled, color, entity);
            }

            public override void DrawRectangle(Vec2 from, Vec2 size, bool filled, Color color, Entity2d entity = null)
            {
                from += entity?.DrawBounds.Pos ?? Vec2.Zero;

                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                var rect = new SDL2.SDL.SDL_Rect
                {
                    x = (int)from.X,
                    y = (int)from.Y,
                    w = (int)size.X,
                    h = (int)size.Y
                };

                if (filled)
                {
                    SDL2.SDL.SDL_RenderFillRect(SDLRenderer, ref rect);
                }
                else
                {
                    SDL2.SDL.SDL_RenderDrawRect(SDLRenderer, ref rect);
                }
            }

            public override void DrawRectangleE(Vec2 from, Vec2 to, bool filled, Color color, Entity2d entity = null)
            {
                DrawRectangle(from, to - from, filled, color, entity);
            }

            public override void Present()
            {
                SDL2.SDL.SDL_RenderPresent(SDLRenderer);
            }

            public override Rectangle Viewport
            {
                get {
                    SDL2.SDL.SDL_RenderGetViewport(SDLRenderer, out SDL2.SDL.SDL_Rect rect);
                    return new Rectangle(rect.x, rect.y, rect.w, rect.h);
                }
                set {
                    var rect = new SDL2.SDL.SDL_Rect
                    {
                        x = (int)value.Pos.X,
                        y = (int)value.Pos.Y,
                        w = (int)value.Size.X,
                        h = (int)value.Size.Y
                    };
                    SDL2.SDL.SDL_RenderSetViewport(SDLRenderer, ref rect);
                }
            }

            public override void DrawText(Font font, string text, Vec2 pos, Entity2d entity = null)
            {
                DrawText(font, text, pos, null, entity: entity);
            }

            public override void DrawText(Font font, string text, Vec2 pos, Vec2? scale = null, double rotation = 0, Vec2? origin = null, Color color = null, Flipped flipped = Flipped.None, Rectangle source = null, Entity2d entity = null)
            {
                InternalDrawText(font, text, pos, scale ?? new Vec2(1), rotation, origin ?? new Vec2(0), color, flipped, source, entity);
            }

            private void InternalDrawText(Font font, string text, Vec2 pos, Vec2 scale, double rotation, Vec2 origin, Color color, Flipped flipped, Rectangle source, Entity2d entity)
            {
                const double degreeToRadianConst = 57.2957795131;

                if (!(font is SDLFont sdlFont))
                {
                    Logger.Log(LogLevel.Warning, "Font is not of type SDLFont");
                    return;
                }

                pos += entity?.DrawBounds.Pos ?? Vec2.Zero;

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
                if (flipped == Flipped.Horizontal)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
                else if (flipped == Flipped.Vertical)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
                else if (flipped == Flipped.Both)
                    sdlRenderFlip = SDL2.SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL2.SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;

                if (source == null)
                    SDL2.SDL.SDL_RenderCopyEx(this.SDLRenderer, fontTex, (IntPtr)null, ref drect, rotation * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                else
                {
                    SDL2.SDL.SDL_Rect srect = new SDL2.SDL.SDL_Rect
                    {
                        x = (int)source.Pos.X,
                        y = (int)source.Pos.Y,
                        w = (int)source.Size.X,
                        h = (int)source.Size.Y
                    };

                    SDL2.SDL.SDL_RenderCopyEx(this.SDLRenderer, fontTex, ref srect, ref drect, rotation * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
                }

                SDL2.SDL.SDL_FreeSurface(fontSur);
                SDL2.SDL.SDL_DestroyTexture(fontTex);
            }

            public override void DrawImage(Texture texture, Vec2 pos, Entity2d entity = null)
            {
                DrawImage(texture, pos, null, entity: entity);
            }

            public override void DrawImage(Texture texture, Vec2 pos, Vec2? scale = null, double rotation = 0, Vec2? origin = null, Color color = null, Flipped flipped = Flipped.None, Rectangle source = null, Entity2d entity = null)
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

                pos += entity?.DrawBounds.Pos ?? Vec2.Zero;
                scale *= entity?.Scale ?? Vec2.One;
                rotation += entity?.Rotation ?? 0;
                origin += entity?.Origin ?? Vec2.Zero;
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

                if (source == null)
                    SDL2.SDL.SDL_RenderCopyEx(this.SDLRenderer, sdltexture.SDL_Image, (IntPtr)null, ref drect, rotation * degreeToRadianConst, ref sdlPoint, sdlRenderFlip);
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
