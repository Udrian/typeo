﻿using System;
using System.Collections.Generic;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using SDL_Renderer = System.IntPtr;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Graphics
    {
        public partial class SDLCanvas : Canvas
        {
            public SDL_Renderer SDLRenderer { get; private set; }

            public override void Initialize()
            {
                if (Window != null && Window is SDLWindow)
                {
                    var sdlWindow = (Window as SDLWindow).SDL_Window;

                    SDLRenderer = SDL2.SDL.SDL_CreateRenderer(sdlWindow, -1, SDL2.SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL2.SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
                    if (SDLRenderer == null)
                    {
                        SDL2.SDL.SDL_DestroyWindow(sdlWindow);
                        Console.WriteLine("SDL_CreateRenderer  Error: " + SDL2.SDL.SDL_GetError());
                        SDL2.SDL.SDL_Quit();
                        //TODO: Error handling!
                    }
                }
            }

            public override void Clear(Color clearColor)
            {
                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)clearColor.R, (byte)clearColor.G, (byte)clearColor.B, (byte)clearColor.A);
                SDL2.SDL.SDL_RenderClear(SDLRenderer);
            }

            public override void DrawLine(Vec2 from, Vec2 size, Color color)
            {
                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                SDL2.SDL.SDL_RenderDrawLineF(SDLRenderer, (float)from.X, (float)from.Y, (float)size.X, (float)size.Y);
            }

            public override void DrawLineE(Vec2 from, Vec2 to, Color color)
            {
                DrawLine(from, to - from, color);
            }

            public override void DrawLines(List<Vec2> points, Color color)
            {
                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);

                var sdlpoints = new SDL2.SDL.SDL_FPoint[points.Count];
                int i = 0;
                foreach (var point in points)
                {
                    sdlpoints[i] = new SDL2.SDL.SDL_FPoint
                    {
                        x = (float)point.X,
                        y = (float)point.Y
                    };
                    i++;
                }

                SDL2.SDL.SDL_RenderDrawLinesF(SDLRenderer, sdlpoints, points.Count);
            }

            public override void DrawPixel(Vec2 point, Color color)
            {
                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                SDL2.SDL.SDL_RenderDrawPointF(SDLRenderer, (float)point.X, (float)point.Y);
            }

            public override void DrawPixels(List<Vec2> points, Color color)
            {
                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                var sdlpoints = new SDL2.SDL.SDL_FPoint[points.Count];
                int i = 0;
                foreach (var point in points)
                {
                    sdlpoints[i] = new SDL2.SDL.SDL_FPoint
                    {
                        x = (float)point.X,
                        y = (float)point.Y
                    };
                    i++;
                }
                SDL2.SDL.SDL_RenderDrawPointsF(SDLRenderer, sdlpoints, points.Count);
            }

            public override void DrawRectangle(Rectangle rectangle, bool filled, Color color)
            {
                DrawRectangle(rectangle.Pos, rectangle.Size, filled, color);
            }

            public override void DrawRectangle(Vec2 from, Vec2 size, bool filled, Color color)
            {
                SDL2.SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)color.R, (byte)color.G, (byte)color.B, (byte)color.A);
                var rect = new SDL2.SDL.SDL_FRect
                {
                    x = (float)from.X,
                    y = (float)from.Y,
                    w = (float)size.X,
                    h = (float)size.Y
                };

                if (filled)
                {
                    SDL2.SDL.SDL_RenderFillRectF(SDLRenderer, ref rect);
                }
                else
                {
                    SDL2.SDL.SDL_RenderDrawRectF(SDLRenderer, ref rect);
                }
            }

            public override void DrawRectangleE(Vec2 from, Vec2 to, bool filled, Color color)
            {
                DrawRectangle(from, to - from, filled, color);
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