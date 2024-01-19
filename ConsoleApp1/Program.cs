using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Iroda
{
    public string Kod { get; set; }
    public int Kezdet { get; set; }
    public List<int> Ltszamok { get; set; }

    // Új mező az emelet szám tárolásához
    public int EmeletSzam => Ltszamok.IndexOf(Ltszamok.Max()) + 1;

    public Iroda(string kod, int kezdet, List<int> ltszamok)
    {
        Kod = kod;
        Kezdet = kezdet;
        Ltszamok = ltszamok;
    }

    public override string ToString()
    {
        string result = $"{Kod,-12} {Kezdet,-8}";
        foreach (var ltszam in Ltszamok)
        {
            result += $"{ltszam,-4}";
        }
        return result;
    }
}

class Program
{
    static void Main()
    {
        List<Iroda> irodak = Beolvas("../../../src/irodahaz.txt");

        for (int i = 0; i < irodak.Count; i++)
        {
            Console.WriteLine($"{i + 1,-8} {irodak[i]}");
        }

        int legtobbEmelet = irodak.Max(iroda => iroda.EmeletSzam);
        Console.WriteLine($"A legtöbben a(z) {legtobbEmelet}. emeleten dolgoznak.");

        Iroda kilencenIroda = irodak.FirstOrDefault(iroda => iroda.Ltszamok.Contains(9));

        if (kilencenIroda != null)
        {
            Console.WriteLine($"Van olyan iroda, ahol kilencen vannak: {kilencenIroda.Kod}, Iroda sorszám: {irodak.IndexOf(kilencenIroda) + 1}");
        }
        else
        {
            Console.WriteLine("Nincs olyan iroda, ahol kilencen vannak.");
        }

        int otNelTobbIrodak = irodak.Count(iroda => iroda.Ltszamok.Any(ltszam => ltszam > 5));
        Console.WriteLine($"Ötnél többen dolgoznak {otNelTobbIrodak} irodában.");

        using (StreamWriter writer = new StreamWriter("../../../src/ures_irodak.txt"))
        {
            foreach (var iroda in irodak)
            {
                var uresEmeletek = Enumerable.Range(1, iroda.Ltszamok.Count)
                    .Where(emelet => iroda.Ltszamok[emelet - 1] == 0);

                writer.WriteLine($"{iroda.Kod} {string.Join(" ", uresEmeletek)}");
            }
        }

        string keresettKod = "LOGMEIN";
        var logmeinIrodak = irodak.Where(iroda => iroda.Kod.Equals(keresettKod, StringComparison.OrdinalIgnoreCase));

        if (logmeinIrodak.Any())
        {
            int atlagLtszam = (int)logmeinIrodak.Average(iroda => iroda.Ltszamok.Sum());
            Console.WriteLine($"A(z) {keresettKod} kódú cég irodáiban átlagosan {atlagLtszam} személy dolgozik.");
        }
        else
        {
            Console.WriteLine($"Nincs olyan cég, amely a(z) {keresettKod} kódot használja.");
        }

    }

    static List<Iroda> Beolvas(string fajlnev)
    {
        List<Iroda> irodak = new List<Iroda>();

        using (StreamReader reader = new StreamReader(fajlnev))
        {
            while (!reader.EndOfStream)
            {
                string[] sor = reader.ReadLine().Split(' ');
                string kod = sor[0];
                int kezdet = int.Parse(sor[1]);
                List<int> ltszamok = new List<int>();

                for (int i = 2; i < sor.Length; i++)
                {
                    ltszamok.Add(int.Parse(sor[i]));
                }

                Iroda iroda = new Iroda(kod, kezdet, ltszamok);
                irodak.Add(iroda);
            }
        }

        return irodak;
    }
}
