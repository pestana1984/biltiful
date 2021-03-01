using Controllers;
using Model;
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

            FileManipulator arquivoInadimplente = new FileManipulator { Path = @"C:\Users\talit\source\repos\biltiful\SysBil\Arquivos", Name = "Risco.dat" };
            FileManipulatorController.InicializarArquivo(arquivoInadimplente);
            List<Cliente> listaCliente = new List<Cliente>();
            ClienteController.ReadFile(listaCliente);

            int op;
            do
            {
                Console.Clear();

                Console.WriteLine("\n------->>> MENU PRINCIPAL <<<-------");
                Console.WriteLine("\n1 - Menu cliente." +
                                    "\n2 - Menu Vendas." +
                                    "\n3 - Menu inadimplentes." +                                    
                                    "\n0 - Sair" +
                                    "\n\n-----------------------------------");                              
                Console.WriteLine("\nDigite a opção desejada");
                op = int.Parse(Console.ReadLine());
                Console.Clear();
                switch (op)
                {
                    case 1:
                        MenuCliente(listaCliente);
                        break;
                    case 2:
                        MenuVendas(arquivoVenda, arquivoInadimplente, listaCliente);
                        break;
                    case 3:
                        MenuInadimplentes(arquivoVenda, arquivoInadimplente);
                        break;
                    default:
                        Console.WriteLine("Opção Invalida!");
                        break;
                }

            } while (op!=0);          
        }
        static void MenuVendas(FileManipulator arquivoVenda, FileManipulator arquivoInadimplente, List<Cliente>listaCliente)
        {
            Venda venda = new Venda();
            List<Produto> produtos = new List<Produto>();
            List<Venda> vendas = new List<Venda>();
            List<Inadimplente> riscos = new List<Inadimplente>();

            string resposta;

            do
            {
                Console.WriteLine("\n------->>> Vendas SysBil <<<-------");
                Console.WriteLine("\n1 - Cadastrar Vendas." +
                                    "\n2 - Localizar uma venda." +
                                    "\n3 - Excluir uma venda." +
                                    "\n4 - Imprimir uma venda." +                                    
                                    "\n0 - Sair" +
                                    "\n\n--------------------------");                
                resposta = Console.ReadLine();

                switch (resposta)
                {
                    case "1":

                        riscos = inadimplenteController.ConverterParaList(FileManipulatorController.LerArquivo(arquivoInadimplente));
                        vendas = VendasController.ConverterParaList(FileManipulatorController.LerArquivo(arquivoVenda));
                        int id;
                        if (vendas.Count == 0)
                        {
                            id = 0;
                        }
                        else
                        {
                            id = vendas.Last().Id;
                        }                       
                        Venda novaVenda = VendasController.CadastrarVenda(riscos, listaCliente,id);
                        if (novaVenda != null)
                        {
                            vendas.Add(novaVenda);
                            FileManipulatorController.EscreverNoArquivo(arquivoVenda, VendasController.ConverterParaSalvar(vendas));
                        }
                        
                        break;

                    case "2":
                        VendasController.Localizar(vendas);
                        break;

                    case "3":
                        VendasController.ExcluirVenda(vendas, arquivoVenda);
                        break;

                    case "4":
                        vendas = VendasController.ConverterParaList(FileManipulatorController.LerArquivo(arquivoVenda));
                        VendasController.PrintVenda(vendas);

                        Console.ReadKey();
                        break;


                    default:
                        Console.WriteLine("\nOpção inválida\n");
                        break;
                }
            } while (resposta != "0");

        }


        static void MenuCliente(List<Cliente> lista)
        {
            List<Cliente> listaCliente = new List<Cliente>();

            string opcao;
            
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

                        ClienteController.Register(lista); // CRIA CLIENTE E ADICIONA NA FILA                        
                        break;
                    case "2":
                        if (lista.Count > 0)
                        {
                            lista.ForEach(x => Console.WriteLine(ClienteController.Get(x))); // IMPRIMI LISTA DE CLIENTES
                        }
                        else
                        {
                            Console.WriteLine("Lista Vazia!!!");
                        }
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
        static void MenuInadimplentes(FileManipulator arquivoVenda, FileManipulator arquivoInadimplente)
        {
            List<Inadimplente> riscos = new List<Inadimplente>();
            int opc;
            do
            {
                Console.WriteLine(">>>Inadimplentes SysBil <<< ");
                Console.WriteLine("\n1)Cadastrar Inadimplente.");
                Console.WriteLine("\n2)Localizar");
                Console.WriteLine("\n3)Excluir.");
                Console.WriteLine("\n0)SAIR.");
                Console.WriteLine("Digite uma opção valida");
                opc = int.Parse(Console.ReadLine());
                switch (opc)
                {
                    case 1:
                        Inadimplente novoInadimplente = inadimplenteController.CadastrarInadimplentes();
                        riscos = inadimplenteController.ConverterParaList(FileManipulatorController.LerArquivo(arquivoInadimplente));
                        if (!inadimplenteController.VerificarInadimplente(novoInadimplente, riscos))
                        {
                            riscos.Add(novoInadimplente);
                            FileManipulatorController.EscreverNoArquivo(arquivoInadimplente, inadimplenteController.ConverterParaSalvar(riscos));
                        }
                        break;

                    case 2:
                        riscos = inadimplenteController.ConverterParaList(FileManipulatorController.LerArquivo(arquivoInadimplente));
                        Console.WriteLine("Informe o CPF para localizar: ");
                        string cpf = Console.ReadLine();
                        Inadimplente devedor = new Inadimplente() { Cpf = long.Parse(cpf) };
                        if (inadimplenteController.VerificarInadimplente(devedor, riscos))
                        {
                            Console.WriteLine("Este CPF está inadimplente!!!!");
                        }                  
                        break;

                    case 3:
                        riscos = inadimplenteController.ConverterParaList(FileManipulatorController.LerArquivo(arquivoInadimplente));
                        riscos = inadimplenteController.DeletarInadimplente(riscos);
                        FileManipulatorController.EscreverNoArquivo(arquivoInadimplente, inadimplenteController.ConverterParaSalvar(riscos));
                        break;

                    default:
                        Console.WriteLine("\nOpção inválida\n");
                        break;
                }
            } while (opc != 0);

        }
    }   
}
