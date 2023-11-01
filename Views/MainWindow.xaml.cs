using Administration;
using EventManager.Data;
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

namespace EventManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MyDBContext context = new MyDBContext();
        
        public MainWindow()
        {
            Initializer.DbSetInitializer(context);

            InitializeComponent();

            //dataGrid vullen met events uit database
            dataGrid.Items.Clear();
            var events = context.Events.ToList();
            dataGrid.ItemsSource = events;
        }
    }
}
