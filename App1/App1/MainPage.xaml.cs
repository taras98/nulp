using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using App1.VM;

namespace App1
{
    public partial class MainPage : ContentPage
    {

        DayViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();

            viewModel = new DayViewModel("ПІ-42", 0);
            BindingContext = viewModel;

        }


    }
}
