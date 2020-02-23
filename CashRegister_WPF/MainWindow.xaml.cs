using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;


namespace CashRegister_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets price for item and money tentered by customer
        /// </summary>
        /// <returns>Tuple with price as first item and payed as second item</returns>
        public Tuple<int, int> GetPriceAndPayed()
        {
            int price = 0;
            int payed = 0;

            if (Int32.TryParse(tbxPrice.Text, out price) && Int32.TryParse(tbxPayed.Text, out payed))
            {
                if (price > payed)
                {
                    MessageBox.Show("Kunden betalade för lite. Snåljåp.", "Fel");
                }
            }
            else
            {
                MessageBox.Show("Kunde inte kovertera Pris eller Betalat till heltal.", "Fel");
            }

            return new Tuple<int, int>(price, payed);
        }

        /// <summary>
        /// Calculate change denominations when button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCalcChange_Click(object sender, RoutedEventArgs e)
        {
            CashRegister cashRegister = new CashRegister();

            Tuple<int, int> priceAndPayed = GetPriceAndPayed();

            List<int> change = cashRegister.MakeChange(priceAndPayed.Item1, priceAndPayed.Item2);

            DisplayChange(change);
        }

        /// <summary>
        /// Display the calculated change
        /// </summary>
        /// <param name="change"></param>
        private void DisplayChange(List<int> change)
        {
            lbl1000.Content = change.Where(item => item == 1000).Count();
            lbl500.Content = change.Where(item => item == 500).Count();
            lbl200.Content = change.Where(item => item == 200).Count();
            lbl100.Content = change.Where(item => item == 100).Count();
            lbl50.Content = change.Where(item => item == 50).Count();
            lbl20.Content = change.Where(item => item == 20).Count();
            lbl10.Content = change.Where(item => item == 10).Count();
            lbl5.Content = change.Where(item => item == 5).Count();
            lbl2.Content = change.Where(item => item == 2).Count();
            lbl1.Content = change.Where(item => item == 1).Count();
        }
    }

    class CashRegister
    {
        private readonly int[] drawer = new int[] { 1000, 500, 200, 100, 50, 20, 10, 5, 2, 1 };

        /// <summary>
        /// Calculate change based on price and money paid
        /// </summary>
        /// <param name="price"></param>
        /// <param name="payed"></param>
        /// <returns></returns>
        public List<int> MakeChange(int price, int payed)
        {
            int difference = payed - price;

            List<int> change = new List<int> { };
            int i = 0;
            int denomination = drawer[i];

            while (difference > 0)
            {
                if (difference < denomination)
                {
                    i += 1;
                    denomination = drawer[i];
                    continue;
                }

                change.Add(denomination);
                difference -= denomination;
            }

            return change;
        }
    }
}
