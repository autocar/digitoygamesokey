using System;
using System.Collections.Generic;
using System.Linq;

namespace basitokeyy
{
    class Program
    {
        enum TasCesidi
        {
            Sari = 0, Mavi = 1, Siyah = 2, Kirmizi = 3, Sahte = 4
        }
        enum Oyuncu
        {
            Cemal = 0, Mertcan = 1, Fatih = 2, Süleyman = 3
        }
        static void Main(string[] args)
        {
            List<Tas> taslarDizisi = taslarDizisiOlustur();
            taslarDizisi.AddRange(taslarDizisiOlustur());
            //karistir
            taslarDizisi = taslarDizisi.OrderBy(a => new Random().Next()).ToList();

            //gosterge seçme ve okey belirleme
            Random rnd = new Random();
            int gostergeIndex = rnd.Next(0, 105);
            while (taslarDizisi[gostergeIndex].tasCesidi == TasCesidi.Sahte)
            {
                gostergeIndex = rnd.Next(0, 105);
            }
            Tas gostergeTasi = taslarDizisi[gostergeIndex];
            Tas okey = taslarDizisi.Find(x => (x.sayi == (gostergeTasi.sayi) % (13 + 1 - 1) + 1) && (x.tasCesidi == gostergeTasi.tasCesidi));
            taslarDizisi.Remove(gostergeTasi);

            //tas dagitma
            List<Istaka> istakalar = new List<Istaka>();
            for (int i = 0; i < 4; i++)
            {
                istakalar.Add(new Istaka() { oyuncu = (Oyuncu)i, taslar = new List<Tas>() });
            }
            int onBesAlacakKisi = rnd.Next(0, 3);
            int rastgeleTasIndex;
            for (int i = 0; i < 15; i++)
            {
                rastgeleTasIndex = rnd.Next(0, taslarDizisi.Count - 1);
                istakalar[onBesAlacakKisi].taslar.Add(taslarDizisi[rastgeleTasIndex]);
                taslarDizisi.RemoveAt(rastgeleTasIndex);
            }
            for (int i = 0; i < 4; i++)
            {
                if (i == onBesAlacakKisi)
                    continue;
                for (int p = 0; p < 14; p++)
                {
                    rastgeleTasIndex = rnd.Next(0, taslarDizisi.Count - 1);
                    istakalar[i].taslar.Add(taslarDizisi[rastgeleTasIndex]);
                    taslarDizisi.RemoveAt(rastgeleTasIndex);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(istakalar[i].oyuncu.ToString());
                Console.WriteLine();
                Console.WriteLine();
                for (int p = 0; p < istakalar[i].taslar.Count; p++)
                {
                    Console.WriteLine(istakalar[i].taslar[p].tasCesidi.ToString() + " " + istakalar[i].taslar[p].sayi.ToString());
                }
                Console.WriteLine();
            }
            Console.WriteLine("Kalan Taş Sayısı: " + taslarDizisi.Count);
            Console.ReadKey();

        }

        static List<Tas> taslarDizisiOlustur()
        {
            List<Tas> taslarDizisi = new List<Tas>();
            for (int i = 0; i < 4; i++)
            {
                TasCesidi tas = (TasCesidi)i;
                for (int p = 0; p < 13; p++)
                {
                    Tas t = Tas.Create(p + 1, i * 13 + p, tas);
                    taslarDizisi.Add(t);
                }
            }
            Tas sahteOkey = Tas.Create(-1, 52, TasCesidi.Sahte);
            taslarDizisi.Add(sahteOkey);
            return taslarDizisi;
        }
        struct Tas
        {
            public static Tas Create(int sayi, int index, TasCesidi tasCesidi)
            {
                return new Tas { sayi = sayi, index = index, tasCesidi = tasCesidi };
            }
            public int sayi;
            public int index;
            public TasCesidi tasCesidi;
        }

        struct Istaka
        {
            /*struct Ciftler
            {
                public Tas ilk;
                public Tas son;
            }*/
            public Oyuncu oyuncu;
            public List<Tas> taslar;

            /*  Çiftleri kontrol etme
             *  Buraya kadar gelebildim.
             * 
             * public int ciftSayisi(Tas okey)
             {
                 List<Tas> localTaslar = taslar;
                 int ciftAdet = 0;
                 List<Ciftler> ciftler = new List<Ciftler>();
                 for (int i = 0; i < localTaslar.Count;)
                 {
                     Tas gecerliTas = localTaslar[i];
                     int lastIndex = taslar.FindLastIndex(x => x.sayi == gecerliTas.sayi && x.tasCesidi == gecerliTas.tasCesidi);
                     if (lastIndex != i)
                     {
                         ciftAdet++;
                         ciftler.Add(new Ciftler { ilk = gecerliTas, son = localTaslar[lastIndex] });
                         localTaslar.RemoveAt(i);
                         localTaslar.RemoveAt(lastIndex);
                         continue;
                     }
                     i++;
                 }
                 for (int i = 0; i < localTaslar.Count; i++)
                 {
                     Tas gecerliTas = localTaslar[i];
                 }*/
        }

    }

}

