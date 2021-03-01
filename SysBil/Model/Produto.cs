using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Produto
    {
        public string CBarras { get; set; }
        public string Nome { get; set; }
        public int  Id { get; set; }
        public double Vvenda { get; set; }
        public DateTime UVenda { get; set; }
        public DateTime DCadastro { get; set; }
        public char Situacao { get; set; }

        public Produto() { }
        public Produto(string cb, string nome, double vv, DateTime uv, DateTime dc, char sit)
        {
            CBarras = cb;
            Nome = nome;
            Vvenda = vv;
            UVenda = uv;
            DCadastro = dc;
            Situacao = sit;
        }
        public override string ToString()
        {           
            return $"Cbarras: {CBarras}\nNome: {Nome}\nValor venda: {Vvenda}\nData última venda: {UVenda}\nData Cadastro: {DCadastro}\nSituação: {Situacao}";
        }

    }
}
