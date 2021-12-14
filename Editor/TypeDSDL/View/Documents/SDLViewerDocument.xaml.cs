using SDL2;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Interop;
using TypeD.Models.Data;
using TypeD.Viewer;
using TypeDSDL.Viewer;
using TypeOEngine.Typedeaf.Desktop;
using TypeOEngine.Typedeaf.SDL;

namespace TypeDSDL.View.Documents
{
    /// <summary>
    /// Interaction logic for SDLViewer.xaml
    /// </summary>
    public partial class SDLViewerDocument : UserControl
    {
		SDLFakeGameViewer Viewer { get; set; }

		// These are the variables you care about.
		private System.Windows.Forms.Panel WinFormsGamePanel { get; set; }
		private WindowsFormsHost WindowsFormsHostGamePanel { get; set; }

		// IGNORE MEEEEE
		private IntPtr glContext;
		private delegate void Viewport(int x, int y, int width, int height);
		private Viewport glViewport;

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

		public SDLViewerDocument(Project project, TypeOType typeOType)
        {
            InitializeComponent();

            if (typeOType.TypeOBaseType == "Drawable2d")
            {
				Viewer = new SDLFakeGameViewer(project, typeOType, new List<TypeOEngine.Typedeaf.Core.Engine.Module>()
                {
                    new DesktopModule(),
                    new SDLModule()
                });
				this.Loaded += SDLViewerDocument_Loaded;
			}
		}

        private void SDLViewerDocument_Loaded(object sender, RoutedEventArgs e)
        {
			// This is what we're going to attach the SDL2 window to
			WinFormsGamePanel = new System.Windows.Forms.Panel();
			var ah = this.ActualHeight;
			WinFormsGamePanel.Size = new System.Drawing.Size((int)ActualWidth, (int)ActualHeight);

			// Make the WinForms window
			WindowsFormsHostGamePanel = new WindowsFormsHost() { Child = WinFormsGamePanel, Width = WinFormsGamePanel.Size.Width, Height = WinFormsGamePanel.Size.Height };
			Canvas.Children.Add(WindowsFormsHostGamePanel);

			// IGNORE MEEEEE
			var windowPtr = Viewer.WindowHandler;
			/*glContext = SDL.SDL_GL_CreateContext(windowPtr);
			glViewport = (Viewport)Marshal.GetDelegateForFunctionPointer(
				SDL.SDL_GL_GetProcAddress("glViewport"),
				typeof(Viewport)
			);
			glViewport(0, 0, WinFormsGamePanel.Size.Width, WinFormsGamePanel.Size.Height);*/

			// Get the Win32 HWND from the SDL2 window
			SDL.SDL_SysWMinfo info = new SDL.SDL_SysWMinfo();
			SDL.SDL_GetWindowWMInfo(windowPtr, ref info);
			IntPtr winHandle = info.info.win.window;

			// Move the SDL2 window to 0, 0
			SetWindowPos(
				winHandle,
				new WindowInteropHelper(Application.Current.MainWindow).Handle,
				0,
				0,
				0,
				0,
				0x0401 // NOSIZE | SHOWWINDOW
			);

			// Attach the SDL2 window to the panel
			SetParent(winHandle, WinFormsGamePanel.Handle);
			ShowWindow(winHandle, 1); // SHOWNORMAL
		}

		private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
			glViewport = null;
			SDL.SDL_GL_DeleteContext(glContext);
			glContext = IntPtr.Zero;
			Viewer.Close();
		}
    }
}
