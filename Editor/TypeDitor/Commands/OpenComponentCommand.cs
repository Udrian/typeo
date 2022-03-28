﻿using TypeD.Commands;
using TypeD.Models.Data.Hooks;
using TypeD.Models.Interfaces;
using TypeDitor.Commands.Data;
using TypeDitor.ViewModel;
using TypeDSDL.View.Documents;

namespace TypeDitor.Commands
{
    class OpenComponentCommand : CustomCommand
    {
        // ViewModel
        MainWindowViewModel MainWindowViewModel { get; set; }

        // Models
        IHookModel HookModel { get; set; }

        public OpenComponentCommand(MainWindowViewModel mainWindowViewModel, IHookModel hookModel)
        {
            MainWindowViewModel = mainWindowViewModel;
            HookModel = hookModel;
        }

        public override void Execute(object parameter)
        {
            var data = parameter as OpenComponentCommandData;

            HookModel.Shoot(new OpenComponentHook() { Project = data.Project, Component = data.Component });
            HookModel.Shoot(new ComponentFocusHook() { Project = data.Project, Component = data.Component });

            //TODO: Fix this, TypeDitor shouldnt have access to SDL
            //MainWindowViewModel.OpenDocument($"{data.Component.ClassName}.{data.Component.TypeOBaseType}", new SDLViewerDocument(data.Project, data.Component));
            //MainWindowViewModel.OpenDocument($"{data.Component.ClassName}.{data.Component.TypeOBaseType}", new ConsoleViewerDocument(data.Project, data.Component));
        }
    }
}
