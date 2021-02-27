using Model;
using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SysBil
{
	class Program
	{
		static void Main(string[] args)
		{
			// VARIAVEIS
			Cliente c;
			List<Cliente> listaCliente = new List<Cliente>();
			byte op;
			string buscar;

			Client.ReadFile(listaCliente); // LE OS CLIENTES DO ARQUIVO

            do
            { // LAÇO DE OPÇOES

				op = Menu(); // CHAMA MENU

				Console.Clear(); // LIMPA A TELA

				switch (op)
                {
					case 1: // INSERIR

						c = lerCliente(listaCliente); // LE O CLIENTE

						listaCliente.Add(c); // ADICIONA CLIENTE A LISTA

						listaCliente = listaCliente.OrderBy(x => x.Nome).ToList(); // ORDENA A LISTA POR NOME

						Client.WriteFile(listaCliente); // ADICIONA A LISTA NO ARQUIVO

						Console.WriteLine("Cliente Cadastrado");

						break;
					case 2: // IMPRIMIR

						if (listaCliente.Count > 0) // SE TIVER CLIENTE NA LISTA
							listaCliente.ForEach(x => Console.WriteLine(Client.Get(x))); // IMPRIME LISTA
						else
							Console.WriteLine("Lista de Clientes vazia");

						break;
					case 3: // IMPRIMIR UM POR UM

						ImpressaoControl(listaCliente);

						break;
					case 4: // LOCALIZAR

						Console.Write("Informe o Nome do Cliente que deseja buscar: ");
						buscar = Console.ReadLine(); // LE O NOME DO CLIENTE A SER BUSCADO

						c = Client.Search(listaCliente, buscar); // LOCALIZA CLIENTE

						if (c != null && c.Situacao == 'A') // SE CLIENTE EXISTIR E TIVER SITUAÇÃO ATIVA
							Console.WriteLine(Client.Get(c));
						else
							Console.WriteLine("Cliente não encontrado");
						break;
					case 5: // ATUALIZAR CLIENTE

						Console.Write("Informe o Nome do Cliente que deseja Alterar: ");
						buscar = Console.ReadLine(); // LE O NOME DO CLIENTE A SER BUSCADO

						c = Client.Search(listaCliente, buscar); // LOCALIZA CLIENTE

						if (c != null) // SE CLIENTE FOI ENCONTRADO
							atualizaCliente(c, listaCliente); // ATUALIZA O CAMPO QUE O CLIENTE DESEJA DELE
						else
							Console.WriteLine("Cliente não encontrado");

						Client.WriteFile(listaCliente); // ADICIONA A LISTA NO ARQUIVO

						Console.WriteLine("Cliente Atualizado");

						break;
					case 6: // DELETAR LOGICAMENTE

						Console.Write("Informe o Nome do Cliente que deseja Alterar: ");
						buscar = Console.ReadLine(); // LE O NOME DO CLIENTE A SER BUSCADO

						c = Client.Search(listaCliente, buscar); // BUSCA CLIENTE

						Client.Delete(c); // DELETA O CLIENTE LOGICAMENTE

						Client.WriteFile(listaCliente); // ADICIONA A LISTA NO ARQUIVO

						Console.WriteLine("Cliente Deletado logicamente");

						break;
                }
            } while (op != 0);
		}

		private static byte Menu()
        {
			// VARIAVEIS
			string opcao; 
			byte op;

						// MENU
			Console.WriteLine("\n------->>> MENU <<<-------");
			Console.WriteLine("\n1 - Inserir Cliente" +
								"\n2 - Imprimir Clientes" +
								"\n3 - Imprimir com Controle" +
								"\n4 - Buscar Cliente" +
								"\n5 - Atualizar Cliente" +
								"\n6 - Deletar Cliente" +
								"\n0 - Sair" +
								"\n\n--------------------------");
			opcao = Console.ReadLine();

			if (byte.TryParse(opcao, out op)) // SE CONSEGUIR CONVERTER PARA BYTE IMPRIME
				return op;
			return Menu(); // SE NÃO CONSEGUIR CHAMA A FUNÇÃO NOVAMENTE
        }

		private static Cliente lerCliente(List<Cliente> lista)
        {
			// VARIAVEIS
			string cpf, nome;
			char sexo;
			DateTime dNascimento;

			cpf = lerCpf(lista); // VERIFICA CPF VALIDO E SE REPETE

			nome = lerNome(); // LE O NOME 

			sexo = lerSexo(); // LE O CARACTER DE SEXO

			dNascimento = lerDNascimento(); // LE A DATA DE NASCIMENTO

			return new Cliente() // RETORNA O CLIENTE
			{
				Cpf = cpf,
				Nome = nome,
				DNascimento = dNascimento,
				DCadastro = DateTime.Now,
				UCompra = DateTime.Now,
				Sexo = sexo,
				Situacao = 'A'
			};
		}

		private static void ImpressaoControl(List<Cliente> lista)
        {
			string opcao;
			Cliente c;
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

				switch (opcao)
				{
					case "1": // INICIO
						Console.WriteLine(Client.Get(lista[0]));
						i = 1;
						break;
					case "2": // FIM
						Console.WriteLine(Client.Get(lista[lista.Count - 1]));
						i = lista.Count - 1;
						break;
					case "3": // PROXIMO
						Console.WriteLine(Client.Get(lista[i]));
						i++;
						break;
					case "4": // ANTERIOR
						i--;
						Console.WriteLine(Client.Get(lista[i]));
						break;

				}
			} while (i >= 0 && i <= (lista.Count - 1) && opcao != "0");
			
        }
		private static void atualizaCliente(Cliente c, List<Cliente> lista)
        {
			byte att;

			Console.WriteLine("Qual campo deseja atualizar:\n1 - Nome\n2 - Sexo\n3 - Data de Nascimento");
			att = byte.Parse(Console.ReadLine());

			switch (att)
            {
				case 1:
					c.Nome = lerNome(); // ATUALIZA SOMENTE NOME
					break;
				case 2:
					c.Sexo = lerSexo(); // ATUALIZA SOMENTE SEXO
					break;
				case 3:
					c.DNascimento = lerDNascimento(); // ATUALIZA DATA DE NASCIMENTO
					break;
			}
        }

		private static string lerCpf(List<Cliente> lista)
        {
			string cpf;

			do
			{ // LAÇO VERIFICA CPF SE É VALIDO E SE JÁ EXISTE NA LISTA DE CLIENTES
				Console.Write("Informe o CPF( Apenas números e com o dígito): ");
				cpf = Console.ReadLine();

				if (!Client.CpfIsValid(cpf))
					Console.WriteLine("CPF inválido");

				if (Client.CpfRepeat(lista, cpf))
					Console.WriteLine("CPF já cadastrado");

			} while (!(Client.CpfIsValid(cpf)) || (Client.CpfRepeat(lista, cpf)));
			       // VERIFICA SE CPF É VALIDO      VERIFICA SE CPF JÁ EXISTE NA LISTA

			return cpf;
		}
		private static string lerNome()
        {
			string nome;

			do
			{ // LAÇO PARA NÃO DEIXAR CAMPO VAZIO
				Console.Write("Informe o Nome: ");
				nome = Console.ReadLine();
			} while (nome == "");

			return nome;
		}

		private static char lerSexo()
        {
			char sexo;

			do
			{ // LAÇO VERIFICA SE O CAMPO É DIFERENTE DO SOLICITADO
				Console.Write("Infome o Sexo M - Masculino F - Feminino: ");
				sexo = char.Parse(Console.ReadLine().ToUpper());
			} while (sexo != 'M' && sexo != 'F');

			return sexo;
		}

		private static DateTime lerDNascimento()
        {
			DateTime dNascimento;
			CultureInfo CultureBr = new CultureInfo(name: "pt-BR"); // DATA NO FORMATO BRASILEIRO

			do
			{ // LAÇO VERIFICA A IDADE COM A DATA DE NASCIMENTO INFORMADA
				Console.Write("Informe sua Data de Nascimento: ");
				dNascimento = DateTime.ParseExact(Console.ReadLine(), "d", CultureBr); // CONVERTE DATA BRASILEIRA PRA INGLESA;
			} while (!Client.CheckAge(dNascimento));
			return dNascimento;
		}
	}
}
