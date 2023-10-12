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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace DesignTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        protected override void OnContentRendered(EventArgs e)
        {
            // Fits the drawing in the viewport
            SketchBoard.ZoomFit();
            base.OnContentRendered(e);
        }

        private void UpdateStatus(object sender, MouseEventArgs e)
        {
            if (SketchBoard.EnabledFeaturse.Count > 0)
            {
                status.Foreground = new SolidColorBrush(Colors.Green);
                status.Text = "ON";
            }
            else
            {
                status.Foreground = new SolidColorBrush(Colors.Red);
                status.Text = "OFF";
            }
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {
            SketchBoard.Points.Clear();
            SketchBoard.EnabledFeaturse.Clear();
            SketchBoard.SetView(viewType.Top, false, true);
            SketchBoard.EnabledFeaturse.Add("Line");
        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {
            SketchBoard.Points.Clear();
            SketchBoard.EnabledFeaturse.Clear();
            SketchBoard.SetView(viewType.Top, false, true);
            SketchBoard.EnabledFeaturse.Add("Circle");
        }

        private void Escape_Click(object sender, RoutedEventArgs e)
        {
            SketchBoard.Points.Clear();
            SketchBoard.EnabledFeaturse.Clear();
        }

        private void ActivateSnap(object sender, RoutedEventArgs e)
        {
            SketchBoard.SnapStatus = true;
        }

        private void DectivateSnap(object sender, RoutedEventArgs e)
        {
            SketchBoard.SnapStatus = false;
        }
    }
}
