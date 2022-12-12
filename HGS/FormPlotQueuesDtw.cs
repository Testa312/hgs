using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Globalization;
namespace HGS
{
    public partial class FormPlotQueuesDtw : Form
    {
        point Point;
        public FormPlotQueuesDtw(point pt)
        {
            InitializeComponent();
            Point = pt;
            plotView1.Model =  PlotPoint();
        }
        private PlotModel PlotPoint()
        {
            if (Point == null || Point.Dtw_Queues_Array == null) return null;
            var pm = new PlotModel
            {
                Title = string.Format("{0}-{1}",Point.pn ,Point.ed),
                PlotType = PlotType.XY,
                Background = OxyColors.White
            };
            double maxdv = double.MinValue;
            double mindv = double.MaxValue;

            foreach (double dv in Point.Dtw_Queues_Array[5].Data())
            {
                maxdv = Math.Max(maxdv, dv);
                mindv = Math.Min(mindv, dv);
            }
            pm.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });

            double margindv = (maxdv- mindv)*0.2;

            pm.Axes.Add(new LinearAxis()
            {
                //Title = "值",
                Position = AxisPosition.Left,
                IsPanEnabled = false,
                IsZoomEnabled = false,
                Minimum = mindv - margindv,
                //Minimum = 0,
                Maximum = maxdv + margindv,
                Key = "yaxis_dv",
            });
            int i = 0;
            foreach (Dtw_queues dq in Point.Dtw_Queues_Array)
            {
                var lineSeries = new LineSeries
                {
                    Title = string.Format("{0}m", (1<<i) * 15),
                    //DataFieldX = "Date",
                    //DataFieldY = "Value",
                    //ItemsSource = dq.Data(),
                    TrackerFormatString = "{0}\r{2}\r{4:0.00}",
                };
                
                var data = dq.Data();
                for (int m = 0; m < data.Length; m++)
                {
                    lineSeries.Points.Add(new DataPoint(m,data[m]));
                }
               
                pm.Series.Add(lineSeries);
            }
            return pm;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            plotView1.Model = PlotPoint();
        }
    }
    
}
