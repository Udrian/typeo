﻿using TypeOEngine.Typedeaf.Core.Common;
using TypeOEngine.Typedeaf.Core.Engine.Services.Interfaces;

namespace TypeOEngine.Typedeaf.Desktop
{
    namespace Engine.Services.Interfaces
    {
        public interface IMouseInputService : IService
        {
            public Vec2 MousePosition { get; }
            public Vec2 MousePositionRelative { get; }

            public Vec2 WheelPosition { get; }
            public Vec2 WheelPositionRelative { get; }

            public void SetKeyAlias(object input, object key);

            public bool IsDown(object input);
            public bool IsPressed(object input);
            public bool IsReleased(object input);
        }
    }
}
