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
			Venda venda = new Venda();

			Produto produto;

			string resposta;
			do
			{
				Console.WriteLine("1)Cadastrar venda");	
				Console.WriteLine("2)Localizar venda");
				Console.WriteLine("0)Sair");

				resposta = Console.ReadLine();
                


				switch (resposta)
				{
					case "1":
						produto = venda.CadastrarVenda();
						break;

					case "2":
						venda.Localizar();
						break;
				}
					
								
			} while (resposta != "0");

			

			Console.ReadKey();
			
		}
	}
}
