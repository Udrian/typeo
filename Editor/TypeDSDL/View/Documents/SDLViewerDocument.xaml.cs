using SDL2;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Interop;
using TypeD.Models.Data;
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
		System.Windows.Forms.Panel WinFormsGamePanel { get; set; }
		WindowsFormsHost WindowsFormsHostGamePanel { get; set; }

		Project Project { get; set; }
		Component Component { get; set; }

		[DllImport("user32.dll")]
		private static extern IntPtr SetParent(IntPtr child, IntPtr newParent);

		public SDLViewerDocument(Project project, Component component)
        {
            InitializeComponent();

			Project = project;
			Component = component;
			Viewer = new SDLFakeGameViewer(Project, new List<Tuple<TypeOEngine.Typedeaf.Core.Engine.Module, TypeOEngine.Typedeaf.Core.Engine.ModuleOption>>()
            {
				new Tuple<TypeOEngine.Typedeaf.Core.Engine.Module, TypeOEngine.Typedeaf.Core.Engine.ModuleOption>(
					new DesktopModule(), null),
				new Tuple<TypeOEngine.Typedeaf.Core.Engine.Module, TypeOEngine.Typedeaf.Core.Engine.ModuleOption>(
					new SDLModule(), new SDLModuleOption() { WindowFlags =  SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN | SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE})
            });
			this.Loaded += SDLViewerDocument_Loaded;
            this.SizeChanged += SDLViewerDocument_SizeChanged;
		}

        private void SDLViewerDocument_SizeChanged(object sender, SizeChangedEventArgs e)
        {
			if (WinFormsGamePanel == null || WindowsFormsHostGamePanel == null) return;
			SetWindowSize(e.NewSize);
		}

		private void SetWindowSize(Size size)
		{
			WinFormsGamePanel.Size = new System.Drawing.Size((int)size.Width, (int)size.Height);
			WindowsFormsHostGamePanel.Width = size.Width;
			WindowsFormsHostGamePanel.Height = size.Height;

			var source = PresentationSource.FromVisual(this);
			var transformToDevice = source.CompositionTarget.TransformToDevice;
			var pixelSize = (Size)transformToDevice.Transform((Vector)size);

			Viewer.SetWindowSize(new TypeOEngine.Typedeaf.Core.Common.Vec2((int)pixelSize.Width, (int)pixelSize.Height));
		}

        private void SDLViewerDocument_Loaded(object sender, RoutedEventArgs e)
        {
			if (ActualWidth == 0 || ActualHeight == 0) return;
			WinFormsGamePanel = new System.Windows.Forms.Panel();
			WindowsFormsHostGamePanel = new WindowsFormsHost() { Child = WinFormsGamePanel };
			Canvas.Children.Add(WindowsFormsHostGamePanel);

			Viewer.Start();
			Viewer.AddComponent(Project, Component);
			SetWindowSize(new Size(ActualWidth, ActualHeight));

			var windowPtr = Viewer.WindowHandler;
			SDL.SDL_SysWMinfo info = new SDL.SDL_SysWMinfo();
			SDL.SDL_GetWindowWMInfo(windowPtr, ref info);
			IntPtr winHandle = info.info.win.window;

			SetParent(winHandle, WinFormsGamePanel.Handle);
		}

		private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
			Viewer.Close();
		}
    }
}
