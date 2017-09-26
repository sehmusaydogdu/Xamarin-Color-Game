using Projem.App_Data;
using Projem.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Projem.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyPageContent : ContentPage
    {
        public MyPageContent()
        {
            InitializeComponent();
            OyunSkorBelirle();
            Start();
        }

        Sayilar sayi = new Sayilar();
        SQLiteManager manager;
        private void OyunSkorBelirle()
        {
            manager = new SQLiteManager();
            if (manager.GetAll().ToList().OrderByDescending(x => x.Skor).FirstOrDefault() == null)
                Sayilar.OyunSkor = 0;
            else
            {
                manager = new SQLiteManager();
                Sayilar.OyunSkor = manager.GetAll().ToList().OrderByDescending(x => x.Skor).FirstOrDefault().Skor;
            }
            lblSkor.Text = "En Yüksek Skor  :  " + Sayilar.OyunSkor;
            manager = null;
        }
        private void onInsert()
        {
            manager = new SQLiteManager();
            OyunBilgi o = new OyunBilgi();
            o.BaslangicZamani= DateTime.Now;
            o.Skor = sayi.ToplamPuan;
            var lis = manager.GetAll().ToList().Count;

            if (lis < 10)
            {
                manager = new SQLiteManager();
                int isDurum = manager.Insert(o);
            }
            else
            {
                manager = new SQLiteManager();
                var liste = manager.GetAll().ToList().OrderBy(x => x.Skor).FirstOrDefault();

                if (liste.Skor < o.Skor)
                {
                    manager = new SQLiteManager();
                    manager.Delete(liste.ID);

                    manager = new SQLiteManager();
                    manager.Insert(o);
                }

            }

            manager = null;
        }
        private void Start()
        {
            Basla();
            GeriSayimAraci(60);
            lblTitle.Text = "Doğru Sayısı  :  " + sayi.ToplamPuan;
        }
        private async void GeriSayimAraci(int t)
        {
            while (t >= 0)
            {
                lblKalanSure.Text = "Kalan Süre   :  " + t.ToString();
                slider.Value = t;
                t--;
                await Task.Delay(750);
            }
            onInsert();

            if (sayi.ToplamPuan > Sayilar.OyunSkor)
            {
                lblSkor.Text = "En Yüksek Skor  : " + sayi.ToplamPuan;
                sayi.IsDurum = await DisplayAlert("Süre Doldu", "Tebrikler. Yeni Skorunuz : "+sayi.ToplamPuan, "TEKRAR OYNA", "ÇIKIŞ");
                sayi.ToplamPuan = 0;
            }
            else
            {
                sayi.IsDurum = await DisplayAlert("Süre Doldu", "Skoru Geçemediniz. ", "TEKRAR OYNA", "ÇIKIŞ");
                sayi.ToplamPuan = 0;
            }

            if (sayi.IsDurum == true)
            {
                lblSkor.Text = "En Yüksek Skor  : " + Sayilar.OyunSkor;
                TekrardanBasla();
            }

            if (sayi.IsDurum == false)
                await Navigation.PushAsync(new ListPage(),true);

            manager = null;

        }
        private void Basla()
        {
            Random uret = new Random();
            int yazi = uret.Next(0, 3);
            int renk = uret.Next(0, 3);

            switch (yazi){
                case 0: lblYazi.Text = "KIRMIZI";break;
                case 1: lblYazi.Text = "MAVİ"; break;
                case 2: lblYazi.Text = "YEŞİL"; break;
            }

            switch (renk){
                case 0: lblYazi.TextColor = Color.Blue; break;
                case 1: lblYazi.TextColor = Color.Green;break;
                case 2: lblYazi.TextColor = Color.Red;break;
            }
        }
        private void TekrardanBasla()
        {
            OyunSkorBelirle();
            sayi.ToplamPuan = 0;
            lblYazi.TextColor = Color.Default;
            Start();
        }
        private void btnDegistir()
        {
            Random uret = new Random();
            int gecici = uret.Next(1, 4);
            string text = string.Empty;
            switch (gecici)
            {
                case 1:
                    {
                        text = btn2.Text;
                        btn2.Text = btn3.Text;
                        btn3.Text = text;
                        break;
                    }
                case 2:
                    {
                        text = btn1.Text;
                        btn1.Text = btn3.Text;
                        btn3.Text = text;
                        break;
                    }
                case 3:
                    {
                        text = btn1.Text;
                        btn1.Text = btn2.Text;
                        btn2.Text = text;
                        break;
                    }
            }
        }
        private async void btnOnClick(object sender , EventArgs e)
        {
            
            Color gecici=new Color();
            Button btn = sender as Button;
            switch (btn.Text)
            {
                case "KIRMIZI":gecici = Color.Red;break;
                case "MAVİ": gecici = Color.Blue; break;
                case "YEŞİL": gecici = Color.Green; break;
            }

            if (lblYazi.TextColor == gecici)
                sayi.ToplamPuan += 10;

            else
                sayi.ToplamPuan -= 10;

            lblTitle.Text = "Doğru Sayısı  : " + sayi.ToplamPuan;
            Basla();
            btnDegistir();
            await Task.Delay(200);
        }
    }
}