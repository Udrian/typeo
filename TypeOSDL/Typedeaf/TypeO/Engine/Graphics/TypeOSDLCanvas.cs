using SDL2;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Typedeaf.TypeO.Common;
using Typedeaf.TypeO.Engine.Core;
using Typedeaf.TypeO.Engine.Graphics;
using SDL_Window = System.IntPtr;
using SDL_Renderer = System.IntPtr;

namespace Typedeaf.TypeO.Engine
{
    namespace Graphics
    {
        public class TypeOSDLCanvas : Canvas
        {
            public SDL_Renderer SDLRenderer { get; private set; }
            /// <summary>
            /// Do not call directly, use Window.CreateCanvas instead
            /// </summary>
            public TypeOSDLCanvas(Core.TypeO typeO, Window window) : base(typeO, window)
            {
                var sdlWindow = window as TypeOSDLWindow;
                if (sdlWindow != null)
                {
                    SDLRenderer = SDL.SDL_CreateRenderer(sdlWindow.SDLWindow, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
                    if (SDLRenderer == null)
                    {
                        SDL.SDL_DestroyWindow(sdlWindow.SDLWindow);
                        Console.WriteLine("SDL_CreateRenderer  Error: " + SDL.SDL_GetError());
                        SDL.SDL_Quit();
                        //TODO: Error handling!
                    }
                }
            }

            public override void Clear()
            {
                SDL.SDL_SetRenderDrawColor(SDLRenderer, 0, 0, 0, 0);
                SDL.SDL_RenderClear(SDLRenderer);
            }

            public override void DrawLine()
            {
                SDL.SDL_SetRenderDrawColor(SDLRenderer, 255, 255, 255, 0);
                var rect = new SDL.SDL_Rect();
                rect.x = 10;
                rect.y = 10;
                rect.h = 100;
                rect.w = 100;
                SDL.SDL_RenderFillRect(SDLRenderer, ref rect);
            }

            public override void Present()
            {
                SDL.SDL_RenderPresent(SDLRenderer);
            }
        }
    }

    namespace Modules
    {
        public partial class TypeOSDL : Module
        {
            public Canvas CreateCanvas(Core.TypeO typeO, Window window)
            {
                return new TypeOSDLCanvas(typeO, window);
            }
        }
    }
}
