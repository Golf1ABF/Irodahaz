using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dolgozat
{
    internal class Iroda
    {
        public string Kod { get; set; }
        public int Kezdet { get; set; }
        public List<int> Letszamadat { get; set; }


        public int EmeletSzam => Letszamadat.IndexOf(Letszamadat.Max()) + 1;


        public Iroda(string l)
        {
            var d = l.Split(' ');
            this.Kod = d[0];
            this.Kezdet = int.Parse(d[1]);
            this.Letszamadat = new List<int>();
        }


        public override string ToString()
        {
            string result = $"{Kod,-12} {Kezdet,-8}";
            foreach (var ltszam in Letszamadat)
            {
                result += $"{ltszam,-4}";
            }
            return result;
        }
    }
}
