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
            sb.Append("Registro da Compra: \n\n");
            for (int i = 0; i < Qtd.Length; i++)
            {
                if (Mprima[i] != "000000")
                {
                    sb.Append($"ID Materia Prima: {Mprima[i]}\n");
                    sb.Append($"Unidades da MP: {Qtd[i]}\n");
                    sb.Append($"Valor unitário: R${Vunitario[i]}\n");
                    sb.Append($"Valor total do item {Mprima[i]}: R${Titem[i]:F2}\n");
                }
            }
            sb.Append($"ID: {Id}\n");
            sb.Append($"Data da Compra: {Dcompra.Day}/{Dcompra.Month}/{Dcompra.Year}\n");
            sb.Append($"Valor total:" + $"R${Vtotal:F2}\n");
            return sb.ToString();
        }

    }
}
