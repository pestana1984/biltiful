using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                return "\n>>>Cliente " + c.Nome + "<<<" +
                    "\nCPF: " + c.Cpf +
                    "\nData Nascimento: " + c.DNascimento.ToString("dd/MM/yyyy") +
                    "\nSexo: " + c.Sexo + 
                    "\nUltima Compra: " + c.UCompra.ToString("dd/MM/yyyy") +
                    "\nData de Cadastro: " + c.DCadastro.ToString("dd/MM/yyyy") + "\n";
            return "";
        }
        public static void WriteFile(List<Cliente> listaCliente)
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\Luiz Sena\source\repos\LuizGustavoSena\biltiful\biltiful\SysBil\Clientes.dat"))
            {
                foreach (Cliente c in listaCliente)
                    file.WriteLine(GetClientFile(c));
            }
        }
        public static void ReadFile(List<Cliente> listaCliente)
        {
            if(File.Exists(@"C:\Users\Luiz Sena\source\repos\LuizGustavoSena\biltiful\biltiful\SysBil\Clientes.dat"))
            {
                using(StreamReader file = new StreamReader(@"C:\Users\Luiz Sena\source\repos\LuizGustavoSena\biltiful\biltiful\SysBil\Clientes.dat"))
                {
                    string nome, cpf, nasc, uCom, dCad ;
                    char sexo, situacao;

                    while (!file.EndOfStream)
                    {
                        nasc = "";
                        uCom = "";
                        dCad = "";
                        nome = "";
                        cpf = "";
                        sexo = ' ';
                        situacao = ' ';

                        char[] line = file.ReadLine().ToCharArray();

                        for (int i = 0; i < 11; i++) // CPF
                            cpf += line[i];

                        for (int i = 11; i < 61 && line[i] != ' ' ; i++) // NOME
                        {
                            if (line[i] == '-')
                                line[i] = ' ';
                            nome += line[i];
                        }

                        // DATA NASCIMENTO
                        for (int i = 64; i < 67; i++) // MM/
                            nasc += line[i];
                        for (int i = 61; i < 64; i++) // MM/dd/
                            nasc += line[i];
                        for (int i = 67; i < 71; i++) // MM/dd/yyyy
                            nasc += line[i];

                        sexo = line[71]; // SEXO

                        // DATA ULTIMA COMPRA
                        for (int i = 75; i < 78; i++) // MM/
                            uCom += line[i];
                        for (int i = 72; i < 75; i++) // MM/dd/
                            uCom += line[i];
                        for (int i = 78; i < 82; i++) // MM/dd/yyyy
                            uCom += line[i];

                        // DATA CADASTRO
                        for (int i = 85; i < 88; i++) // MM/
                            dCad += line[i];
                        for (int i = 82; i < 85; i++) // MM/dd/
                            dCad += line[i];
                        for (int i = 88; i < 92; i++) // MM/dd/yyyy
                            dCad += line[i];

                        situacao = line[92];


                        listaCliente.Add(new Cliente()
                        {
                            Cpf = cpf,
                            Nome = nome,
                            DNascimento = DateTime.Parse(nasc),
                            Sexo = sexo,
                            UCompra = DateTime.Parse(uCom),
                            DCadastro = DateTime.Parse(dCad),
                            Situacao = situacao
                        });
                    }
                }
            }
        }
        private static string GetClientFile(Cliente c)
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
