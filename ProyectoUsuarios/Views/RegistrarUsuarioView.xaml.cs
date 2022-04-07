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

namespace ProyectoUsuarios.Views
{
    /// <summary>
    /// Lógica de interacción para RegistrarUsuarioView.xaml
    /// </summary>
    public partial class RegistrarUsuarioView : Window
    {
        public RegistrarUsuarioView()
        {
            InitializeComponent();
        }

        private void psw1_LostFocus(object sender, RoutedEventArgs e)
        {
            txtPassword.Text = psw1.Password;
        }
    }
}
