using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MPrima
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public DateTime Ucompra { get; set; }
        public DateTime Dcadastro { get; set; }
        public char Situacao { get; set; }

        public MPrima() { }
        public MPrima(string id, string nome, DateTime ucompra, DateTime dcadastro, char situacao)
        {
            Id = id;
            Nome = nome;
            Ucompra = ucompra;
            Dcadastro = dcadastro;
            Situacao = situacao;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nNome: {Nome}\nData Compra: {Ucompra}\nData Cadastro: {Dcadastro}\nSituação: {Situacao}";
        }
    }
}
