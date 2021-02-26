using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class Client
    {
        public static string GetClient(Cliente c)
        {
            if(c.Situacao == 'A')
                return ">>>Cliente " + c.Nome + "<<<" +
                    "\nCPF: " + c.Cpf +
                    "\nData Nascimento: " + c.DNascimento.ToString("dd/MM/yyyy") +
                    "\nSexo: " + c.Sexo + 
                    "\nUltima Compra: " + c.UCompra.ToString("dd/MM/yyyy") +
                    "\nData de Cadastro: " + c.DCadastro.ToString("dd/MM/yyyy");
            return "";
        }
        public static string GetClientFile(Cliente c)
        {
            return c.Cpf + NameToFile(c.Nome) + c.DNascimento.ToString("dd/MM/yyyy") +
                c.Sexo + c.UCompra.ToString("dd/MM/yyyy") + c.DCadastro.ToString("dd/MM/yyyy") + c.Situacao;
        }
        public static Cliente SearchClient(List<Cliente> c, string value)
        {
            foreach(Cliente i in c)
                if(i.Nome == value)
                    return i;
            return null;
        }
        public static bool CheckAge(DateTime value)
        {
            var birthdate = value;
            var today = DateTime.Now;
            var age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;
            if (age >= 18)
                return true;
            return false;
        }
        private static string NameToFile(string value)
        {
            char[] caracter = value.ToCharArray();
            int amo = value.Length;
            string name = "" ;

            for (int i = 0; i < amo; i++)
            {
                if (caracter[i] == ' ')
                    caracter[i] = '-';
                name += caracter[i];
            }

            for (; amo < 50; amo++)
            {
                name += ' ';
            }
            return name;
        }
        public static bool CpfIsValid(string value)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            value = value.Trim();
            value = value.Replace(".", "").Replace("-", "");

            if (value.Length != 11)
                return false;

            tempCpf = value.Substring(0, 9);
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

            return value.EndsWith(digito);
        }
    }
}
