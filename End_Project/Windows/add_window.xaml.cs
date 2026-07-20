using End_Project.DB;
using End_Project.Pages;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
using System.Xml.Linq;

namespace End_Project.Windows
{
    /// <summary>
    /// Логика взаимодействия для add_window.xaml
    /// </summary>
    public partial class add_window : Window
    {
        tech_table item = new tech_table();

        public add_window()
        {
            InitializeComponent();
            using (TechnicalDBEntities connect = new TechnicalDBEntities())
            {
                connect.tech_type_table.Load();
                tech_type_combo_box.ItemsSource = connect.tech_type_table.Local;
                connect.Dispose();
            }
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(name_text.Text) || string.IsNullOrEmpty(price_text.Text) ||
                tech_type_combo_box.SelectedIndex == -1)
            {
                MessageBox.Show("fill in all the fields", "", MessageBoxButton.OK);
            }
            else
            {
                using (TechnicalDBEntities connect = new TechnicalDBEntities())
                {
                    if (connect.tech_table.Where(d => d.tech_name == name_text.Text && d.tech_price == price_text.Text).FirstOrDefault() != null)
                    {
                        MessageBox.Show("The equipment is already there", "", MessageBoxButton.OK);
                        return;
                    }
                    item.tech_name = name_text.Text;
                    item.tech_price = price_text.Text;
                    item.tech_type_id = Convert.ToInt32(((tech_type_table)tech_type_combo_box.SelectedItem).tech_type_id);
                    connect.tech_table.Add(item);
                    connect.SaveChanges();
                    this.DialogResult = true;
                    connect.Dispose();
                }
            }
        }

        private void button_photo_add_Click(object sender, RoutedEventArgs e)
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
