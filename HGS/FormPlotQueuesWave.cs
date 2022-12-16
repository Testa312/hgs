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
    public partial class FormPlotQueuesWave : Form
    {
        point Point;
        public FormPlotQueuesWave(point pt)
        {
            InitializeComponent();
            Point = pt;
            plotView1.Model =  PlotPoint();
        }
        private PlotModel PlotPoint()
        {
            if (Point == null || Point.Wd3s_Queues_Array == null) return null;
            //
            int j = 0;
            for (; j < Point.Wd3s_Queues_Array.Length; j++)
            {
                if (Point.Wd3s_Queues_Array[j].Data() == null)
                {
                    j--;
                    break;
                }
            }
            if (j < 0) return null;
            var pm = new PlotModel
            {
                Title = string.Format("{0}-{1}",Point.pn ,Point.ed),
                PlotType = PlotType.XY,
                Background = OxyColors.White
            };
            double maxdv = double.MinValue;
            double mindv = double.MaxValue;
            for (int m = 0; m < j; m++)
            {
                foreach (double dv in Point.Wd3s_Queues_Array[m].Data())
                {
                    maxdv = Math.Max(maxdv, dv);
                    mindv = Math.Min(mindv, dv);
                }
            }
            pm.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });

            double margindv = (maxdv- mindv) * 0.2;

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
            foreach (DetectorWave wd in Point.Wd3s_Queues_Array)
            {
                var lineSeries = new LineSeries
                {
                    Title = string.Format("{0}s", (1<<i++) * 96),
                    //DataFieldX = "Date",
                    //DataFieldY = "Value",
                    //ItemsSource = dq.Data(),
                    TrackerFormatString = "{0}\r{2}\r{4:0.00}",
                };
                
                var data = wd.Data();
                if (data == null) continue;
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
