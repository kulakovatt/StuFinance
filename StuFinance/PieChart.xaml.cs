using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для PieChart.xaml
    /// </summary>
    public partial class PieChart : UserControl
    {
        public PieChart()
        {
            InitializeComponent();

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-3HUHOSV; Initial Catalog=StuFinance; Integrated Security=True");
            connection.Open();
            string cmd = "SELECT SUM(sum_cost) FROM Cost WHERE type_cost='Транспорт' AND cash_cost='Наличные' AND MONTH(data_cost) = MONTH(getDate())";
            string cmd1 = "SELECT SUM(sum_cost) FROM Cost WHERE type_cost='Питание' AND cash_cost='Наличные' AND MONTH(data_cost) = MONTH(getDate())";
            string cmd2 = "SELECT SUM(sum_cost) FROM Cost WHERE type_cost='Одежда' AND cash_cost='Наличные' AND MONTH(data_cost) = MONTH(getDate())";
            string cmd3 = "SELECT SUM(sum_cost) FROM Cost WHERE type_cost='Здоровье' AND cash_cost='Наличные' AND MONTH(data_cost) = MONTH(getDate())";
            string cmd4 = "SELECT SUM(sum_cost) FROM Cost WHERE type_cost='Развлечения' AND cash_cost='Наличные' AND MONTH(data_cost) = MONTH(getDate())";
            string cmd5 = "SELECT SUM(sum_cost) FROM Cost WHERE type_cost='Жилье' AND cash_cost='Наличные' AND MONTH(data_cost) = MONTH(getDate())";
            string cmd6 = "SELECT SUM(sum_transfer) FROM Transfer WHERE category_from='Наличные' AND MONTH(data_transfer) = MONTH(getDate())";
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            SqlCommand createCommand1 = new SqlCommand(cmd1, connection);
            SqlCommand createCommand2 = new SqlCommand(cmd2, connection);
            SqlCommand createCommand3 = new SqlCommand(cmd3, connection);
            SqlCommand createCommand4 = new SqlCommand(cmd4, connection);
            SqlCommand createCommand5 = new SqlCommand(cmd5, connection);
            SqlCommand createCommand6 = new SqlCommand(cmd6, connection);
            createCommand.CommandType = CommandType.Text;
            object sum = createCommand.ExecuteScalar();
            object sum1 = createCommand1.ExecuteScalar();
            object sum2 = createCommand2.ExecuteScalar();
            object sum3 = createCommand3.ExecuteScalar();
            object sum4 = createCommand4.ExecuteScalar();
            object sum5 = createCommand5.ExecuteScalar();
            object sum6 = createCommand6.ExecuteScalar();
            double trn, food, cloth, health, ent, lodg, trans;
            if (sum.ToString() == "")
            {
                trn = 0;
            }
            else
            {
                trn = Convert.ToDouble(sum);
            }

            if (sum1.ToString() == "")
            {
                food = 0;
            }
            else
            {
                food = Convert.ToDouble(sum1);
            }

            if (sum2.ToString() == "")
            {
                cloth = 0;
            }
            else
            {
                cloth = Convert.ToDouble(sum2);
            }

            if (sum3.ToString() == "")
            {
                health = 0;
            }
            else
            {
                health = Convert.ToDouble(sum3);
            }

            if (sum4.ToString() == "")
            {
                ent = 0;
            }
            else
            {
                ent = Convert.ToDouble(sum4);
            }

            if (sum5.ToString() == "")
            {
                lodg = 0;
            }
            else
            {
                lodg = Convert.ToDouble(sum5);
            }

            if (sum6.ToString() == "")
            {
                trans = 0;
            }
            else
            {
                trans = Convert.ToDouble(sum6);
            }


            myPieChart.Series.Add(new PieSeries { Title = "Транспорт", Fill = Brushes.RoyalBlue, StrokeThickness = 0, Values = new ChartValues<double> { 0 + trn } });
            myPieChart.Series.Add(new PieSeries { Title = "Питание", Fill = Brushes.Crimson, StrokeThickness = 0, Values = new ChartValues<double> { 0 + food } });
            myPieChart.Series.Add(new PieSeries { Title = "Одежда", Fill = Brushes.Yellow, StrokeThickness = 0, Values = new ChartValues<double> { 0 + cloth } });
            myPieChart.Series.Add(new PieSeries { Title = "Здоровье", Fill = Brushes.Chartreuse, StrokeThickness = 0, Values = new ChartValues<double> { 0 + health } });
            myPieChart.Series.Add(new PieSeries { Title = "Развлечения", Fill = Brushes.MediumTurquoise, StrokeThickness = 0, Values = new ChartValues<double> { 0 + ent } });
            myPieChart.Series.Add(new PieSeries { Title = "Жильё", Fill = Brushes.OrangeRed, StrokeThickness = 0, Values = new ChartValues<double> { 0 + lodg } });
            myPieChart.Series.Add(new PieSeries { Title = "Перевод средств", Fill = Brushes.DarkOrchid, StrokeThickness = 0, Values = new ChartValues<double> { 0 + trans } });


            DataContext = this;
        }
        
    }
}
