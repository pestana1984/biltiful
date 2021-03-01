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
            List<Cliente> listaCliente = new List<Cliente>();
            MenuCliente(listaCliente);
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
        
        static void MenuCliente(List<Cliente> lista)
        {
            string opcao;
            ClienteController.ReadFile(lista);
            do
            { // MENU
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

                Console.Clear();

                switch (opcao)
                {
                    case "1":
                        lista.Add(ClienteController.Register(lista)); // CRIA CLIENTE E ADICIONA NA FILA
                        lista = lista.OrderBy(x => x.Nome).ToList(); // ORDENA FILA EM ORDEM ALFABETICA
                        ClienteController.WriteFile(lista); // ADICINA LISTA A FILA
                        break;
                    case "2":
                        lista.ForEach(x => Console.WriteLine(ClienteController.Get(x))); // IMPRIMI LISTA DE CLIENTES
                        break;
                    case "3":
                        ClienteController.ControlPrint(lista); // IMPRIMI LISTA COM CONTROLE DO CLIENTE
                        break;
                    case "4":
                        Cliente c = ClienteController.Search(lista); // PROCURA CLIENTE PELO NOME
                        if (c != null) // SE EXISTIR IMPRIME
                            Console.WriteLine(ClienteController.Get(c));
                        break;
                    case "5":
                        ClienteController.Update(lista); // ATUALIZA CAMPO DE CLIENTE
                        break;
                    case "6":
                        ClienteController.Delete(lista); // DELETA CLIENTE LOGICAMENTE
                        break;
                }
            } while (opcao != "0");
        }
    }
}
