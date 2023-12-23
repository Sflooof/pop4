using pop4.Entities;
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
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Page
    {
        private int NumberOfPage = 0;
        private int maxItemShow = 30;
        public Catalog()
        {
            InitializeComponent();
            ListView_Catalog.ItemsSource = App.db.Product.ToList();
            if (App.CurrentUser == null || App.CurrentUser.role == 1)
            {
                BT_add.Visibility = Visibility.Hidden;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void CB_sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void CB_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            var products = App.db.Product.ToList();
            products = products.Where(p => p.name.ToLower().Contains(TB_find.Text.ToLower())).ToList();
            if (CB_sort.SelectedIndex == 0)
            {
                products = products.OrderByDescending(p => p.coust).ToList();
            }
            else if (CB_sort.SelectedIndex == 1)
            {
                products = products.OrderBy(p => p.coust).ToList();
            }
            if (CB_filter.SelectedIndex == 1)
                products = products.Where(p => p.current_discount >= 0 && p.current_discount < 10).ToList();
            if (CB_filter.SelectedIndex == 2)
                products = products.Where(p => p.current_discount >= 10 && p.current_discount < 15).ToList();
            if (CB_filter.SelectedIndex == 3)
                products = products.Where(p => p.current_discount >= 15).ToList();
            ListView_Catalog.ItemsSource = products;
            ListView_Catalog.ItemsSource = products.Skip(maxItemShow * NumberOfPage).Take(maxItemShow).ToList();
            int count_find = ListView_Catalog.Items.Count;
            Tb_count_find.Text = count_find.ToString() + " из " + App.db.Product.Count().ToString();
            if (count_find < 1)
                Tb_count_find.Text += "\n по вашему запросу ничего не найдено";
        }

        private void BT_exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BT_add_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEdit(null));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                App.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                ListView_Catalog.ItemsSource = App.db.Category.ToList();
            }
        }

        private void TB_find_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void BT_edit_Click(object sender, RoutedEventArgs e)
        {
            var edit = (sender as Button).DataContext as Product;
            NavigationService.Navigate(new AddEdit(edit));
        }

        private void BT_deldete_Click(object sender, RoutedEventArgs e)
        {
            var delete = (sender as Button).DataContext as Entities.Product;
            if (MessageBox.Show("Вы действительно хотите удалить этот товар ?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                App.db.Product.Remove(delete);
                App.db.SaveChanges();
                Update();
            }
        }
    }
}
