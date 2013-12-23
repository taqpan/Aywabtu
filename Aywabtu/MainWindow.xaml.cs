using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;


namespace Aywabtu {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
            grid.Focus();
            Keyboard.Focus(grid);
            if (grid.Items.Count > 0) {
                grid.SelectedIndex = 0;
            }
        }
    }
}
