﻿using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.Core.Engine.Interfaces;
using TypeOEngine.Typedeaf.Core.Entities;
using SDL_Renderer = System.IntPtr;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Graphics
    {
        public partial class SDLCanvas : Canvas
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
                        Logger.Log(LogLevel.Fatal, $"Error creating SDLRenderer with error: {SDL2.SDL.SDL_GetError()}");
                        SDL2.SDL.SDL_DestroyWindow(sdlWindow);
                        SDL2.SDL.SDL_Quit();
                    }
                }
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
        }
    }
}
