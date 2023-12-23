using Microsoft.Win32;
using pop4.Entities;
using System;
using System.Collections.Generic;
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

namespace pop4.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Page
    {
        private Product _curProduct = new Product();
        private byte[] img = null;
        public AddEdit(Product curProduct)
        {
            InitializeComponent();
            if (_curProduct != null)
            {
                _curProduct = curProduct;
            }
            FillComboBox();
            DataContext = _curProduct;
        }

        private void FillComboBox()
        {
            foreach (var item in App.db.Unit.ToList())
            {
                CB_unit.Items.Add(item.name);
            }
            foreach (var item in App.db.Manufacturer.ToList())
            {
                CB_manufacturer.Items.Add(item.name);
            }
            foreach (var item in App.db.Supplier.ToList())
            {
                CB_supplier.Items.Add(item.name);
            }
            foreach (var item in App.db.Category.ToList())
            {
                CB_category.Items.Add(item.name);
            }
            if (_curProduct == null)
{
return;
}
            if (_curProduct.article != null)
            {
                var _curUnit = App.db.Unit.Where(p => p.Id_u == _curProduct.unit_of_measurement).First();
                CB_unit.SelectedItem = _curUnit.name;
                var _curManufacturer = App.db.Manufacturer.Where(p => p.Id_m == _curProduct.manufacturer).First();
                CB_manufacturer.SelectedItem = _curManufacturer.name;
                var _curSupplier = App.db.Supplier.Where(p => p.Id_s == _curProduct.supplier).First();
                CB_supplier.SelectedItem = _curSupplier.name;
                var _curCategory = App.db.Category.Where(p => p.Id_c == _curProduct.category).First();
                CB_category.SelectedItem = _curCategory.name;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BT_save_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder error = new StringBuilder();

            if (_curProduct == null)
            {
                return;
            }

            _curProduct.unit_of_measurement = CB_unit.SelectedIndex + 1;
            _curProduct.manufacturer = CB_manufacturer.SelectedIndex + 1;
            _curProduct.supplier = CB_supplier.SelectedIndex + 1;
            _curProduct.category = CB_category.SelectedIndex + 1;

            decimal cost = 0;
            int max_discount = 0;
            int current_discount = 0;
            int quantity_in_stock = 0;
            if (string.IsNullOrWhiteSpace(_curProduct.name))
                error.AppendLine("Введите название");
            if (CB_unit.SelectedItem == null)
                error.AppendLine("Введите еденицу измерения");
            if (decimal.TryParse(TB_coust.Text, out cost) == false || cost <= 0)
                error.AppendLine("Введите корректное значение цены");
            if (int.TryParse(TB_max_discount.Text, out max_discount) == false || max_discount <= 0)
                error.AppendLine("Введите корректное значение максимальной скидки");
            if (CB_manufacturer.SelectedItem == null)
                error.AppendLine("Введите производителя");
            if (CB_supplier.SelectedItem == null)
                error.AppendLine("Введите поставщика");
            if (CB_category.SelectedItem == null)
                error.AppendLine("Введите категорию");
            if (int.TryParse(TB_current_discount.Text, out current_discount) == false || current_discount <= 0)
                error.AppendLine("Введите корректное значение действующей скидки");
            if (int.TryParse(TB_quantity_in_stock.Text, out quantity_in_stock) == false || quantity_in_stock < 0)
                error.AppendLine("Введите корректное значение количества на складе");
            if (string.IsNullOrWhiteSpace(_curProduct.description))
                error.AppendLine("Введите описание");
            if (error.Length > 0)
            {
                MessageBox.Show(error.ToString());
                return;
            }
            if (_curProduct.article == null)
            {
                App.db.Product.Add(_curProduct);
            }

            try
            {
                App.db.SaveChanges();
                MessageBox.Show("Информация сохранена!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            NavigationService.GoBack();
        }

        private void BT_change_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "Image | *.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == null)
            {
                img = File.ReadAllBytes(ofd.FileName);
                Image_photo.Source = new ImageSourceConverter().ConvertFrom(img) as ImageSource;
                _curProduct.photo = img;
            }
            
        }

        private void BT_exit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
