﻿using Alternet.UI;
using Alternet.Drawing;
using System;

namespace DrawingSample
{
    internal abstract class DrawingPage
    {
        private Control? settingsControl;

        private Control? canvas;

        public Control? Canvas
        {
            get => canvas;

            set
            {
                if (canvas == value)
                    return;

                if (canvas != null)
                    canvas.Paint -= Canvas_Paint;

                var oldValue = canvas;
                canvas = value;

                if (canvas != null)
                    canvas.Paint += Canvas_Paint;

                OnCanvasChanged(oldValue, value);
            }
        }

        protected virtual void OnCanvasChanged(Control? oldValue, Control? value)
        {
        }

        public Control SettingsControl => settingsControl ??= CreateSettingsControl();

        public abstract string Name { get; }

        public abstract void Draw(DrawingContext dc, Rect bounds);

        protected abstract Control CreateSettingsControl();

        public virtual void OnActivated()
        {
        }

        public virtual void OnDeactivated()
        {
        }

        private void Canvas_Paint(object? sender, PaintEventArgs e)
        {
            //e.DrawingContext.FillRectangle(e.Bounds, Color.White);
            var b = e.Bounds;
//            b.Width -= 1;
            //b.Height -= 30;
            e.DrawingContext.DrawRectangle(Pens.Gray, b);
            Draw(e.DrawingContext, e.Bounds);
        }
    }
}