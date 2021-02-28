using Controllers;
using Models;
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
            Menu();
            Console.ReadKey();          
        }
        static void Menu()
        {
            Venda venda = new Venda();
            List<Produto> produtos = new List<Produto>();
            List<Venda> vendas = new List<Venda>();
            string resposta;

            do
            {
                Console.WriteLine("1)Cadastrar produto");
                Console.WriteLine("2)Cadastrar venda");
                Console.WriteLine("0)Sair");
                resposta = Console.ReadLine();

                switch (resposta)
                {
                    case "1":
                        VendasController.CadastrarVenda(vendas);
                        break;

                    case "2":
                        VendasController.Localizar(vendas);
                        break;

                    case "3":
                        break;

                    case "4":
                        break;
                }
            } while (resposta != "0");

        }
        
        

       
    }
}
