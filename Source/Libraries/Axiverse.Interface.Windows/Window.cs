﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX.Windows;

namespace Axiverse.Interface.Windows
{
    public class Window : Control
    {
        // translate font to device 
        // graphics, bitmap, paths, etc.

        private Vector2 MouseLocation;
        private Control Hover;
        private Control Click;
        private Control SelectedControl;

        public Window()
        {

        }

        public void Select(Control control)
        {
            Requires.That(control.Window == this);

            if (SelectedControl != control)
            {
                SelectedControl?.SetSelection(false);
                control.Selected = true;
            }
        }

        public void Bind(RenderForm form)
        {
            form.MouseMove += (sender, e) => OnMouseMove(e.X, e.Y);
            form.MouseWheel += (sender, e) => OnMouseScroll(e.Delta);
            form.MouseDown += (sender, e) => OnMouseDown(new MouseEventArgs(e.X, e.Y, (MouseButtons)((int)e.Button >> 20)));
            form.MouseUp += (sender, e) => OnMouseUp(new MouseEventArgs(e.X, e.Y, (MouseButtons)((int)e.Button >> 20)));
            form.Resize += (sender, e) => Size = new Vector2(form.Width, form.Height);
            
            Width = form.Width;
            Height = form.Height;
        }

        public void InvokeMouse()
        {

        }

        public void InvokeKeyboard()
        {

        }

        protected void OnMouseMove(float x, float y)
        {
            var point = new Vector2(x, y);
            var delta = point - MouseLocation;
            MouseLocation = point;

            if (Click == null)
            {
                Hover?.OnMouseMove(Hover, new MouseMoveEventArgs
                {
                    DeltaX = delta.X,
                    DeltaY = delta.Y,
                    X = x,
                    Y = y,
                });

                var hover = FindControl(MouseLocation);

                if (hover != Hover)
                {
                    Hover?.OnMouseLeave(Hover, null);
                    hover?.OnMouseEnter(hover, null);
                    Hover = hover;
                }
            }
            else
            {
                Click.OnMouseMove(Hover, new MouseMoveEventArgs
                {
                    DeltaX = delta.X,
                    DeltaY = delta.Y,
                    X = x,
                    Y = y,
                });
            }
        }

        protected void OnMouseScroll(float z)
        {
            Hover?.OnMouseWheel(Hover, new MouseMoveEventArgs
            {
                DeltaZ = z
            });
        }

        protected void OnMouseDown(MouseEventArgs eventArgs)
        {
            Hover?.OnMouseDown(Hover, eventArgs);

            if (Hover != null)
            {
                Click = Hover;
            }
        }

        protected void OnMouseUp(MouseEventArgs eventArgs)
        {
            Click?.OnMouseUp(Click, eventArgs);

            // continue hover mechanics.
            var hover = FindControl(MouseLocation);

            if (hover != Hover)
            {
                Hover?.OnMouseLeave(Hover, null);
                hover?.OnMouseEnter(hover, null);
                Hover = hover;
            }
        }

        public override void Draw(DrawContext compositor)
        {
            // intentionally blank.
        }
    }
}