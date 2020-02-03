using System;
using Typedeaf.Common;
using TypeOEngine.Typedeaf.Core.Engine.Contents;
using TypeOEngine.Typedeaf.Core.Engine.Graphics;
using TypeOEngine.Typedeaf.SDL.Engine.Contents;
using SDL_Window = System.IntPtr;

namespace TypeOEngine.Typedeaf.SDL
{
    namespace Engine.Graphics
    {
        public class SDLWindow : Window
        {
            public SDL_Window SDL_Window { get; private set; }

            public override void Initialize(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false)
            {
                SDL_Window = SDL2.SDL.SDL_CreateWindow(title, (int)position.X, (int)position.Y, (int)size.X, (int)size.Y, SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
                if (SDL_Window == null)
                {
                    //TODO: Error handling
                    Console.WriteLine("SDL_CreateWindow  Error: " + SDL2.SDL.SDL_GetError());
                    SDL2.SDL.SDL_Quit();
                    //return (SDL_Window)0;
                }

                if (fullscreen)
                    Fullscreen = fullscreen;
                if (borderless)
                    Borderless = borderless;
            }

            public override string Title {
                get {
                    if (SDL_Window == null)
                        return "";
                    return SDL2.SDL.SDL_GetWindowTitle(SDL_Window);
                }
                set {
                    if (SDL_Window != null)
                        SDL2.SDL.SDL_SetWindowTitle(SDL_Window, value);
                }
            }
            public override Vec2 Position {
                get {
                    if (SDL_Window == null)
                        return Vec2.Zero;
                    SDL2.SDL.SDL_GetWindowPosition(SDL_Window, out int x, out int y);
                    return new Vec2(x, y);
                }
                set {
                    if (SDL_Window != null)
                        SDL2.SDL.SDL_SetWindowPosition(SDL_Window, (int)value.X, (int)value.Y);
                }
            }
            public override Vec2 Size {
                get {
                    if (SDL_Window == null)
                        return Vec2.Zero;
                    SDL2.SDL.SDL_GetWindowSize(SDL_Window, out int x, out int y);
                    return new Vec2(x, y);
                }
                set {
                    if (SDL_Window != null)
                        SDL2.SDL.SDL_SetWindowSize(SDL_Window, (int)value.X, (int)value.Y);
                }
            }
            public override bool Fullscreen {
                get {
                    if (SDL_Window == null)
                        return false;
                    SDL2.SDL.SDL_GetWindowDisplayMode(SDL_Window, out SDL2.SDL.SDL_DisplayMode mode);
                    return mode.format == (uint)SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
                }
                set {
                    if (SDL_Window != null)
                        SDL2.SDL.SDL_SetWindowFullscreen(SDL_Window, (uint)(value ? SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN : 0));
                }
            }
            public override bool Borderless {
                get {
                    if (SDL_Window == null)
                        return false;
                    SDL2.SDL.SDL_GetWindowDisplayMode(SDL_Window, out SDL2.SDL.SDL_DisplayMode mode);
                    return mode.format == (uint)SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
                }
                set {
                    if (SDL_Window != null)
                        SDL2.SDL.SDL_SetWindowFullscreen(SDL_Window, (uint)(value ? SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP : 0));
                }
            }

            public override Canvas CreateCanvas()
            {
                var canvas = new SDLCanvas
                {
                    Window = this
                };
                canvas.Initialize();
                return canvas;
            }

            public override Canvas CreateCanvas(Rectangle viewport)
            {
                var canvas = CreateCanvas();
                canvas.Viewport = viewport;
                return canvas;
            }

            public override ContentLoader CreateContentLoader(Canvas canvas)
            {
                return new SDLContentLoader((SDLCanvas)canvas, TypeO.GetContentBinding());
            }
        }
    }
}
