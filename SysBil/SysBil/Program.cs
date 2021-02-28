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
            FileManipulator arquivoVenda = new FileManipulator {Path = @"C:\Users\talit\source\repos\biltiful\SysBil\Arquivos", Name = "Venda.dat"};
            FileManipulatorController.InicializarArquivo(arquivoVenda);
            Menu(arquivoVenda);
            Console.ReadKey();          
        }
        static void Menu(FileManipulator arquivoVenda)
        {
            Venda venda = new Venda();
            List<Produto> produtos = new List<Produto>();
            List<Venda> vendas = new List<Venda>();
            string resposta;

            do
            {
                Console.WriteLine(">>> Vendas SysBil <<< ");
                Console.WriteLine("\n1)Cadastrar Venda.");
                Console.WriteLine("\n2)Localizar uma venda.");                
                Console.WriteLine("\n3)Excluir uma venda.");
                Console.WriteLine("\n4)Imprimir uma venda.");
                Console.WriteLine("\n0)SAIR.");

                resposta = Console.ReadLine();

                switch (resposta)
                {
                    case "1":
                        vendas = VendasController.ConverterParaList(FileManipulatorController.LerArquivo(arquivoVenda));
                        vendas.Add(VendasController.CadastrarVenda());
                        FileManipulatorController.EscreverNoArquivo(arquivoVenda, VendasController.ConverterParaSalvar(vendas));
                        break;

                    case "2":
                        VendasController.Localizar(vendas);
                        break;

                    case "3":
                        break;

                    case "4":
                        vendas = VendasController.ConverterParaList(FileManipulatorController.LerArquivo(arquivoVenda));
                        vendas.ForEach(amostra=> Console.WriteLine(amostra));
                        Console.ReadKey();
                        break;

                   
                    default: Console.WriteLine("\nOpção inválida\n"); 
                        break;
                }
            } while (resposta != "0");

        }    
 
    }
}
