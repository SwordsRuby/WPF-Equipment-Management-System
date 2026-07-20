using End_Project.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace End_Project.Windows
{
    /// <summary>
    /// Логика взаимодействия для edit_window.xaml
    /// </summary>
    public partial class edit_window : Window
    {
        tech_table item;

        public edit_window(tech_table _item)
        {
            item = _item;
            InitializeComponent();
            tech_type_table tech_type_item;
            using (TechnicalDBEntities connect = new TechnicalDBEntities())
            {
                connect.tech_type_table.Load();
                tech_type_combo_box.ItemsSource = connect.tech_type_table.ToList();
                tech_type_item = (from tech_type in connect.tech_type_table where tech_type.tech_type_id == item.tech_type_id select tech_type).SingleOrDefault();
                connect.Dispose();
            }
            this.DataContext = item;
            tech_type_combo_box.SelectedItem = tech_type_item;
            name_text.Text = item.tech_name;
            price_text.Text = item.tech_price;
        }

        private void button_photo_add_Click(object sender, RoutedEventArgs e)
        {
            using (TechnicalDBEntities connect = new TechnicalDBEntities()) 
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|GIF Files (*.gif)|*.gif|WEBP Files (*.webp)|*.webp"
                };
                if (openFileDialog.ShowDialog().GetValueOrDefault())
                {
                    item.tech_photo = File.ReadAllBytes(openFileDialog.FileName);
                    MemoryStream byteStream = new MemoryStream(item.tech_photo);
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = byteStream;
                    image.EndInit();
                    IPicture.Source = image;
                }

            };
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(name_text.Text) || string.IsNullOrEmpty(price_text.Text) || tech_type_combo_box.SelectedIndex == -1)
            {
                MessageBox.Show("fill in all the fields", "", MessageBoxButton.OK);
            }
            else
            {
                using (TechnicalDBEntities connect = new TechnicalDBEntities())
                {
                    var tech_item = connect.tech_table.Where(z => z.tech_id == item.tech_id).FirstOrDefault();
                    if (item.tech_photo != null)
                    {
                        tech_item.tech_photo = item.tech_photo;
                    }
                    tech_item.tech_name = name_text.Text;
                    tech_item.tech_price = price_text.Text;
                    tech_item.tech_type_id = Convert.ToInt32(((tech_type_table)tech_type_combo_box.SelectedItem).tech_type_id);
                    connect.SaveChanges();
                    this.DialogResult = true;
                    connect.Dispose();
                }
            }
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void price_text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
