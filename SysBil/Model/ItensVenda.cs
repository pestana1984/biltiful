using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ItensVenda
    {
        public string Produto { get; set; }

        public int Qtd { get; set; }

        public double Vunitario { get; set; }

        public double Titem { get; set; }


        public override string ToString()
        {
            return $"\nProduto: {Produto}\nQuantidade: {Qtd}\nVunitario: {Vunitario}\nTotal do item: {Titem}\n";
        }
    }
}
