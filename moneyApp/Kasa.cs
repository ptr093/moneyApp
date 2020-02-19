using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moneyApp
{
    class Kasa
    {
        public Decimal Kwota { get; set; }

        public string NazwaTransakcji { get; set; }


        public string Data { get; set; }

        public string RodzajTransakcji { get; set; } 

        public override string ToString()
        {
            return base.ToString();
        }
    }

    
}
