using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projem.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GirisSayfasi : ContentPage
    {
        public GirisSayfasi()
        {
            InitializeComponent();
        }

        private void onOyunaBasla(object sender, EventArgs e)
        {
            MyPageContent ctx = new MyPageContent();
            Navigation.PushAsync(ctx,true);
        }

        private void onOyunListesi(object sender, EventArgs e)
        {
            ListPage lst = new ListPage();
            Navigation.PushAsync(lst,true);
        }

        private void onNasilOynanir(object sender, EventArgs e)
        {
            DisplayAlert("İPUCU","EKRANDA VERİLEN YAZININ RENGİNE GÖRE İLGİLİ BUTON SEÇİLİR","OK");
        }
    }
}