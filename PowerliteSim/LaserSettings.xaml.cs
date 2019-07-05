using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

namespace PowerliteSim
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LaserSettings : Window
    {

        public LaserSettings()
        {
            InitializeComponent();
            
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        RS232Settings settings;
        

        public RS232Settings Settings
        {
            get { return settings; }
            set
            {
                settings = value;
                propertyGrid.SelectedObject = settings;
            }
        }

        private void rectangle1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
            //Application.Current.MainWindow.DragMove();
        }
    }
}
