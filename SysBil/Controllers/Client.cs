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
        public static string Get(Cliente c) 
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
                    file.WriteLine(GetFile(c)); // ESCREVE A LISTA NO ARQUIVO SEPARADOS POR QUEBRA LINHA
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

                    CultureInfo CultureBr = new CultureInfo(name: "pt-BR"); // DATA NO FORMATO BRASILEIRO

                    // ENQUANTO ARQUIVO EXISTIR
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine(); // ARMAZENA A LINHA EM CARACTERES

                        cpf = line.Substring(0, 11); // LE CPF
                        
                        nome = line.Substring(11, 50).Trim(); // LE NOME
                        
                        nasc = line.Substring(61, 10); // LE DATA DE NASCIMENTO
                        
                        sexo = line[71]; // LE SEXO

                        uCom = line.Substring(72, 10); // LE DATA DE ULTIMA COMPRA
                        
                        dCad = line.Substring(82, 10); // LE DATA DE CADASTRO
                        
                        situacao = line[92]; // LE SITUACAO

                        //ADICIONANDO CLIENTE A LISTA
                        listaCliente.Add(new Cliente()
                        {
                            Cpf = cpf,
                            Nome = nome,
                            DNascimento = DateTime.ParseExact(nasc, "d", CultureBr), // CONVERTE DATA BRASILEIRA PRA INGLESA
                            Sexo = sexo,
                            UCompra = DateTime.ParseExact(uCom, "d", CultureBr), // CONVERTE DATA BRASILEIRA PRA INGLESA
                            DCadastro = DateTime.ParseExact(dCad, "d", CultureBr), // CONVERTE DATA BRASILEIRA PRA INGLESA
                            Situacao = situacao
                        });
                    }
                }
            }
        }

        // VERIFICA CPF REPETE
        public static bool CpfRepeat(List<Cliente> lista, string cpf)
        {
            foreach(Cliente i in lista)
            {
                if(i.Cpf.Equals(cpf))
                    return true;
            }
            return false;
        }

        // RETORNA CLIENTE NO FORMATO PARA ARQUIVO
        private static string GetFile(Cliente c)
        {
            return c.Cpf + NameToFile(c.Nome) + c.DNascimento.ToString("dd/MM/yyyy") +
                c.Sexo + c.UCompra.ToString("dd/MM/yyyy") + c.DCadastro.ToString("dd/MM/yyyy") + c.Situacao;
        }

        // PROCURA CLIENTE
        public static Cliente Search(List<Cliente> c, string value)
        {
            foreach(Cliente i in c)
                if(i.Nome == value) 
                    return i;
            return null;
        }

        // DELETA CLIENTE LOGICAMENTE
        public static void Delete(Cliente c)
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
            // ADICIONA OS ESPAÇOS ATÉ O LIMITE DA VARIAVEL
            value = value.PadRight(50, ' '); 
            
            return value;
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
