using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Venda
    {
        public int Id { get; set; }


        public DateTime Data { get; set; }
        public List<ItensVenda> ListaItensVendas { get; set; }

        public double ValorTotal { get; set; }

        public string ClienteCpf{ get; set; }

        public override string ToString()
        {
            string itemVendas = "";
            foreach (ItensVenda vend in ListaItensVendas)
            {
                itemVendas = itemVendas + vend.ToString();
            }
            return $"\n>>>>>DADOS DO CLIENTE<<<<<\nId: {Id}\nData: {Data.ToString("dd/MM/yyyy")}\n\n>>>>>ITENS COMPRADOS<<<<<{itemVendas}" +
                $"\nValor total da compra: {ValorTotal}\n"; //\nNome: {Produto}
        }       
    }
}
