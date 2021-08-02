using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace StuFinance
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            myPieChart.Series.Add(new PieSeries { Title = "BAD", Fill = Brushes.Red, StrokeThickness = 0, Values = new ChartValues<double> { 0.0 } });
            myPieChart.Series.Add(new PieSeries { Title = "GOOD", Fill = Brushes.Green, StrokeThickness = 0, Values = new ChartValues<double> { 100.0 } });

            DataContext = this;
        }
        internal void RefreshData(double badPct)
        {
            myPieChart.Series[0].Values[0] = badPct;
            myPieChart.Series[1].Values[0] = 100.0 - badPct;
        }

    }
}
