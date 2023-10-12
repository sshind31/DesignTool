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

namespace DesignTool
{
    partial class Sketch
    {
        //public int i = 1;
        //This code is only to render items until it get 
        protected override void DrawOverlay(Design.DrawSceneParams myParams)
        {
            //RenderSnapPoints();
            //to set line size
            RenderContext.SetLineSize(2);
            //to enable renderer to 
            RenderContext.EnableXOR(false);
            //default ask
            RenderContext.SetState(depthStencilStateType.DepthTestOff);

            if (EnabledFeaturse.Count > 0 && !ActiveViewport.ToolBar.Contains(current))
            {
                if (Points.Count == 1 && EnabledFeaturse.Contains("Line"))
                {
                    ScreenToPlane(current, Plane.XY, out MouseLocation3D);
                    Point2D temp1 = WorldToScreen(Points[0]);
                    Point2D temp2 = WorldToScreen(MouseLocation3D);
                    Debug.WriteLine(temp1+" +=+ "+temp2);
                    RenderContext.DrawLine(temp1, temp2);
                    //i++;
                }
                if (Points.Count == 1 && EnabledFeaturse.Contains("Circle"))
                {
                    ScreenToPlane(current, Plane.XY, out MouseLocation3D);
                    double radius = Points[0].DistanceTo(MouseLocation3D);
                    Debug.WriteLine(MouseLocation3D+": Radiii :"+radius);
                    if (radius>0.001)
                    {
                        Debug.WriteLine("Circle");
                        Circle circle = new Circle(SketchPlane, Points[0], radius);
                        Point2D[] ListOfPointsOnCircle = new Point3D[20];
                        for (int i = 0; i < 20; i++)
                        {
                            ListOfPointsOnCircle[i] = WorldToScreen(circle.PointAt(circle.Domain.ParameterAt((double)i / 20)));
                            //This loop will give 100 points on which circle lies
                            Debug.WriteLine(ListOfPointsOnCircle[i]);
                        }
                        RenderContext.DrawLineStrip(ListOfPointsOnCircle);
                    } 
                }
            }

            Point3D[] ArrOfSnapPoints = GetPointsToSnap();
            if (ArrOfSnapPoints != null)
            {
                foreach(Point3D p in ArrOfSnapPoints)
                {
                    /*Circle circle = new Circle(SketchPlane, p, 3);
                    Point2D[] ListOfPointsOnCircle = new Point3D[100];
                    for (int i = 0; i < 100; i++)
                    {
                        ListOfPointsOnCircle[i] = WorldToScreen(circle.PointAt(circle.Domain.ParameterAt((double)i / 100)));
                        //This loop will give 100 points on which circle lies
                    }
                    RenderContext.DrawLineStrip(ListOfPointsOnCircle);*/
                    Point2D[] ArrOfSquareOfSP = new Point3D[5];
                    double Xmax = p.X + 2;
                    double Xmin = p.X - 2;
                    double Ymax = p.Y + 2;
                    double Ymin = p.Y - 2;
                    ArrOfSquareOfSP[0] = ArrOfSquareOfSP[4] = WorldToScreen(new Point3D(Xmin,Ymax));
                    ArrOfSquareOfSP[1] = WorldToScreen(new Point3D(Xmin, Ymin));
                    ArrOfSquareOfSP[2] = WorldToScreen(new Point3D(Xmax, Ymin));
                    ArrOfSquareOfSP[3] = WorldToScreen(new Point3D(Xmax, Ymax));
                    RenderContext.DrawLineStrip(ArrOfSquareOfSP);
                }
            }
            RenderContext.EnableXOR(true);
            base.DrawOverlay(myParams);
        }
    }
}
