using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeitorNfe.Transporte;
using LeitorNfe.Outros;

namespace LeitorNfe.Classe
{
    public class NFE
    {
        public Ide ide { get; set; }
        public Emit emit { get; set; }
        public Dest dest { get; set; }
        public Retirada ret { get; set; }
        public Entrega entr { get; set; }
        public Imposto imposto { get; set; }
        public Transp transporte { get; set; }
        public InfAdic infAdic { get; set; }
        public TribTotal total { get; set; }
    }
}
