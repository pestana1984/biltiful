using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class ClienteController
    {// RETORNO DE DADOS DE CLIENTE

        // VIEWS

        // REGISTRAR CLIENTE
        public static void Register(List<Cliente> lista)
        {
            // VARIAVEIS
            string cpf, nome;
            char sexo;
            DateTime dNascimento;

            cpf = ReadCpf(lista); // VERIFICA CPF VALIDO E SE REPETE NA LISTA

            nome = ReadNome(); // LE O NOME 

            sexo = ReadSexo(); // LE O CARACTER DE SEXO

            dNascimento = ReadDNascimento(); // LE A DATA DE NASCIMENTO

            Cliente novoCliente = new Cliente()
            {                 
                Cpf = cpf,
                Nome = nome,
                DNascimento = dNascimento,
                DCadastro = DateTime.Now,
                UCompra = DateTime.Now,
                Sexo = sexo,
                Situacao = 'A'
            };
            lista.Add(novoCliente);
            lista = lista.OrderBy(x => x.Nome).ToList();
            WriteFile(lista);            
        }
            
        // LE CPF
        public static string ReadCpf(List<Cliente> lista)
        {
            string cpf;

            do
            { // LAÇO VERIFICA CPF SE É VALIDO E SE JÁ EXISTE NA LISTA DE CLIENTES
                Console.Write("Informe o CPF( Apenas números e com o dígito): ");
                cpf = Console.ReadLine();

                if (!CpfIsValid(cpf))
                    Console.WriteLine("CPF inválido");

                if (CpfRepeat(lista, cpf))
                    Console.WriteLine("CPF já cadastrado");

            } while (!(CpfIsValid(cpf)) || (CpfRepeat(lista, cpf)));
            // VERIFICA SE CPF É VALIDO      VERIFICA SE CPF JÁ EXISTE NA LISTA

            return cpf;
        }

        // LE NOME
        private static string ReadNome()
        {
            string nome;

            do
            { // LAÇO PARA NÃO DEIXAR CAMPO VAZIO E MAIOR QUE 50 CARACTERES
                Console.Write("Informe o Nome: ");
                nome = Console.ReadLine();
                if (nome.Length > 50)
                    Console.WriteLine("Nome tem que ser menor que 50 caracteres");

            } while (nome == "" || nome.Length > 50);

            return nome;
        }

        // LE SEXO
        private static char ReadSexo()
        {
            char sexo;

            do
            { // LAÇO VERIFICA SE O CAMPO É DIFERENTE DO SOLICITADO
                Console.Write("Infome o Sexo M - Masculino F - Feminino: ");
                sexo = char.Parse(Console.ReadLine().ToUpper());
                if (sexo != 'M' && sexo != 'F')
                    Console.WriteLine("Caracter não reconhecido como sexo");
            } while (sexo != 'M' && sexo != 'F');

            return sexo;
        }

        // LE DATA DE NASCIMENTO
        private static DateTime ReadDNascimento()
        {
            DateTime dNascimento;
            CultureInfo CultureBr = new CultureInfo(name: "pt-BR"); // DATA NO FORMATO BRASILEIRO

            do
            { // LAÇO VERIFICA A IDADE COM A DATA DE NASCIMENTO INFORMADA
                Console.Write("Informe sua Data de Nascimento: ");
                dNascimento = DateTime.ParseExact(Console.ReadLine(), "d", CultureBr); // CONVERTE DATA BRASILEIRA PRA INGLESA;
                if (!CheckAge(dNascimento))
                {
                    Console.WriteLine("Necessário ser maior de idade!!!");
                }
            } while (!CheckAge(dNascimento));
            return dNascimento;
        }

        // ATUALIZA DADOS DO CLIENTE
        public static void Update(List<Cliente> lista)
        {
            string att;

            Cliente c = Search(lista);

            if (c != null)
            {
                Console.WriteLine(Get(c));

                Console.WriteLine("Qual campo deseja atualizar:\n1 - Nome\n2 - Sexo\n3 - Data de Nascimento");
                att = Console.ReadLine();

                switch (att)
                {
                    case "1":
                        c.Nome = ReadNome(); // ATUALIZA SOMENTE NOME
                        break;
                    case "2":
                        c.Sexo = ReadSexo(); // ATUALIZA SOMENTE SEXO
                        break;
                    case "3":
                        c.DNascimento = ReadDNascimento(); // ATUALIZA DATA DE NASCIMENTO
                        break;
                }

                WriteFile(lista);

                Console.WriteLine("Campo atualizado");
            }
            else
                Console.WriteLine("Cliente não encontrado");
        }

        // IMPRESSÃO COM O CONTROLE DO USUARIO (INICIO, FIM, PROXIMO, ANTERIOR)
        public static void ControlPrint(List<Cliente> lista)
        {
            string opcao;
            int i = 0;
            do
            {
                Console.WriteLine("Escolha uma opção para impressão da fila de Clientes:" +
                            "\n1 - Inicio da fila" +
                            "\n2 - Fim da fila" +
                            "\n3 - Próximo da fila" +
                            "\n4 - Anterir da fila" +
                            "\n0 - Encerrar impressão");
                opcao = Console.ReadLine();

                Console.Clear(); // LIMPA TELA CONSOLE

                switch (opcao)
                {
                    case "1": // INICIO
                        i = 0;
                        while (lista[i]?.Situacao != 'A' && i < lista.Count)
                            i++;
                        Console.Clear();
                        Console.WriteLine(Get(lista[i]));
                        break;
                    case "2": // FIM
                        i = lista.Count - 1;
                        while (lista[i]?.Situacao != 'A' && i >= 0)
                            i--;
                        Console.Clear();
                        Console.WriteLine(Get(lista[i]));
                        break;
                    case "3": // PROXIMO
                        if(i != 0)
                            i++;
                        if (i < lista.Count)
                        {
                            while (lista[i]?.Situacao != 'A' && i < lista.Count - 1)
                                i++;
                            Console.Clear();
                            Console.WriteLine(Get(lista[i]));
                        }
                        break;
                    case "4": // ANTERIOR
                        i--;
                        if (i >= 0)
                        {
                            while (lista[i]?.Situacao != 'A' && i > 0)
                                i--;
                            Console.Clear();
                            Console.WriteLine(Get(lista[i]));
                        }
                        break;
                }
            } while (i >= 0 && i < lista.Count && opcao != "0");
        }
        // VIEWS
        
        public static string Get(Cliente c)
        {
            if (c.Situacao == 'A') // SE O CLIENTE FOR ATIVO RETORNA
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
            using (StreamWriter file = new StreamWriter(@"C:\Arquivos\Clientes.dat"))
            {
                foreach (Cliente c in listaCliente)
                    file.WriteLine(GetFile(c)); // ESCREVE A LISTA NO ARQUIVO SEPARADOS POR QUEBRA LINHA
            }
        }

        // LEITURA DE ARQUIVO
        public static void ReadFile(List<Cliente> listaCliente)
        {
            // SE O ARQUIVO EXISTIR
            if (File.Exists(@"C:\Arquivos\Clientes.dat"))
            {
                using (StreamReader file = new StreamReader(@"C:\Arquivos\Clientes.dat"))
                {
                    // VARIAVEIS
                    string nome, cpf, nasc, uCom, dCad;
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
            foreach (Cliente i in lista)
            {
                if (i.Cpf.Equals(cpf))
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
        public static Cliente Search(List<Cliente> c)
        {
            string value;

            Console.Write("Informe o CPF do Cliente: ");
            value = Console.ReadLine();
            foreach (Cliente i in c)
                if (i.Cpf == value)
                    return i;

            return null;
        }

        // DELETA CLIENTE LOGICAMENTE
        public static void Delete(List<Cliente> lista)
        {
            Cliente c = Search(lista);

            if (c != null) { 
                c.Situacao = 'I';
                WriteFile(lista);
                Console.WriteLine("Cliente excluido logicamente");
            }
            else
                Console.WriteLine("Cliente não encontrado");

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
