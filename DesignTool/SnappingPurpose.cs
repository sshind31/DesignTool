using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignTool
{
    partial class Sketch
    {
        public bool SnapStatus=false;

        public Point3D[] GetPointsToSnap()
        {
            int[] indexOfDrawingOnCursor;
            if (SnapStatus)
            {
                indexOfDrawingOnCursor= GetCrossingEntities(new Rectangle(current.X - 5, current.Y - 5, 10, 10), Entities, true, true,null);
                if (indexOfDrawingOnCursor.Length > 0)
                {
                    Entity ent = Entities[indexOfDrawingOnCursor[0]];
                    if (ent is Line)
                    {
                        Debug.WriteLine("Got a line");
                        Line line = (Line)ent;
                        Debug.WriteLine(line.StartPoint + " = " + line.MidPoint + " = " + line.EndPoint);
                        /*Debug.WriteLine(WorldToScreen(line.StartPoint)+"="+ WorldToScreen(line.MidPoint)+"="+ WorldToScreen(line.EndPoint));*/
                        return new Point3D[] {line.StartPoint,line.MidPoint,line.EndPoint };
                    }
                    else if(ent is Circle)
                    {
                        Circle circle = (Circle)ent;
                        return new Point3D[] { circle.Center };
                    } 
                }
                Debug.WriteLine("null");
            }
            return null;
        }

        /*public void RenderSnapPoints()
        {
            Point3D[] ListOfPointstoSnap = GetPointsToSnap();
            
            if (ListOfPointstoSnap != null)
            {
                *//*Point2D[] ArrOf2Dpoints = Array.ConvertAll(ListOfPointstoSnap, item => WorldToScreen(item));*//*
                
                foreach(Point3D p in ListOfPointstoSnap)
                {
                    RenderGivenPoint(new System.Drawing.Point((int)p.X, (int)p.Y));
                }
            }
        }*/

        public void RenderGivenPoint(System.Drawing.Point PointToShow)
        {
            RenderContext.SetLineSize(2);

            RenderContext.EnableXOR(false);
            RenderContext.SetColorWireframe(Color.FromArgb(0, 0, 255));
            RenderContext.SetState(depthStencilStateType.DepthTestOff);
            RenderContext.SetColorWireframe(Color.Red);

            double XLe = PointToShow.X - 2;
            double XRi = PointToShow.X + 2;
            double YUp = PointToShow.Y + 2;
            double YDo = PointToShow.Y - 2;

            Point3D LTop = new Point3D(XLe, YUp);
            Point3D LBottom = new Point3D(XLe,YDo);
            Point3D RTop = new Point3D(XRi,YUp);
            Point3D RBottom = new Point3D(XRi, YDo);
            Debug.WriteLine("New");
            Debug.WriteLine(RTop+" = "+RBottom+" = "+LTop+" = "+LBottom);
            RenderContext.DrawLineStrip(new Point3D[]
            {
                RTop,RBottom,LBottom,LTop
            });
        }
    }
}
 