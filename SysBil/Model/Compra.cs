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
        public string[] Mprima { get; set; }
        public float[] Qtd { get; set; }
        public float[] Vunitario { get; set; }
        public float[] Titem { get; set; }
        public string CNPJ { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Qtd.Length; i++)
            {
                sb.Append(Mprima[i]);
                sb.Append($"{Qtd[i]:000.00}".Replace(".", ""));
                sb.Append($"{Vunitario[i]:000.00}".Replace(".", ""));
                sb.Append($"{Titem[i]:0000.00}".Replace(".", ""));
            }
            sb.Append($"{Id:D5}");
            sb.Append($"{Dcompra:ddMMyyyy}");
            sb.Append(CNPJ);
            sb.Append($"{Vtotal:00000.00}".Replace(".", ""));
            return sb.ToString();
        }

    }
}
