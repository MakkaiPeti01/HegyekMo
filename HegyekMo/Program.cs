using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HegyekMo
{
    class Program
    {
        static List<Hegyek> Hegyek = new List<Hegyek>();
        static List<double> magasok = new List<double>();
        static void Beolvasas()
        {
            StreamReader olvas = new StreamReader("hegyekMo.txt");
            olvas.ReadLine();
            while (!olvas.EndOfStream)
            {
                string[] adat = olvas.ReadLine().Split(';');
                Hegyek.Add(new Hegyek(adat[0], adat[1], int.Parse(adat[2])));
            }
            olvas.Close();
            Console.WriteLine($"3. feladat: Hegycsúcsok száma: {Hegyek.Count} db");
        }
        static void Negyedik()
        {
            double atlag = 0;
            double sum = 0;
            foreach (var i in Hegyek)
            {
                sum = sum + i.Magassag;
            }
            atlag = sum / Hegyek.Count;
            Console.WriteLine($"4. feladat: A hegyek magasságának az átlaga: {atlag}");
        }
        static void Otodik()
        {
            //maximum
            string neve = "";
            string hegyseg = "";
            int magassag = 0;
            int max = 0;
            foreach (var i in Hegyek)
            {
                if (max<i.Magassag)
                {
                    max = i.Magassag;
                    neve = i.Hegycsúcs;
                    hegyseg = i.Hegyseg;
                    magassag = i.Magassag;
                }
            }
            Console.WriteLine($"5. feladat: A legmagasabb hegycsúcs adatai:");
            Console.WriteLine($"\t Név: {neve}" +
                $"\n\t Hegység: {hegyseg}" +
                $"\n\t Magassága: {magassag} m");
        }
        static void Hatodik()
        {
            Console.Write("6. feladat: Kérek egy magasságot: ");
            int be = int.Parse(Console.ReadLine());
            int talalat = 0;
            foreach (var i in Hegyek)
            {
                if (i.Hegyseg.Contains("Börzsöny"))
                {
                    if (be<i.Magassag)
                    {
                        talalat = i.Magassag;
                        Console.WriteLine($"\t Van magasabb hegycsúcs a Börzsönyben {be}-nél, {talalat}.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"\t{be}m-nél nincs magasabb hegycsúcs a Börzsönyben!");
                        break;
                    }
                }
            }
        }
        static void Hetedik()
        {
            // Határozza meg és írja ki a képernyőre a minta szerint azoknak a hegycsúcsoknak a
            // számát, amelyek 3000 lábnál magasabbak!Az átváltáshoz az 1 m = 3.280839895 láb
            //értékkel dolgozzon!
           
            double valto = 3.280839895;
            int db = 0;
            foreach (var i in Hegyek)
            {
                magasok.Add(i.Magassag * valto);
            }
            foreach (var i in magasok)
            {
                if (i > 3000)
                {
                    db++;
                }
            }
            Console.WriteLine($"7. feladat: 3000 lábnál magasabb hegycsúcsok száma: {db}");
            
        }
        static void Nyolcadik()
        {
            Dictionary<string, int> csucsok = new Dictionary<string, int>();
            foreach (var i in Hegyek)
            {
                if (!csucsok.ContainsKey(i.Hegyseg))
                {
                    csucsok.Add(i.Hegyseg, 0);
                }
            }
            foreach (var i in Hegyek)
            {
                if (csucsok.ContainsKey(i.Hegyseg))
                {
                    csucsok[i.Hegyseg]++;
                }
            }
            Console.WriteLine("8. feladat: Hegység statisztika");
            foreach (var i in csucsok)
            {
                Console.WriteLine($"\t{i.Key} - {i.Value} db");
            }
        }
        static void Kilencedik()
        {
            Dictionary<string, double> bukk = new Dictionary<string, double>();
            double valto = 3.280839895;
            foreach (var i in Hegyek)
            {
               if (i.Hegyseg.Contains("Bükk-vidék"))
                  {
                    if (!bukk.ContainsKey(i.Hegycsúcs))
                    {
                        bukk.Add(i.Hegycsúcs, i.Magassag * valto);
                    }
                  }
            }
            StreamWriter iro = new StreamWriter("bukk-videk.txt");
            iro.WriteLine("Hegycsúcs neve;Magasság láb");
            foreach (KeyValuePair<string, double> i in bukk.OrderBy(key => key.Key))
            {
                iro.WriteLine("{0},{1}", i.Key, Math.Round(i.Value),2);
            }
            iro.Close();
            Console.WriteLine("9. feladat: bukk-videk.txt");
        }
        static void Main(string[] args)
        {
            Beolvasas();
            Negyedik();
            Otodik();
            Hatodik();
            Hetedik();
            Nyolcadik();
            Kilencedik();
            Console.ReadKey();
        }
    }
}
