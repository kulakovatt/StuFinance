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
    /// Логика взаимодействия для Transaction.xaml
    /// </summary>
    public partial class Transaction : Page
    {
        int max = 11;
        int min = 0;

        public Transaction()
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

        private void esc_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
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

        private void add_Click(object sender, RoutedEventArgs e)
        {

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-3HUHOSV; Initial Catalog=StuFinance; Integrated Security=True");

            connection.Open();
            string cmd = "Insert into Transfer (category_from,category_before,id,sum_transfer,data_transfer) Values (@FROM,@BEFORE,@ID,@SUM,@DATA)";
            string cmd2 = "SELECT SUM(sum_incom) FROM Incom WHERE cash_incom='Наличные'";
            string cmd3 = "SELECT SUM(sum_incom) FROM Incom WHERE cash_incom='Платёжная карта'";
            string cmd6 = "SELECT SUM(sum_cost) FROM Cost WHERE cash_cost='Наличные'";
            string cmd7 = "SELECT SUM(sum_cost) FROM Cost WHERE cash_cost='Платёжная карта'";
            string cmd4 = "SELECT SUM(sum_transfer) FROM Transfer WHERE category_from='Платёжная карта'";
            string cmd5 = "SELECT SUM(sum_transfer) FROM Transfer WHERE category_from='Наличные'";
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            SqlCommand createCommand1 = new SqlCommand(cmd2, connection);
            SqlCommand createCommand2 = new SqlCommand(cmd3, connection);
            SqlCommand createCommand3 = new SqlCommand(cmd4, connection);
            SqlCommand createCommand4 = new SqlCommand(cmd5, connection);
            SqlCommand createCommand5 = new SqlCommand(cmd6, connection);
            SqlCommand createCommand6 = new SqlCommand(cmd7, connection);
            createCommand.CommandType = CommandType.Text;
            createCommand.Parameters.AddWithValue("@ID", combo1.SelectedValue.ToString());
            createCommand.Parameters.AddWithValue("@FROM", from.Text);
            createCommand.Parameters.AddWithValue("@BEFORE", before.Text);
            string txt = txt_display.Text.Replace(".", ",");
            double pp = Convert.ToDouble(txt);
            string rz = String.Format("{0:F2}", pp);
            string rezult = rz.Replace(",", ".");
            var nal = createCommand1.ExecuteScalar();
            var beznal = createCommand2.ExecuteScalar();
            var transnal = createCommand3.ExecuteScalar();
            var transbeznal = createCommand4.ExecuteScalar();
            var rasnal = createCommand5.ExecuteScalar();
            var rasbeznal = createCommand6.ExecuteScalar();
            double dnrez, dbrez, rasnrez, rasbrez;
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
                MessageBox.Show("Введите сумму перевода.");
            }
            else
            {                
                createCommand.Parameters.AddWithValue("@SUM", rezult);
            }
            
            if ((from.Text == "Наличные" && from.Text == before.Text) || (from.Text == "Платёжная карта" && from.Text == before.Text))
            {
                MessageBox.Show("Некорректный маршрут перевода.");
            }
            else
            {
                if ((from.Text == "Наличные" && dnrez > pp) || (from.Text == "Платёжная карта" && dbrez > pp))
                {
                    try
                    {
                        createCommand.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка, при выполнении запроса на добавление расхода");
                        return;
                    }
                    connection.Close();

                    using (var context = new StuModel())
                    {
                        context.SaveChanges();
                        MessageBox.Show($"Добавлен перевод!");

                    }
                }
                else
                {
                    MessageBox.Show("Недостаточно средств на балансе для перевода");
                }
            }
            
        }
    }
}
