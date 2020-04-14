using System.Collections.Generic;
using TypeOEngine.Typedeaf.Core.Engine;

namespace TypeOEngine.Typedeaf.SDL
{
    public class SDLModuleOption : ModuleOption
    {
        public Dictionary<string, string> SDLHints { get; set; } = new Dictionary<string, string>() { { SDL2.SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1" } };
        public uint SDLInitFlags { get; set; } = SDL2.SDL.SDL_INIT_VIDEO;
        public SDL2.SDL_image.IMG_InitFlags IMGInitFlags { get; set; } = SDL2.SDL_image.IMG_InitFlags.IMG_INIT_PNG;
        public SDL2.SDL.SDL_WindowFlags WindowFlags { get; set; } = SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN;
        public SDL2.SDL.SDL_RendererFlags RenderFlags { get; set; } = SDL2.SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL2.SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC;
    }
}
