using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBil
{
    class Produto
    {

        public string Nome { get; set; }
        public int  Id { get; set; }
        public double Vvenda { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}\nNome: {Nome}\nValor de venda: {Vvenda}";
        }

    }
}
