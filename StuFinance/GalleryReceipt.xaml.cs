using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
    /// Логика взаимодействия для GalleryReceipt.xaml
    /// </summary>
    public partial class GalleryReceipt : Page
    {
        public GalleryReceipt()
        {
            InitializeComponent();
            bindcombo();            

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-3HUHOSV; Initial Catalog=StuFinance; Integrated Security=True");

            connection.Open();
            string cmd = "Select image_receipt from Receipt";
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            createCommand.CommandType = CommandType.Text;
            SqlDataReader rd;
            rd = createCommand.ExecuteReader();
            while (rd.Read())
            {
                listimage.Items.Add(rd[0].ToString());
            }
            connection.Close();
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

        private void open_photo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Image files (*.BMP, *.JPG, *.GIF, *.TIF, *.PNG, *.ICO, *.EMF, *.WMF)|*.bmp;*.jpg;*.gif; *.tif; *.png; *.ico; *.emf; *.wmf";

            if (openDialog.ShowDialog() == true)
            {
                img.Source = new BitmapImage(new Uri(openDialog.FileName));
                txtpath.Text = openDialog.FileName;
            }
        }

        private void save_photo_Click(object sender, RoutedEventArgs e)
        {
            
            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-3HUHOSV; Initial Catalog=StuFinance; Integrated Security=True");

            connection.Open();
            string cmd = "Insert into Receipt (id,image_receipt) Values (@ID,@IMG)";
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            createCommand.CommandType = CommandType.Text;
            createCommand.Parameters.AddWithValue("@ID", combo1.SelectedValue.ToString());
            createCommand.Parameters.AddWithValue("@IMG", txtpath.Text);        
            try
            {
                if (txtpath.Text != "")
                {
                    listimage.Items.Add(txtpath.Text);
                    createCommand.ExecuteNonQuery();
                    MessageBox.Show("Ваш чек успешно сохранен!");
                }
                else 
                {
                    MessageBox.Show("Выберите фотографию.");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка, при выполнении запроса на добавление записи");
                return;
            }
            connection.Close();

            using (var context = new StuModel())
            {
                context.SaveChanges();

            }
        }

        private void delete_photo_Click(object sender, RoutedEventArgs e)
        {
            

            SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-3HUHOSV; Initial Catalog=StuFinance; Integrated Security=True");

            connection.Open();
            string cmd = "DELETE FROM Receipt WHERE image_receipt = @recpt";           
            SqlCommand createCommand = new SqlCommand(cmd, connection);
            createCommand.CommandType = CommandType.Text;
            createCommand.Parameters.AddWithValue("@recpt", txtpath.Text);
            try
            {
                if (txtpath.Text != "")
                {
                    createCommand.ExecuteNonQuery();
                    listimage.Items.Remove(txtpath.Text);
                    MessageBox.Show("Фото удалено!");
                    txtpath.Text = "";
                    img.Visibility = Visibility.Hidden;
                }
                else 
                {
                    MessageBox.Show("Выберите фотографию.");
                }
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
                
            }
            
        }

        private void view_Click(object sender, RoutedEventArgs e)
        {
            if (listimage.SelectedItem != null)
            {
                img.Visibility = Visibility.Visible;
                txtpath.Text = listimage.SelectedItem.ToString();
                img.Source = new BitmapImage(new Uri(txtpath.Text));
            }
        }
    }
}
