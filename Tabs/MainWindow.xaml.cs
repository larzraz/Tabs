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

namespace Tabs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private List<TabItem> _tabItems;
        private TabItem _tabAdd;
        public MainWindow()
        {

            
                InitializeComponent();
                _tabItems = new List<TabItem>();
                _tabAdd = new TabItem();
                _tabAdd.Header = "+";
                _tabItems.Add(_tabAdd);               
                this.AddTabItem();            
                tabDynamic.DataContext = _tabItems;
                tabDynamic.SelectedIndex = 0;
        }
        private TabItem AddTabItem()
        {
            int count = _tabItems.Count;
            // create new tab item
            TabItem tab = new TabItem();

            tab.Header = string.Format("Tilbud {0}", count);
            tab.Name = string.Format("tab{0}", count);
            tab.HeaderTemplate = tabDynamic.FindResource("TabHeader") as DataTemplate;
            tab.MouseDoubleClick += new MouseButtonEventHandler(tab_MouseDoubleClick);
            // add controls to tab item, this case I added just a textbox
            DataGrid dG = new DataGrid();
            TextBox txt = new TextBox();
            txt.Name = "txt";
            
            tab.Content = dG;
            // insert tab item right before the last (+) tab item
            _tabItems.Insert(count - 1, tab);
            return tab;
        }

        private void tabAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // clear tab control binding
            tabDynamic.DataContext = null;
            TabItem tab = this.AddTabItem();
            // bind tab control
            tabDynamic.DataContext = _tabItems;
            // select newly added tab item
            tabDynamic.SelectedItem = tab;
        }

        private void tab_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TabItem tab = sender as TabItem;

            TabProperty dlg = new TabProperty();

            // get existing header text
            dlg.txtTitle.Text = tab.Header.ToString();

            if (dlg.ShowDialog() == true)
            {
                // change header text
                tab.Header = dlg.txtTitle.Text.Trim();
            }
        }

        private void tabDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem tab = tabDynamic.SelectedItem as TabItem;
            if (tab == null) return;

            if (tab.Equals(_tabAdd))
            {
                // clear tab control binding
                tabDynamic.DataContext = null;
                TabItem newTab = this.AddTabItem();
                // bind tab control
                tabDynamic.DataContext = _tabItems;
                // select newly added tab item
                tabDynamic.SelectedItem = newTab;
            }
            else
            {
                // your code here...
            }
        }



    }
}
