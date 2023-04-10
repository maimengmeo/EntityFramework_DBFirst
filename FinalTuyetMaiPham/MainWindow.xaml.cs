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

namespace FinalTuyetMaiPham
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Category> categories;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetAllProd_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new NorthwindEntities())
            {
                var products = context.Products.ToList();
                grdProd.ItemsSource = products;

                categories = context.Categories.ToList();

                cmbCategories.ItemsSource = categories;
                cmbCategories.DisplayMemberPath = "CategoryName";
                cmbCategories.SelectedValuePath = "CategoryID";
            }
        }

        private void btnClearData_Click(object sender, RoutedEventArgs e)
        {

            txtProdName.Text = string.Empty;
            grdProd.ItemsSource = null;

        }

        private void cmbCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selectedCategory = Convert.ToInt32(cmbCategories.SelectedValue);

            using (var context = new NorthwindEntities())
            {
                var prodInCategory = (from product in context.Products
                                      where product.CategoryID == selectedCategory
                                      select product).ToList();

                grdProd.ItemsSource = prodInCategory;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            using (var context = new NorthwindEntities())
            {
                var products = context.Products
                                        .Where(p => p.ProductName.Contains(txtProdName.Text))
                                        .ToList();

                grdProd.ItemsSource = products;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow(categories);
            addProductWindow.Owner = this;
            addProductWindow.ShowDialog();
        }
    }
}
