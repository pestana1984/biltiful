using Model;
using Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBil
{
	class Program
	{
		static void Main(string[] args)
		{
			Cliente c;
			List<Cliente> listaCliente = new List<Cliente>();
			byte op;
			string buscar;

            do
            {
				op = Menu();

				Console.Clear();

				switch (op)
                {
					case 1: // INSERIR

						c = lerCliente();
						listaCliente.Add(c);
						listaCliente = listaCliente.OrderBy(x => x.Nome).ToList();

						break;
					case 2: // IMPRIMIR
						listaCliente.ForEach(x => Console.WriteLine(Client.GetClient(x)));
						break;
					case 3: // BUSCAR
						Console.Write("Informe o Cliente que deseja buscar: ");
						buscar = Console.ReadLine();
						c = Client.SearchClient(listaCliente, buscar);
						if (c != null)
							Console.WriteLine(Client.GetClient(c));
						else
							Console.WriteLine("Cliente não encontrado");
						break;
                }
            } while (op != 0);
		}

		private static byte Menu()
        {
			string opcao;
			byte op;
			Console.WriteLine("\n------->>> MENU <<<-------");
			Console.WriteLine("\n1 - Inserir Cliente" +
								"\n2 - Imprimir Clientes" +
								"\n3 - Buscar Cliente" +
								"\n0 - Sair" +
								"\n\n--------------------------");
			opcao = Console.ReadLine();
			if (byte.TryParse(opcao, out op))
				return op;
			return Menu();
        }

		private static Cliente lerCliente()
        {
			string cpf, nome;
			char sexo;
			DateTime dNascimento;

			do {
				Console.Write("Informe o CPF: ");
				cpf = Console.ReadLine();
			} while (!Client.CpfIsValid(cpf));

            do
            {
				Console.Write("Informe o Nome: ");
				nome = Console.ReadLine();
            } while (nome == "");

            do
            {
				Console.Write("Infome o Sexo M - Masculino F - Feminino: ");
				sexo = char.Parse(Console.ReadLine().ToUpper());
            } while (sexo != 'M' && sexo != 'F');

            do
            {
				Console.Write("Informe sua Data de Nascimento: ");
				dNascimento = DateTime.Parse(Console.ReadLine());
			} while (!Client.CheckAge(dNascimento));

			return new Cliente()
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
	}
}
