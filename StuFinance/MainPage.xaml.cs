using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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

namespace StuFinance
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            piechartBack.Visibility = Visibility.Hidden;
            piechartBack2.Visibility = Visibility.Hidden;
            piechartBackC.Visibility = Visibility.Hidden;
            piechartBack2C.Visibility = Visibility.Hidden;
            month.Text = DateTime.Now.ToString("MMMMMMMM", CultureInfo.CurrentCulture);
            balance();
            
        }

        private void balance()
        {
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-3HUHOSV; Initial Catalog=StuFinance; Integrated Security=True");
            connection.Open();
            string cmd = "SELECT SUM(sum_incom) FROM Incom";
            string cmd1 = "SELECT SUM(sum_cost) FROM Cost";
            string cmd2 = "SELECT SUM(sum_incom) FROM Incom WHERE cash_incom='Наличные'";
            string cmd3 = "SELECT SUM(sum_incom) FROM Incom WHERE cash_incom='Платёжная карта'";
            string cmd4 = "SELECT SUM(sum_transfer) FROM Transfer WHERE category_from='Платёжная карта'";
            string cmd5 = "SELECT SUM(sum_transfer) FROM Transfer WHERE category_from='Наличные'";
            string cmd6 = "SELECT SUM(sum_cost) FROM Cost WHERE cash_cost='Наличные'";
            string cmd7 = "SELECT SUM(sum_cost) FROM Cost WHERE cash_cost='Платёжная карта'";
            SqlCommand createCommand = new SqlCommand(cmd, connection); 
            SqlCommand createCommand1 = new SqlCommand(cmd1, connection); 
            SqlCommand createCommand2 = new SqlCommand(cmd2, connection); 
            SqlCommand createCommand3 = new SqlCommand(cmd3, connection); 
            SqlCommand createCommand4 = new SqlCommand(cmd4, connection); 
            SqlCommand createCommand5 = new SqlCommand(cmd5, connection); 
            SqlCommand createCommand6 = new SqlCommand(cmd6, connection); 
            SqlCommand createCommand7 = new SqlCommand(cmd7, connection); 
            createCommand.CommandType = CommandType.Text;
            object sum = createCommand.ExecuteScalar(); 
            object sum1 = createCommand1.ExecuteScalar(); 
            var nal = createCommand2.ExecuteScalar();
            var beznal = createCommand3.ExecuteScalar();
            var transnal = createCommand4.ExecuteScalar();
            var transbeznal = createCommand5.ExecuteScalar();
            var rasnal = createCommand6.ExecuteScalar();
            var rasbeznal = createCommand7.ExecuteScalar();
            double two, one, rez, dnrez, dbrez, rasnrez, rasbrez;
            if (sum1.ToString() == "" && sum.ToString() == "")
            {
                two = 0;
                one = 0;                
                rez = one - two;
                blnc.Text = rez.ToString();

            }
            else if (sum.ToString() == "")
            {
                two = Convert.ToDouble(sum1);
                one = 0;
                rez = one - two;
                blnc.Text = rez.ToString();
            }
            else if (sum1.ToString() == "")
            {
                two = 0;
                one = Convert.ToDouble(sum);
                rez = one - two;
                blnc.Text = rez.ToString();

            }            
            else
            {
                one = Convert.ToDouble(sum);
                two = Convert.ToDouble(sum1);
                rez = one - two;                
                string rz1 = String.Format("{0:F2}", rez);
                blnc.Text = rz1;

            }

            if (rasnal.ToString() == "")
            {
                rasnal = 0;
                rasnrez = Convert.ToDouble(rasnal);
            }
            else
            {
                rasnrez = Convert.ToDouble(rasnal);
            }

            if (rasbeznal.ToString() == "")
            {
                rasbeznal = 0;
                rasbrez = Convert.ToDouble(rasbeznal);
            }
            else
            {
                rasbrez = Convert.ToDouble(rasbeznal);
            }

            if (nal.ToString() == "" && transnal.ToString() == "")
            {
                nal = 0;
                transnal = 0;
                if (transbeznal.ToString() == "")
                {
                    transbeznal = 0;
                    dnrez = Convert.ToDouble(nal) - Convert.ToDouble(rasnrez) - Convert.ToDouble(transbeznal) + Convert.ToDouble(transnal);
                }
                dnrez = Convert.ToDouble(nal) - Convert.ToDouble(rasnrez) - Convert.ToDouble(transbeznal) + Convert.ToDouble(transnal);
            }
            else if (nal.ToString() == "")
            {
                nal = 0;
                if (transbeznal.ToString() == "")
                {
                    transbeznal = 0;
                    dnrez = Convert.ToDouble(nal) - Convert.ToDouble(rasnrez) - Convert.ToDouble(transbeznal) + Convert.ToDouble(transnal);
                }
                dnrez = Convert.ToDouble(nal) - Convert.ToDouble(rasnrez) - Convert.ToDouble(transbeznal) + Convert.ToDouble(transnal);
            }
            else if (transnal.ToString() == "")
            {
                transnal = 0;
                if (transbeznal.ToString() == "")
                {
                    transbeznal = 0;
                    dnrez = Convert.ToDouble(nal) - Convert.ToDouble(rasnrez) - Convert.ToDouble(transbeznal) + Convert.ToDouble(transnal);
                }
                dnrez = Convert.ToDouble(nal) - Convert.ToDouble(rasnrez) - Convert.ToDouble(transbeznal) + Convert.ToDouble(transnal);
            }
            else
            {
                if (transbeznal.ToString() == "")
                {
                    transbeznal = 0;
                    dnrez = Convert.ToDouble(nal) - Convert.ToDouble(rasnrez) - Convert.ToDouble(transbeznal) + Convert.ToDouble(transnal);
                }
                dnrez = Convert.ToDouble(nal) - Convert.ToDouble(rasnrez) - Convert.ToDouble(transbeznal) + Convert.ToDouble(transnal);
            }

            if (beznal.ToString() == "" && transbeznal.ToString() == "")
            {
                beznal = 0;
                transbeznal = 0;
                if (transnal.ToString() == "")
                {
                    transnal = 0;
                    dbrez = Convert.ToDouble(beznal) - Convert.ToDouble(rasbrez) - Convert.ToDouble(transnal) + Convert.ToDouble(transbeznal);
                }
                dbrez = Convert.ToDouble(beznal) - Convert.ToDouble(rasbrez) - Convert.ToDouble(transnal) + Convert.ToDouble(transbeznal);
            }
            else if (beznal.ToString() == "")
            {
                beznal = 0;
                if (transnal.ToString() == "")
                {
                    transnal = 0;
                    dbrez = Convert.ToDouble(beznal) - Convert.ToDouble(rasbrez) - Convert.ToDouble(transnal) + Convert.ToDouble(transbeznal);
                }
                dbrez = Convert.ToDouble(beznal) - Convert.ToDouble(rasbrez) - Convert.ToDouble(transnal) + Convert.ToDouble(transbeznal);
            }
            else if (transbeznal.ToString() == "")
            {
                if (transnal.ToString() == "")
                {
                    transnal = 0;
                    dbrez = Convert.ToDouble(beznal) - Convert.ToDouble(rasbrez) - Convert.ToDouble(transnal) + Convert.ToDouble(transbeznal);
                }
                transbeznal = 0;
                dbrez = Convert.ToDouble(beznal) - Convert.ToDouble(rasbrez) - Convert.ToDouble(transnal) + Convert.ToDouble(transbeznal);
            }
            else
            {
                dbrez = Convert.ToDouble(beznal) - Convert.ToDouble(rasbrez) - Convert.ToDouble(transnal) + Convert.ToDouble(transbeznal);
            }
            double dn = Math.Round(dnrez, 2);
            string s1 = String.Format("{0:F2}", dn.ToString());
            string s2 = " руб.";
            string s3 = s1 + s2;
            double db = Math.Round(dbrez, 2);
            string s4 = String.Format("{0:F2}", db.ToString());
            string s5 = s4 + s2;
            labelnal.Content = s3;
            labelbeznal.Content = s5;

        }
        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            if (Tg_Btn.IsChecked == true)
            {
                tt_receipt.Visibility = Visibility.Collapsed;
                tt_family.Visibility = Visibility.Collapsed;
                tt_transfer.Visibility = Visibility.Collapsed;


            }
            else
            {
                tt_receipt.Visibility = Visibility.Visible;
                tt_family.Visibility = Visibility.Visible;
                tt_transfer.Visibility = Visibility.Visible;


            }

        }
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        
        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime date1 = DateTime.Now;
            string current = month.Text;
            DateTime date2 = date1.AddMonths(-1);
            string dt2 = date2.ToString("MMMMMMMM", CultureInfo.CurrentCulture);
            DateTime date3 = date1.AddMonths(-2);
            string dt3 = date3.ToString("MMMMMMMM", CultureInfo.CurrentCulture);

            if (current == dt3)
            {
                month.Text = date3.AddMonths(1).ToString("MMMMMMMM", CultureInfo.CurrentCulture);
                piechartBack.Visibility = Visibility.Visible;
                piechartBackC.Visibility = Visibility.Visible;
                piechartNow.Visibility = Visibility.Hidden;
                piechartBack2.Visibility = Visibility.Hidden;
                piechartNowC.Visibility = Visibility.Hidden;
                piechartBack2C.Visibility = Visibility.Hidden;
            }
            else if (current == dt2)
            {
                month.Text = date2.AddMonths(1).ToString("MMMMMMMM", CultureInfo.CurrentCulture);
                piechartNow.Visibility = Visibility.Visible;
                piechartNowC.Visibility = Visibility.Visible;
                piechartBack.Visibility = Visibility.Hidden;
                piechartBack2.Visibility = Visibility.Hidden;
                piechartBackC.Visibility = Visibility.Hidden;
                piechartBack2C.Visibility = Visibility.Hidden;
            }
            else
            {
                month.Text = DateTime.Now.ToString("MMMMMMMM", CultureInfo.CurrentCulture);
                piechartNow.Visibility = Visibility.Visible;
                piechartNowC.Visibility = Visibility.Visible;
                piechartBack.Visibility = Visibility.Hidden;
                piechartBack2.Visibility = Visibility.Hidden;
                piechartBackC.Visibility = Visibility.Hidden;
                piechartBack2C.Visibility = Visibility.Hidden;
            }           
        }
        
        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            
            DateTime date1 = DateTime.Now;
            string current = month.Text;
            DateTime date2 = date1.AddMonths(-1);
            string dt2 = date2.ToString("MMMMMMMM", CultureInfo.CurrentCulture);
            DateTime date3 = date1.AddMonths(-2);
            string dt3 = date3.ToString("MMMMMMMM", CultureInfo.CurrentCulture);

            if (current == dt3)
            {
                month.Text = date3.ToString("MMMMMMMM", CultureInfo.CurrentCulture);
                piechartBack2.Visibility = Visibility.Visible;
                piechartBack2C.Visibility = Visibility.Visible;
                piechartNow.Visibility = Visibility.Hidden;
                piechartNowC.Visibility = Visibility.Hidden;
            }
            else if (current != dt2)
            {
                month.Text = date1.AddMonths(-1).ToString("MMMMMMMM", CultureInfo.CurrentCulture);
                piechartBack.Visibility = Visibility.Visible;
                piechartBackC.Visibility = Visibility.Visible;
                piechartNow.Visibility = Visibility.Hidden;
                piechartBack2.Visibility = Visibility.Hidden;
                piechartNowC.Visibility = Visibility.Hidden;
                piechartBack2C.Visibility = Visibility.Hidden;
            }
            else if (current == dt2)
            {
                month.Text = date2.AddMonths(-1).ToString("MMMMMMMM", CultureInfo.CurrentCulture);
                piechartBack2.Visibility = Visibility.Visible;
                piechartBack2C.Visibility = Visibility.Visible;
                piechartNow.Visibility = Visibility.Hidden;
                piechartBack.Visibility = Visibility.Hidden;
                piechartNowC.Visibility = Visibility.Hidden;
                piechartBackC.Visibility = Visibility.Hidden;
            }
        }

        private void ListViewItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new GalleryReceipt());
        }

        private void ListViewItem_PreviewMouseDown_1(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new AddFamily());
        }

        private void ListViewItem_PreviewMouseDown_2(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Transaction());
        }

        private void MinusBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Incom1());
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Costs1());
        }

        private void svern_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
