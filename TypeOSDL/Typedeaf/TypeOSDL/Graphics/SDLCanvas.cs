using SDL2;
using System;
using System.Collections.Generic;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Graphics;
using SDL_Renderer = System.IntPtr;

namespace Typedeaf.TypeOSDL
{
    namespace Graphics
    {
        public partial class SDLCanvas : Canvas
        {
            public SDL_Renderer SDL_Renderer { get; private set; }
            /// <summary>
            /// Do not call directly, use Window.CreateCanvas instead
            /// </summary>
            public SDLCanvas(SDLWindow window) : base(window)
            {
                if (window != null)
                {
                    SDL_Renderer = SDL.SDL_CreateRenderer(window.SDL_Window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
                    if (SDL_Renderer == null)
                    {
                        SDL.SDL_DestroyWindow(window.SDL_Window);
                        Console.WriteLine("SDL_CreateRenderer  Error: " + SDL.SDL_GetError());
                        SDL.SDL_Quit();
                        //TODO: Error handling!
                    }
                }
            }

            public override void Clear(Color clearColor)
            {
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, (byte)clearColor.R, (byte)clearColor.G, (byte)clearColor.B, (byte)clearColor.A);
                SDL.SDL_RenderClear(SDL_Renderer);
            }

            public override void DrawLine(Vec2 from, Vec2 size, Color color)
            {
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                SDL.SDL_RenderDrawLineF(SDL_Renderer, from.X, from.Y, size.X, size.Y);
            }

            public override void DrawLineE(Vec2 from, Vec2 to, Color color)
            {
                DrawLine(from, to - from, color);
            }

            public override void DrawLines(List<Vec2> points, Color color)
            {
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);

                var sdlpoints = new SDL.SDL_FPoint[points.Count];
                int i = 0;
                foreach (var point in points)
                {
                    sdlpoints[i] = new SDL.SDL_FPoint();
                    sdlpoints[i].x = point.X;
                    sdlpoints[i].y = point.Y;
                    i++;
                }

                SDL.SDL_RenderDrawLinesF(SDL_Renderer, sdlpoints, points.Count);
            }

            public override void DrawPixel(Vec2 point, Color color)
            {
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                SDL.SDL_RenderDrawPointF(SDL_Renderer, point.X, point.Y);
            }

            public override void DrawPixels(List<Vec2> points, Color color)
            {
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                var sdlpoints = new SDL.SDL_FPoint[points.Count];
                int i = 0;
                foreach (var point in points)
                {
                    sdlpoints[i] = new SDL.SDL_FPoint();
                    sdlpoints[i].x = point.X;
                    sdlpoints[i].y = point.Y;
                    i++;
                }
                SDL.SDL_RenderDrawPointsF(SDL_Renderer, sdlpoints, points.Count);
            }

            public override void DrawRectangle(Rectangle rectangle, bool filled, Color color)
            {
                DrawRectangle(rectangle.Pos, rectangle.Size, filled, color);
            }

            public override void DrawRectangle(Vec2 from, Vec2 size, bool filled, Color color)
            {
                SDL.SDL_SetRenderDrawColor(SDL_Renderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                var rect = new SDL.SDL_FRect();
                rect.x = from.X;
                rect.y = from.Y;
                rect.w = size.X;
                rect.h = size.Y;

                if (filled)
                {
                    SDL.SDL_RenderFillRectF(SDL_Renderer, ref rect);
                }
                else
                {
                    SDL.SDL_RenderDrawRectF(SDL_Renderer, ref rect);
                }
            }

            public override void DrawRectangleE(Vec2 from, Vec2 to, bool filled, Color color)
            {
                DrawRectangle(from, to - from, filled, color);
            }

            public override void Present()
            {
                SDL.SDL_RenderPresent(SDL_Renderer);
            }

            public override Rectangle Viewport
            {
                get {
                    SDL.SDL_RenderGetViewport(SDL_Renderer, out SDL.SDL_Rect rect);
                    return new Rectangle(rect.x, rect.y, rect.w, rect.h);
                }
                set {
                    var rect = new SDL.SDL_Rect();
                    rect.x = (int)value.Pos.X;
                    rect.y = (int)value.Pos.Y;
                    rect.w = (int)value.Size.X;
                    rect.h = (int)value.Size.Y;
                    SDL.SDL_RenderSetViewport(SDL_Renderer, ref rect);
                }
            }
        }
    }
}
