﻿using System;
using System.Collections.Generic;
using TypeD.Models.Data;
using TypeOEngine.Typedeaf.Core.Entities;
using TypeOEngine.Typedeaf.Core.Entities.Interfaces;
using TypeOEngine.Typedeaf.Core.Interfaces;

namespace TypeDCore.Code.Entity
{
    partial class EntityCode : ComponentTypeCode
    {
        // Properties
        public override Type TypeOBaseType { get { return typeof(Entity2d); } }
        public bool Updatable { get; private set; }
        public bool Drawable { get; private set; }

        // Constructors
        public EntityCode(string className, string @namespace, Component parentComponentType, bool updatable, bool drawable) : base(className, @namespace, parentComponentType)
        {
            Updatable = updatable;
            Drawable = drawable;
            Drawables = new List<string>();
        }
        protected override void InitClass()
        {
            if(IsBaseComponentType)
            {
                AddFunction(new Function("protected virtual void InternalInitialize()", () => { }));
                AddFunction(new Function("public override void Cleanup()", () => { }));
            }
            else
            {
                AddFunction(new Function("protected override void InternalInitialize()", () => {
                    Writer.AddLine("base.InternalInitialize();");
                }));
                AddFunction(new Function("public override void Cleanup()", () => {
                    Writer.AddLine("base.Cleanup();");
                }));
            }

            if (Updatable && (ParentComponent == null || !ParentComponent.Interfaces.Contains(typeof(IUpdatable))))
            {
                AddFunction(new Function("public virtual void Update(double dt)", () => { }));
            }
            else if(Updatable)
            {
                AddFunction(new Function("public override void Update(double dt)", () => {
                    Writer.AddLine("base.Update(dt);");
                }));
            }

            if (Drawable && (ParentComponent == null || !ParentComponent.Interfaces.Contains(typeof(IDrawable))))
            {
                AddUsing("TypeOEngine.Typedeaf.Core.Engine.Graphics");
                AddFunction(new Function("public virtual void Draw(Canvas canvas)", () => { }));
            }
            else if(Drawable)
            {
                AddUsing("TypeOEngine.Typedeaf.Core.Engine.Graphics");
                AddFunction(new Function("public override void Draw(Canvas canvas)", () => {
                    Writer.AddLine("base.Draw(canvas);");
                }));
            }
        }
    }
}
