using LeitorNfe.Transporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeitorNfe.Classe
{
    public class Transp
    {
        public string modFrete { get; set; }

        public Transporta transporta { get; set; }

        public VeicTransp veicTransp { get; set; }

        public Reboque reboque { get; set; }

        public Vol vol { get; set; }
    }
}
