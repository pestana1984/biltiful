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
        // RETORNO DE DADOS DE CLIENTE
        public static string GetClient(Cliente c) 
        {
            if(c.Situacao == 'A') // SE O CLIENTE FOR ATIVO RETORNA
                return "\n>>>Cliente " + c.Nome + "<<<" +
                    "\nCPF: " + c.Cpf +
                    "\nData Nascimento: " + c.DNascimento.ToString("dd/MM/yyyy") +
                    "\nSexo: " + c.Sexo + 
                    "\nUltima Compra: " + c.UCompra.ToString("dd/MM/yyyy") +
                    "\nData de Cadastro: " + c.DCadastro.ToString("dd/MM/yyyy") + "\n";
            return "";
        }

        // ESCRITA DE ARQUIVO
        public static void WriteFile(List<Cliente> listaCliente) 
        {
            using (StreamWriter file = new StreamWriter(@"C:\Users\Luiz Sena\source\repos\LuizGustavoSena\biltiful\biltiful\SysBil\Clientes.dat"))
            {
                foreach (Cliente c in listaCliente)
                    file.WriteLine(GetClientFile(c)); // ESCREVE A LISTA NO ARQUIVO SEPARADOS POR QUEBRA LINHA
            }
        }

        // LEITURA DE ARQUIVO
        public static void ReadFile(List<Cliente> listaCliente) 
        {
            // SE O ARQUIVO EXISTIR
            if(File.Exists(@"C:\Users\Luiz Sena\source\repos\LuizGustavoSena\biltiful\biltiful\SysBil\Clientes.dat"))
            {
                using(StreamReader file = new StreamReader(@"C:\Users\Luiz Sena\source\repos\LuizGustavoSena\biltiful\biltiful\SysBil\Clientes.dat"))
                {
                    // VARIAVEIS
                    string nome, cpf, nasc, uCom, dCad ;
                    char sexo, situacao;

                    // ENQUANTO ARQUIVO EXISTIR
                    while (!file.EndOfStream)
                    {
                        // INICIALIZACAO DE VARIAVEIS
                        nasc = "";
                        uCom = "";
                        dCad = "";
                        nome = "";
                        cpf = "";
                        sexo = ' ';
                        situacao = ' ';

                        char[] line = file.ReadLine().ToCharArray(); // ARMAZENA A LINHA EM CARACTERES

                        for (int i = 0; i < 11; i++) // LE CPF
                            cpf += line[i];

                        for (int i = 11; i < 61 && line[i] != ' ' ; i++) // LE NOME
                        {
                            if (line[i] == '-')
                                line[i] = ' ';
                            nome += line[i];
                        }

                        // LE DATA NASCIMENTO
                        for (int i = 64; i < 67; i++) // MM/
                            nasc += line[i];
                        for (int i = 61; i < 64; i++) // MM/dd/
                            nasc += line[i];
                        for (int i = 67; i < 71; i++) // MM/dd/yyyy
                            nasc += line[i];

                        sexo = line[71]; // LE SEXO

                        // LE DATA ULTIMA COMPRA
                        for (int i = 75; i < 78; i++) // MM/
                            uCom += line[i];
                        for (int i = 72; i < 75; i++) // MM/dd/
                            uCom += line[i];
                        for (int i = 78; i < 82; i++) // MM/dd/yyyy
                            uCom += line[i];

                        // LE DATA CADASTRO
                        for (int i = 85; i < 88; i++) // MM/
                            dCad += line[i];
                        for (int i = 82; i < 85; i++) // MM/dd/
                            dCad += line[i];
                        for (int i = 88; i < 92; i++) // MM/dd/yyyy
                            dCad += line[i];

                        situacao = line[92]; // LE SITUACAO

                        //ADICIONANDO CLIENTE A LISTA
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

        // VERIFICA CPF REPETE
        public static bool CpfRepeat(List<Cliente> lista, string cpf)
        {
            Console.WriteLine("Entrou");
            foreach(Cliente i in lista)
            {
                if(i.Cpf.Equals(cpf))
                    return true;
            }
            return false;
        }

        // RETORNA CLIENTE NO FORMATO PARA ARQUIVO
        private static string GetClientFile(Cliente c)
        {
            return c.Cpf + NameToFile(c.Nome) + c.DNascimento.ToString("dd/MM/yyyy") +
                c.Sexo + c.UCompra.ToString("dd/MM/yyyy") + c.DCadastro.ToString("dd/MM/yyyy") + c.Situacao;
        }

        // PROCURA CLIENTE
        public static Cliente SearchClient(List<Cliente> c, string value)
        {
            foreach(Cliente i in c)
                if(i.Nome == value) 
                    return i;
            return null;
        }

        // DELETA CLIENTE LOGICAMENTE
        public static void DeleteClient(Cliente c)
        {
            c.Situacao = 'I';
        }

        // VERIFICA IDADE (MAIOR OU MENOR DE 18 ANOS)
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

        // TRATAMENTO DO NOME PARA ENVIAR AO ARQUIVO
        private static string NameToFile(string value)
        {
            char[] caracter = value.ToCharArray();
            int amo = value.Length;
            string name = "" ;

            for (int i = 0; i < amo; i++) // TROCA O ESPACO PARA UMA '-'
            {
                if (caracter[i] == ' ')
                    caracter[i] = '-';
                name += caracter[i];
            }

            for (; amo < 50; amo++) // ADICIONA OS ESPAÇOS ATÉ O LIMITE DA VARIAVEL
            {
                name += ' ';
            }
            return name;
        }

        // VERIFICA SE O CPF É VERDADEIRO
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
