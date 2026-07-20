using End_Project.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using End_Project.Windows;
using System.Text.RegularExpressions;
using System.Data;
using System.Globalization;

namespace End_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для view_page.xaml
    /// </summary>
    public partial class view_page : Page
    {

        public view_page()
        {
            InitializeComponent();
            Refresh();
        }

        public void Refresh()
        {
            using (TechnicalDBEntities connect = new TechnicalDBEntities())
            {
                if (string.IsNullOrEmpty(Search_text.Text) || Search_text.Text.Equals("Search"))
                {
                    var c = connect.tech_table.Join(connect.tech_type_table,
                    a => a.tech_type_id, b => b.tech_type_id,
                    (a, b) => new { a.tech_id, a.tech_name, a.tech_photo, a.tech_price, b.tech_type_name });
                    layout_grid.ItemsSource = null;
                    layout_grid.Items.Clear();
                    layout_grid.ItemsSource = c.ToList();
                    connect.Dispose();
                }
                else
                {
                    var c = connect.tech_table.Join(connect.tech_type_table,
                    a => a.tech_type_id, b => b.tech_type_id,
                    (a, b) => new { a.tech_id, a.tech_name, a.tech_photo, a.tech_price, b.tech_type_name });
                    layout_grid.ItemsSource = null;
                    layout_grid.Items.Clear();
                    foreach (var item in c)
                    {
                        foreach (var str in Search_text.Text.Split(' '))
                        {
                            if ((Regex.IsMatch(item.tech_name, @"(\w*)" + str + @"(\w*)", RegexOptions.IgnoreCase) ||
                            Regex.IsMatch(item.tech_price, @"(\w*)" + str + @"(\w*)", RegexOptions.IgnoreCase) ||
                            Regex.IsMatch(item.tech_type_name, @"(\w*)" + str + @"(\w*)", RegexOptions.IgnoreCase)) & !string.IsNullOrEmpty(str))
                            {
                                int count = 0;
                                foreach(var n in layout_grid.Items)
                                {
                                    if (n == item)
                                    {
                                        count += 1;
                                    }
                                }
                                if (count == 0) layout_grid.Items.Add(item);
                            }
                        }
                    }
                    connect.Dispose();
                }
            }
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button_delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                using (TechnicalDBEntities connect = new TechnicalDBEntities())
                {
                    int tech_item_id = Convert.ToInt32((sender as Button).DataContext.ToString().Split(' ', ',')[3]);
                    tech_table tech = (from item in connect.tech_table where item.tech_id == tech_item_id select item).SingleOrDefault();
                    connect.tech_table.Remove(tech);
                    connect.SaveChanges();
                    Refresh();
                    connect.Dispose();
                }

            }
        }

        private void button_add_Click(object sender, RoutedEventArgs e)
        {
            add_window add = new add_window();
            if (add.ShowDialog() == true) Refresh();
        }

        private void button_edit_Click(object sender, RoutedEventArgs e)
        {
            using (TechnicalDBEntities connect = new TechnicalDBEntities())
            {
                int tech_item_id = Convert.ToInt32((sender as Button).DataContext.ToString().Split(' ', ',')[3]);
                tech_table tech = (from item in connect.tech_table where item.tech_id == tech_item_id select item).SingleOrDefault();
                edit_window edit = new edit_window(tech);
                if (edit.ShowDialog() == true) Refresh();
                connect.Dispose();
            }
        }

        private void Search_text_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search_text.Text = new string
                    (
                         Search_text.Text.Where
                         (ch =>
                            (ch >= '0' && ch <= '9')
                            || (ch >= 'a' && ch <= 'z')
                            || (ch >= 'A' && ch <= 'Z')
                            || (ch >= 'а' && ch <= 'я')
                            || (ch >= 'А' && ch <= 'Я')
                            || (ch == ' ')
                         ).ToArray()
                    );
            Search_text.SelectionStart = e.Changes.First().Offset + 1;
            Search_text.SelectionLength = 0;
            Refresh();
        }

        private void Search_text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (Search_text.Text == "Search")
            {
                Search_text.Text = null;
            }
        }
    }
}
