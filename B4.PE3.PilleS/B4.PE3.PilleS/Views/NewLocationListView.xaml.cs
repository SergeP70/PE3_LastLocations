﻿using B4.PE3.PilleS.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B4.PE3.PilleS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewLocationListView : ContentPage
	{
		public NewLocationListView ()
		{
			InitializeComponent ();
            BindingContext = new TrackingViewModel(this.Navigation);
        }
    }
}