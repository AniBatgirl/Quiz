using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    internal class Priklad
    {
        private string Vypocet;
        private string Vysledek;
        List<string> SpatneOdpovedi = new List<string>();

        public Priklad(string _vypocet, string _vysledek, List<string> _spatneOdpovedi)
        {
            Vypocet = _vypocet;
            Vysledek = _vysledek;
            SpatneOdpovedi = _spatneOdpovedi;
        }
    }
}
