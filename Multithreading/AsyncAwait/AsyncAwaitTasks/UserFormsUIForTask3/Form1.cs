using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserFormsUIForTask3
{
    public partial class Form1 : Form
    {
        private List<Product> products = new List<Product>();
        private List<Product> bascet = new List<Product>();

        public Form1()
        {
            InitializeComponent();
            Initialize();
        }

        private void Initialize()
        {
            products.Add(new Product { Name = "Bananas", Price = 347 });
            products.Add(new Product { Name = "Coconuts", Price = 525 });
            products.Add(new Product { Name = "Oranges", Price = 422 });
            products.Add(new Product { Name = "Lemons", Price = 661 });

            this.FillProductElements(products[0], ProductName1, BascetProductName1, ProductPrice1);
            this.FillProductElements(products[1], ProductName2, BascetProductName2, ProductPrice2);
            this.FillProductElements(products[2], ProductName3, BascetProductName3, ProductPrice3);
            this.FillProductElements(products[3], ProductName4, BascetProductName4, ProductPrice4);
        }

        private void FillProductElements(Product product, Label productName, Label bascetProductName, Label price)
        {
            productName.Text = product.Name;
            price.Text = product.Price.ToString();
            bascetProductName.Text = product.Name;
        }

        private async void AddProductButton1_Click(object sender, EventArgs e)
        {
            await AddProductToBascet(this.products[0]);
            this.ChangeProductElementsVisibility(true, BascetProductName1, ProductCount1, RemoveProduct1);
            AddProductButton1.Enabled = false;
        }

        private async void AddProductButton2_Click(object sender, EventArgs e)
        {
            await AddProductToBascet(this.products[1]);
            this.ChangeProductElementsVisibility(true, BascetProductName2, ProductCount2, RemoveProduct2);
            AddProductButton2.Enabled = false;
        }

        private async void AddProductButton3_Click(object sender, EventArgs e)
        {
            await AddProductToBascet(this.products[2]);
            this.ChangeProductElementsVisibility(true, BascetProductName3, ProductCount3, RemoveProduct3);
            AddProductButton3.Enabled = false;
        }

        private async void AddProductButton4_Click(object sender, EventArgs e)
        {
            await AddProductToBascet(this.products[3]);
            this.ChangeProductElementsVisibility(true, BascetProductName4, ProductCount4, RemoveProduct4);
            AddProductButton4.Enabled = false;
        }

        private Task AddProductToBascet(Product product)
        {
            return Task.Factory.StartNew(() =>
            {
                this.bascet.Add(product);
            });
        }

        private void ChangeProductElementsVisibility(bool isVisible, params Control[] controls)
        {
            foreach (var control in controls)
            {
                control.Visible = isVisible;
            }
        }

        private async void ProductCount1_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;
            int productCount = (int)numericUpDown.Value;
            products[0].CurrentCount = productCount;

            int total = await this.CalculateTotal();
            TotalLabel.Text = total.ToString();
        }

        private async void ProductCount2_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;
            int productCount = (int)numericUpDown.Value;
            products[1].CurrentCount = productCount;

            int total = await this.CalculateTotal();
            TotalLabel.Text = total.ToString();
        }

        private async void ProductCount3_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;
            int productCount = (int)numericUpDown.Value;
            products[2].CurrentCount = productCount;

            int total = await this.CalculateTotal();
            TotalLabel.Text = total.ToString();
        }

        private async void ProductCount4_ValueChanged(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;
            int productCount = (int)numericUpDown.Value;
            products[3].CurrentCount = productCount;

            int total = await this.CalculateTotal();
            TotalLabel.Text = total.ToString();
        }

        private Task<int> CalculateTotal()
        {
            return Task.Factory.StartNew(() =>
            {
                int total = this.bascet.Select(product => product.CurrentCount * product.Price).Sum();
                return total;
            });
        }

        private async void RemoveProduct1_Click(object sender, EventArgs e)
        {
            this.bascet.Remove(this.products[0]);
            int total = await this.CalculateTotal();
            TotalLabel.Text = total.ToString();
            this.ChangeProductElementsVisibility(false, BascetProductName1, ProductCount1, RemoveProduct1);
            ProductCount1.Value = 0;
            AddProductButton1.Enabled = true;
        }

        private async void RemoveProduct2_Click(object sender, EventArgs e)
        {
            this.bascet.Remove(this.products[1]);
            int total = await this.CalculateTotal();
            TotalLabel.Text = total.ToString();
            this.ChangeProductElementsVisibility(false, BascetProductName2, ProductCount2, RemoveProduct2);
            ProductCount2.Value = 0;
            AddProductButton2.Enabled = true;
        }

        private async void RemoveProduct3_Click(object sender, EventArgs e)
        {
            this.bascet.Remove(this.products[2]);
            int total = await this.CalculateTotal();
            TotalLabel.Text = total.ToString();
            this.ChangeProductElementsVisibility(false, BascetProductName3, ProductCount3, RemoveProduct3);
            ProductCount3.Value = 0;
            AddProductButton3.Enabled = true;
        }

        private async void RemoveProduct4_Click(object sender, EventArgs e)
        {
            this.bascet.Remove(this.products[3]);
            int total = await this.CalculateTotal();
            TotalLabel.Text = total.ToString();
            this.ChangeProductElementsVisibility(false, BascetProductName4, ProductCount4, RemoveProduct4);
            ProductCount4.Value = 0;
            AddProductButton4.Enabled = true;
        }
    }
}
