using End_Project.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace End_Project.Pages
{
    /// <summary>
    /// Логика взаимодействия для authorization_page.xaml
    /// </summary>
    public partial class authorization_page : Page
    {

        public authorization_page()
        {
            InitializeComponent();
            login_text.Text = "Login";
            password_text.Text = "Password";
            //var sw = new Stopwatch();
            //sw.Start();
            //sw.Stop();
            //MessageBox.Show(sw.Elapsed.ToString());
        }

        private void button_enter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(login_text.Text) || string.IsNullOrEmpty(password_text.Text))
                {
                    MessageBox.Show("Please fill in all fields", "", MessageBoxButton.OK);
                }
                else
                {
                    using (TechnicalDBEntities connect = new TechnicalDBEntities()) {
                        if (connect.admin_table.Where(item => item.admin_login == login_text.Text & item.admin_password == password_text.Text).FirstOrDefault() != null)
                        {
                            connect.Dispose();
                            NavigationService.Navigate(new view_page());
                        }
                        else
                        {
                            MessageBox.Show("Incorrect password or login", "", MessageBoxButton.OK);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "", MessageBoxButton.OK);
            }
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void login_text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.Text, 0) & login_text.GetLineLength(0) > 4)
            {
                login_text.Text = null;
            }
        }

        private void password_text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
            else if (char.IsDigit(e.Text, 0) & password_text.GetLineLength(0) > 4)
            {
                password_text.Text = null;
            }
        }
    }
}
