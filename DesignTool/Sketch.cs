using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesignTool
{
    partial class Sketch: devDept.Eyeshot.Design
    {
        public Plane SketchPlane = Plane.XY;

        public List<Point3D> Points=new List<Point3D>();

        private Point3D MouseLocation3D;

        public System.Drawing.Point current;

        public List<string> EnabledFeaturse = new List<string>();

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (EnabledFeaturse.Count>0)
            {
                ScreenToPlane(current, SketchPlane, out MouseLocation3D);
                Points.Add(MouseLocation3D);
            }
            if (EnabledFeaturse.Contains("Line") && Points.Count == 2)
            {
                Line line = new Line(Points[0], Points[1]);
                Entities.Add(line);
                Debug.WriteLine(Points[0]+""+Points[1]);
                Points.Clear();
                EnabledFeaturse.Remove("Line");
            }
            else if (EnabledFeaturse.Contains("Circle") && Points.Count == 2)
            {
                Circle circle = new Circle(SketchPlane, Points[0], Points[0].DistanceTo(Points[1]));
                Entities.Add(circle);
                Points.Clear();
                EnabledFeaturse.Remove("Circle");
            }

            Entities.Regen();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            // save the current mouse position
            current = RenderContextUtility.ConvertPoint(GetMousePosition(e));

            // paint the viewport surface
            PaintBackBuffer();// This calls the overlay method of design class

            // consolidates the drawing
            SwapBuffers();

            base.OnMouseMove(e);
        }
    }
}
