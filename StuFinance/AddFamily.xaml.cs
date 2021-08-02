using System;
using System.Collections.Generic;
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
using System.Configuration;
using System.Data;
using System.Data.Entity;

namespace StuFinance
{
    /// <summary>
    /// Логика взаимодействия для AddFamily.xaml
    /// </summary>
    public partial class AddFamily : Page
    {
        
        public AddFamily()
        {
            InitializeComponent();
            StuModel c = new StuModel();
            DGridFamily.ItemsSource = c.Families.ToList();
        }
        public List<Family> Fml { get; set; }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
           
            using (var context = new StuModel())
            {
                if (addPerson.Text == "")
                {
                    MessageBox.Show("Введите имя.");
                }
                else if (addPerson.Text.Any(c => char.IsLetter(c)))
                {
                    var mem = new Family()
                    {
                       FIO = addPerson.Text
                    };
                    // добавляем их в бд
                    context.Families.Add(mem);
                    context.SaveChanges();
                    MessageBox.Show($"Добавлен член семьи! id: {mem.id_member}, Имя: {mem.FIO}");
                    DGridFamily.ItemsSource = context.Families.ToList(); // Сам вывод 
                    DGridFamily.Items.Refresh();
                }
                else
                {
                    MessageBox.Show("Недопустимые символы.");
                }
            }

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-3HUHOSV; Initial Catalog=StuFinance; Integrated Security=True");
            
            connection.Open();
            string cmd = "DELETE FROM Family WHERE id_member = @idmem"; 
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            createCommand.CommandType = CommandType.Text;
            var cellInfo = DGridFamily.SelectedCells[0];
            var content = (cellInfo.Column.GetCellContent(cellInfo.Item) as TextBlock).Text;
            createCommand.Parameters.AddWithValue("@idmem", content.ToString());
            try
            {
                createCommand.ExecuteNonQuery();
                MessageBox.Show($"Удаление прошло успешно.");
            }
            catch
            {
                MessageBox.Show("Ошибка, при выполнении запроса на удаление записи");
                return;
            }
            connection.Close();
            using (var context = new StuModel())
            {

                context.SaveChanges();
                DGridFamily.ItemsSource = context.Families.ToList(); // Сам вывод 
                DGridFamily.Items.Refresh();
            }

        }

        private void esc_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
        }
    }
}
