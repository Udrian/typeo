using SDL2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Graphics;
using SDL_Window = System.IntPtr;

namespace Typedeaf.TypeO.Engine
{
    namespace Graphics
    {
        public class TypeOSDLWindow : Window
        {
            /// <summary>
            /// Do not call directly, use Game.CreateWindow instead
            /// </summary>
            public TypeOSDLWindow(string title,
                                    Vec2 position,
                                    Vec2 size,
                                    bool fullscreen,
                                    bool borderless) : base(title, position, size, fullscreen, borderless)
            {
                window = SDL.SDL_CreateWindow(title, (int)position.X, (int)position.Y, (int)size.X, (int)size.Y, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
                if (window == null)
                {
                    //TODO: Error handling
                    Console.WriteLine("SDL_CreateWindow  Error: " + SDL.SDL_GetError());
                    SDL.SDL_Quit();
                    //return (SDL_Window)0;
                }
                if(fullscreen)
                    Fullscreen = fullscreen;
                if(borderless)
                    Borderless = borderless;
            }

            private SDL_Window window;

            public override string Title {
                get {
                    if (window == null)
                        return "";
                    return SDL.SDL_GetWindowTitle(window);
                }
                set {
                    if (window != null)
                        SDL.SDL_SetWindowTitle(window, value);
                }
            }
            public override Vec2 Position {
                get {
                    if (window == null)
                        return Vec2.Zero;
                    SDL.SDL_GetWindowPosition(window, out int x, out int y);
                    return new Vec2(x, y);
                }
                set {
                    if (window != null)
                        SDL.SDL_SetWindowPosition(window, (int)value.X, (int)value.Y);
                }
            }
            public override Vec2 Size {
                get {
                    if (window == null)
                        return Vec2.Zero;
                    SDL.SDL_GetWindowSize(window, out int x, out int y);
                    return new Vec2(x, y);
                }
                set {
                    if (window != null)
                        SDL.SDL_SetWindowSize(window, (int)value.X, (int)value.Y);
                }
            }
            public override bool Fullscreen {
                get {
                    if (window == null)
                        return false;
                    SDL.SDL_GetWindowDisplayMode(window, out SDL.SDL_DisplayMode mode);
                    return mode.format == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
                }
                set {
                    if (window != null)
                        SDL.SDL_SetWindowFullscreen(window, (uint)(value ? SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN : 0));
                }
            }
            public override bool Borderless {
                get {
                    if (window == null)
                        return false;
                    SDL.SDL_GetWindowDisplayMode(window, out SDL.SDL_DisplayMode mode);
                    return mode.format == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
                }
                set {
                    if (window != null)
                        SDL.SDL_SetWindowFullscreen(window, (uint)(value ? SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP : 0));
                }
            }
        }
    }

    namespace Modules
    {
        public partial class TypeOSDL : Module
        {
            public Window CreateWindow(string title,
                             Vec2 position,
                             Vec2 size,
                             bool fullscreen,
                             bool borderless)
            {
                return new TypeOSDLWindow(title, position, size, fullscreen, borderless);
            }
        }
    }
}
