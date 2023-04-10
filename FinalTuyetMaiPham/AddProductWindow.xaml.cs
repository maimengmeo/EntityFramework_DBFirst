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
using System.Windows.Shapes;

namespace FinalTuyetMaiPham
{
    /// <summary>
    /// Interaction logic for AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        List<Category> categories;
        public AddProductWindow(List<Category> categories)
        {
            InitializeComponent();
            this.categories = categories;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Invalid Product Name. Try again!");
            }
            else if (cmbCategory.SelectedItem == null)
            {
                MessageBox.Show("Please Select Product Category!");
            }
            else
            {
                string productName = txtName.Text;
                decimal price;
                int category = Convert.ToInt32(cmbCategory.SelectedValue);

                if (decimal.TryParse(txtPrice.Text, out price))
                {
                    using (var context = new NorthwindEntities())
                    {
                        Product newProduct = new Product();
                        newProduct.ProductName = productName;
                        newProduct.UnitPrice = price;
                        newProduct.CategoryID = category;

                        context.Products.Add(newProduct);
                        context.SaveChanges();

                        MessageBox.Show("New Product is added!");

                        txtName.Text = "";
                        txtName.Focus();
                        txtPrice.Text = "";
                    }


                }
                else
                {
                    MessageBox.Show("Invalid Price. Try again!");
                }
            }
            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cmbCategory.ItemsSource = this.categories;
            cmbCategory.DisplayMemberPath = "CategoryName";
            cmbCategory.SelectedValuePath = "CategoryID";
        }
    }
}
