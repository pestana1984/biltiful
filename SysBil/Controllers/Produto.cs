using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    class Produto
    {
        public override string ToString()
        {
            return $"Id: {Id}\nNome: {Nome}\nValor de venda: {Vvenda}";
        }

    }
}
