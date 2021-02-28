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
            FileManipulator arquivoVenda = new FileManipulator {Path = @"C:\Users\Vitor Faccio\Desktop\Nova pasta\SysBil\Arquivos", Name = "Venda.dat"};
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
                Console.WriteLine("1)Cadastrar produto");
                Console.WriteLine("2)Cadastrar venda");
                Console.WriteLine("0)Sair");
                resposta = Console.ReadLine();

                switch (resposta)
                {
                    case "1":
                        vendas.Add(VendasController.CadastrarVenda());
                        FileManipulatorController.EscreverNoArquivo(arquivoVenda, VendasController.ConverterParaSalvar(vendas));
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
