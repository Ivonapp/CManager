using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CManager.Presentation.GuiApp.Views
{
    /// <summary>
    /// Interaction logic for ActivitiesView.xaml
    /// </summary>
    public partial class ActivitiesView : UserControl
    {


        public ActivitiesView()
        {
            InitializeComponent();
        }



        private void CreateCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            Activities.Items.Add(AddActivity.Text); 
            AddActivity.Clear();
        }
    }
    }
