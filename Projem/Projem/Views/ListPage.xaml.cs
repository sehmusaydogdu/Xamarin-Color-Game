using Projem.App_Data;
using Projem.Model;
using System;
using System.Collections.Generic;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projem.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
            List<OyunBilgi> oyunbilgileri = new List<OyunBilgi>();
            SQLiteManager manager = new SQLiteManager();
            oyunbilgileri = manager.GetAll().OrderByDescending(x => x.Skor).ToList();
            lstOyunBilgi.BindingContext = oyunbilgileri;
        }

        private void onOyunaDon(object sender, EventArgs e)
        {
            Navigation.PushAsync(new GirisSayfasi(),true);
        }
    }
}