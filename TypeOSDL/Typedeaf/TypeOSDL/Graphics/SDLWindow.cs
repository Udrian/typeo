using SDL2;
using System;
using Typedeaf.TypeOCommon;
using Typedeaf.TypeOCore;
using Typedeaf.TypeOCore.Graphics;
using Typedeaf.TypeOSDL.Graphics;
using SDL_Window = System.IntPtr;

namespace Typedeaf.TypeOSDL
{
    namespace Graphics
    {
        public class SDLWindow : Window
        {
            public SDL_Window SDL_Window { get; private set; }

            public void Initialize(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false)
            {
                SDL_Window = SDL.SDL_CreateWindow(title, (int)position.X, (int)position.Y, (int)size.X, (int)size.Y, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
                if (SDL_Window == null)
                {
                    //TODO: Error handling
                    Console.WriteLine("SDL_CreateWindow  Error: " + SDL.SDL_GetError());
                    SDL.SDL_Quit();
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
                    return SDL.SDL_GetWindowTitle(SDL_Window);
                }
                set {
                    if (SDL_Window != null)
                        SDL.SDL_SetWindowTitle(SDL_Window, value);
                }
            }
            public override Vec2 Position {
                get {
                    if (SDL_Window == null)
                        return Vec2.Zero;
                    SDL.SDL_GetWindowPosition(SDL_Window, out int x, out int y);
                    return new Vec2(x, y);
                }
                set {
                    if (SDL_Window != null)
                        SDL.SDL_SetWindowPosition(SDL_Window, (int)value.X, (int)value.Y);
                }
            }
            public override Vec2 Size {
                get {
                    if (SDL_Window == null)
                        return Vec2.Zero;
                    SDL.SDL_GetWindowSize(SDL_Window, out int x, out int y);
                    return new Vec2(x, y);
                }
                set {
                    if (SDL_Window != null)
                        SDL.SDL_SetWindowSize(SDL_Window, (int)value.X, (int)value.Y);
                }
            }
            public override bool Fullscreen {
                get {
                    if (SDL_Window == null)
                        return false;
                    SDL.SDL_GetWindowDisplayMode(SDL_Window, out SDL.SDL_DisplayMode mode);
                    return mode.format == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
                }
                set {
                    if (SDL_Window != null)
                        SDL.SDL_SetWindowFullscreen(SDL_Window, (uint)(value ? SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN : 0));
                }
            }
            public override bool Borderless {
                get {
                    if (SDL_Window == null)
                        return false;
                    SDL.SDL_GetWindowDisplayMode(SDL_Window, out SDL.SDL_DisplayMode mode);
                    return mode.format == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
                }
                set {
                    if (SDL_Window != null)
                        SDL.SDL_SetWindowFullscreen(SDL_Window, (uint)(value ? SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP : 0));
                }
            }
        }
    }

    public partial class SDLGame
    {
        public SDLWindow CreateWindow()
        {
            return CreateWindow<SDLWindow>();
        }

        public SDLWindow CreateWindow(string title, Vec2 position, Vec2 size, bool fullscreen = false, bool borderless = false)
        {
            var win = CreateWindow();
            win.Initialize(title, position, size, fullscreen, borderless);

            return win;
        }
    }
}
