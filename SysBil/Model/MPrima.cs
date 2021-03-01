using System;
using System.Collections.Generic;
using System.Globalization;
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
        public string Situacao { get; set; }

        public MPrima() { }
        public MPrima(string id, string nome, DateTime ucompra, DateTime dcadastro, string situacao)
        {
            Id = id;
            Nome = nome;
            Ucompra = ucompra;
            Dcadastro = dcadastro;
            Situacao = situacao;
        }

        public override string ToString()
        {
            return $"ID: {Id}\nNome: {Nome}\nData Compra: {Ucompra.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}\nData Cadastro: {Dcadastro.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}\nSituação: {Situacao}";
        }
    }
}
