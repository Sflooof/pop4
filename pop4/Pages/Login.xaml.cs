using System;
using System.Collections.Generic;
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

namespace pop4.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
            App.CurrentUser = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.Catalog());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var login_user = App.db.User.FirstOrDefault(x => x.login == TB_login.Text && x.password == PB_password.Password);
            if (TB_login.Text == "" || PB_password.Password == "")
            {
                MessageBox.Show("Не все поля заполнены", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (login_user != null)
            {
                App.CurrentUser = login_user;
                NavigationService.Navigate(new Pages.Catalog());
            }
            else
            {
                MessageBox.Show("Пользователя не существует", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
