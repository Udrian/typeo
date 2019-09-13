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
            public TypeOSDLWindow(Core.TypeO typeO,
                                  string title,
                                    Vec2 position,
                                    Vec2 size) : base(typeO)
            {
                SDLWindow = SDL.SDL_CreateWindow(title, (int)position.X, (int)position.Y, (int)size.X, (int)size.Y, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
                if (SDLWindow == null)
                {
                    //TODO: Error handling
                    Console.WriteLine("SDL_CreateWindow  Error: " + SDL.SDL_GetError());
                    SDL.SDL_Quit();
                    //return (SDL_Window)0;
                }
            }

            public SDL_Window SDLWindow { get; private set; }

            public override string Title {
                get {
                    if (SDLWindow == null)
                        return "";
                    return SDL.SDL_GetWindowTitle(SDLWindow);
                }
                set {
                    if (SDLWindow != null)
                        SDL.SDL_SetWindowTitle(SDLWindow, value);
                }
            }
            public override Vec2 Position {
                get {
                    if (SDLWindow == null)
                        return Vec2.Zero;
                    SDL.SDL_GetWindowPosition(SDLWindow, out int x, out int y);
                    return new Vec2(x, y);
                }
                set {
                    if (SDLWindow != null)
                        SDL.SDL_SetWindowPosition(SDLWindow, (int)value.X, (int)value.Y);
                }
            }
            public override Vec2 Size {
                get {
                    if (SDLWindow == null)
                        return Vec2.Zero;
                    SDL.SDL_GetWindowSize(SDLWindow, out int x, out int y);
                    return new Vec2(x, y);
                }
                set {
                    if (SDLWindow != null)
                        SDL.SDL_SetWindowSize(SDLWindow, (int)value.X, (int)value.Y);
                }
            }
            public override bool Fullscreen {
                get {
                    if (SDLWindow == null)
                        return false;
                    SDL.SDL_GetWindowDisplayMode(SDLWindow, out SDL.SDL_DisplayMode mode);
                    return mode.format == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
                }
                set {
                    if (SDLWindow != null)
                        SDL.SDL_SetWindowFullscreen(SDLWindow, (uint)(value ? SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN : 0));
                }
            }
            public override bool Borderless {
                get {
                    if (SDLWindow == null)
                        return false;
                    SDL.SDL_GetWindowDisplayMode(SDLWindow, out SDL.SDL_DisplayMode mode);
                    return mode.format == (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP;
                }
                set {
                    if (SDLWindow != null)
                        SDL.SDL_SetWindowFullscreen(SDLWindow, (uint)(value ? SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP : 0));
                }
            }
        }
    }

    namespace Modules
    {
        public partial class TypeOSDL : Module
        {
            public Window CreateWindow(Core.TypeO typeO,
                                       string title,
                                         Vec2 position,
                                         Vec2 size,
                                         bool fullscreen,
                                         bool borderless)
            {
                var win = new TypeOSDLWindow(typeO, title, position, size);

                if (fullscreen)
                    win.Fullscreen = fullscreen;
                if (borderless)
                    win.Borderless = borderless;
                return win;
            }
        }
    }
}
