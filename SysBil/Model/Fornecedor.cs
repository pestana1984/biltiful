using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Fornecedor
    {
        public string Cnpj { get; set; }
        public string RSocial { get; set; }
        public DateTime DAbertura { get; set; }
        public DateTime UCompra { get; set; }
        public DateTime DCadastro { get; set; }
        public char Situacao { get; set; }

        public override string ToString()
        {
            string situacao;
            if (Situacao == 'a')
            {
                situacao = "Ativo";
            }
            else
            {
                situacao = "Inativo";
            }
            return $"CNPJ: {Cnpj}\n" +
                   $"Razao social : {RSocial}\n" +
                   $"Data abertura da empresa: {DAbertura.Day}/{DAbertura.Month}/{DAbertura.Year}\n" +
                   $"Data da ultima compra: {UCompra.Day}/{UCompra.Month}/{UCompra.Year}\n" +
                   $"Data de cadastro: {DCadastro.Day}/{DCadastro.Month}/{DCadastro.Year}\n" +
                   $"Situacao: {situacao}";
        }
    }
}
