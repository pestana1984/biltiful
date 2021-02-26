using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloVendas
{
    class Cliente
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime UCompra { get; set; }
        public DateTime DCadastro { get; set; }
        public char Situacao { get; set; }

        // MÉTODOS
        private static bool CalculaIdade(DateTime nascimento)
        {
            var birthdate = nascimento;
            var today = DateTime.Now;
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;
            if (age >= 18)
                return true;
            return false;
        }

        private static bool IsCpfValido(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }

        // IMPRESSAO
        public override string ToString()
        {
            if (Situacao == 'A')
                return "\n>>> Cliente " + Nome + "<<<\n" + "CPF: " + Cpf +
                    "\nData Nascimento: " + DNascimento.ToString("dd/MM/yyyy") +
                    "\nSexo: " + Sexo + "\nUltima Compra: " + UCompra.ToString("dd/MM/yyyy") +
                    "\nData de Cadastro: " + DCadastro.ToString("dd/MM/yyyy") + "\n";

            return "";
        }

        public string Consultar()
        {
            return Cpf + Nome + DNascimento.ToString("dd/MM/yyyy") +
                Sexo + UCompra.ToString("dd/MM/yyyy") + DCadastro.ToString("dd/MM/yyyy") + Situacao;
        }
    }
}
