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

namespace StuFinance
{
    /// <summary>
    /// Логика взаимодействия для Incom.xaml
    /// </summary>
    public partial class Incom1 : Page
    {
        int max = 11;
        int min = 0;
       
        public Incom1()
        {
            InitializeComponent();
            bindcombo();

        }
        public List<Family> Fml { get; set; }
        private void bindcombo()
        {
            StuModel dc = new StuModel();
            var item = dc.Families.ToList();
            Fml = item;
            DataContext = Fml;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var button = sender as Button;
            if (!(txt_display.Text.Length > max || (txt_display.Text == "" && button.Content.ToString() == "0")))
                txt_display.Text += button.Content;

        }

        private void ButtonBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (txt_display.Text.Length > min)
            txt_display.Text = txt_display.Text.Remove(txt_display.Text.Length - 1);
        }

        private void ButtonCE_Click(object sender, RoutedEventArgs e)
        {
            txt_display.Text = "";
        }

        private void esc_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-3HUHOSV; Initial Catalog=StuFinance; Integrated Security=True");

            connection.Open();
            string cmd = "Insert into Incom (id,data_record,type_incom,sum_incom,cash_incom) Values (@ID,@DATA,@TYPE,@SUM,@CASH)";
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            createCommand.CommandType = CommandType.Text;
            createCommand.Parameters.AddWithValue("@ID", combo1.SelectedValue.ToString());
            createCommand.Parameters.AddWithValue("@CASH", combo1_Copy.Text);
            if (datapicker.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату дохода.");
            }
            else
            {
                DateTime dt = (DateTime)datapicker.SelectedDate;
                createCommand.Parameters.AddWithValue("@DATA", dt.ToString("MM.dd.yyyy", System.Globalization.CultureInfo.InvariantCulture));
            }

            if (txt_display.Text == "")
            {
                MessageBox.Show("Введите сумму дохода.");
            }
            else
            {
                string txt = txt_display.Text.Replace(".", ",");
                double pp = Convert.ToDouble(txt);
                string rz = String.Format("{0:F2}", pp);
                string rezult = rz.Replace(",", ".");
                createCommand.Parameters.AddWithValue("@SUM", rezult);
            }
            string type = "";
            if (depozit_rdb.IsChecked == true)
            {
                type = "Депозиты";
                createCommand.Parameters.AddWithValue("@TYPE", type);
            }
            else if (sber_rdb.IsChecked == true)
            {
                type = "Сбережения";
                createCommand.Parameters.AddWithValue("@TYPE", type);
            }
            else if (zp_rdb.IsChecked == true)
            {
                type = "ЗП";
                createCommand.Parameters.AddWithValue("@TYPE", type);
            }
            else
            {
                MessageBox.Show("Выберите категорию дохода.");
            }
            try
            {
                createCommand.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("Ошибка, при выполнении запроса на добавление дохода");
                return;
            }
            connection.Close();

            using (var context = new StuModel())
            {
                context.SaveChanges();
                MessageBox.Show($"Добавлен доход!");

            }
        }
    }
}
