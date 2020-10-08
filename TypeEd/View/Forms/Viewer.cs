using SDL2;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TypeEd.View.Forms
{
    public partial class Viewer : UserControl
    {
		private IntPtr gameWindow;
		private IntPtr glContext;
		private delegate void Viewport(int x, int y, int width, int height);
		private delegate void ClearColor(float r, float g, float b, float a);
		private delegate void Clear(uint flags);
		private Viewport glViewport;
		private ClearColor glClearColor;
		private Clear glClear;

		public Viewer()
        {
			// Make the WinForms window
			Size = new Size(800, 600);

			SDL.SDL_Init(SDL.SDL_INIT_VIDEO);
			gameWindow = SDL.SDL_CreateWindow(
				string.Empty,
				0,
				0,
				this.Size.Width,
				this.Size.Height,
				SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS | SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL
			);
			glContext = SDL.SDL_GL_CreateContext(gameWindow);
			glViewport = (Viewport)Marshal.GetDelegateForFunctionPointer(
				SDL.SDL_GL_GetProcAddress("glViewport"),
				typeof(Viewport)
			);
			glClearColor = (ClearColor)Marshal.GetDelegateForFunctionPointer(
				SDL.SDL_GL_GetProcAddress("glClearColor"),
				typeof(ClearColor)
			);
			glClear = (Clear)Marshal.GetDelegateForFunctionPointer(
				SDL.SDL_GL_GetProcAddress("glClear"),
				typeof(Clear)
			);
			glViewport(0, 0, this.Size.Width, this.Size.Height);
			glClearColor(1.0f, 0.0f, 0.0f, 1.0f);
			glClear(0x4000);
			SDL.SDL_GL_SwapWindow(gameWindow);

			// Get the Win32 HWND from the SDL2 window
			SDL.SDL_SysWMinfo info = new SDL.SDL_SysWMinfo();
			SDL.SDL_GetWindowWMInfo(gameWindow, ref info);
			IntPtr winHandle = info.info.win.window;

			// Move the SDL2 window to 0, 0
			SetWindowPos(
				winHandle,
				Handle,
				0,
				0,
				0,
				0,
				0x0401 // NOSIZE | SHOWWINDOW
			);

			// Attach the SDL2 window to the panel
			SetParent(winHandle, this.Handle);
			ShowWindow(winHandle, 1); // SHOWNORMAL

			InitializeComponent();
        }

		[DllImport("user32.dll")]
		private static extern IntPtr SetWindowPos(
		IntPtr handle,
		IntPtr handleAfter,
		int x,
		int y,
		int cx,
		int cy,
		uint flags
	);
		[DllImport("user32.dll")]
		private static extern IntPtr SetParent(IntPtr child, IntPtr newParent);
		[DllImport("user32.dll")]
		private static extern IntPtr ShowWindow(IntPtr handle, int command);
	}
}
