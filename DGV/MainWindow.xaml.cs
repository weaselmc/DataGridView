using DGV.AdventureWorks2012CustomerDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace DGV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CustomerDataProvider cdp = new CustomerDataProvider();

        public MainWindow()
        {
            InitializeComponent();            
            DataContext = cdp.GetCustomers();
            foreach(var s in cdp.getStoreIds())
                StoreIdsCombo.Items.Add(s);
        }

        private void StoreIdsCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var storeid = (string)StoreIdsCombo.SelectedItem;
            DataContext = cdp.GetCustomersForStore(storeid);
        }
    }
}
