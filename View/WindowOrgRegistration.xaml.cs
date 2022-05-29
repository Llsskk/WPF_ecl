﻿using System;
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

namespace ecl.View
{
    /// <summary>
    /// Логика взаимодействия для WindowOrgRegistration.xaml
    /// </summary>
    public partial class WindowOrgRegistration : Window
    {
        public WindowOrgRegistration()
        {
            InitializeComponent();
        }
        private void btOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btConcel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}