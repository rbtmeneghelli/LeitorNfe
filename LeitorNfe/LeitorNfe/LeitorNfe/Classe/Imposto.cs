using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeitorNfe.Impostos;

namespace LeitorNfe.Classe
{
    public class Imposto
    {
        public List<Prod> produto { get; set; }
        public List<ICMS> ICMS { get; set; }
        public List<PIS> PIS { get; set; }
        public List<COFINS> COFINS { get; set; }
    }
}
