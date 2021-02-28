using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{ 
    public class Compra
    {
        public int Id { get; set; }
        public DateTime Dcompra { get; set; }
        public string Fornecedor { get; set; }
        public float Vtotal { get; set; }
        public string Mprima { get; set; }
        public float Qtd { get; set; }
        public float Vunitario { get; set; }
        public float Titem { get; set; }
        

    }
}
