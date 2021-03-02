using Model;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class VendasController
    {
        public static Venda CadastrarVenda(List<Inadimplente> listaDeInadimplentes, List<Cliente> listaClientes, int id )
        {
           
           
            
            List<ItensVenda> listaItemVendas = new List<ItensVenda>();            
            Cliente cliente =  ClienteController.Search(listaClientes);
            if (cliente != null)
            {
                Console.WriteLine(ClienteController.Get(cliente));
                Inadimplente iNada = new Inadimplente()
                {
                    Cpf = long.Parse(cliente.Cpf)
                };

                if (ClienteController.CpfRepeat(listaClientes, cliente.Cpf))
                {
                    if (inadimplenteController.VerificarInadimplente(iNada, listaDeInadimplentes))
                    {
                        Console.WriteLine("Cliente inadimplente!");
                        Console.ReadKey();
                        return null;
                    }

                }
            }            
            else
            {
                Console.WriteLine("\n O Cliente não esta cadastrado!\n");
                return null;
            }


            id++;
            int contador = 0;
            string nome, resposta = "";
            int quantidade;
            double vunitario, somaTotal = 0, precoItens;

            do
            {
                if (contador == 0)
                {                    
                    Console.WriteLine($"O id da compra é: {id}" );

                    
                }

                Console.WriteLine("Informe o Id do produto: "); nome = Console.ReadLine();

                Produto produto = ProdutoController.RetornarProduto(nome);

                Console.WriteLine(produto + "\n");
                do
                {
                    Console.WriteLine("Informe a quantidade: "); quantidade = int.Parse(Console.ReadLine());
                    if (quantidade > 999)
                    {
                        Console.WriteLine("\nVocê só pode levar 999 itens do mesmo produto\n");
                    }
                } while (quantidade > 999 && quantidade < 0);


                
                vunitario = produto.Vvenda;

                precoItens = quantidade * vunitario;
                listaItemVendas.Add(new ItensVenda { Qtd = quantidade, Vunitario = vunitario, Titem = precoItens, Produto = nome });

                if (contador < 2)
                {
                    Console.WriteLine("\nQuer informar mais produtos? S ou N");
                    resposta = Console.ReadLine();
                    if (contador == 1 && resposta.ToUpper() == "N")
                    {                      
                        listaItemVendas.Add(new ItensVenda { Qtd = 0, Vunitario = 0, Titem = 0, Produto = "" });
                    }
                    else if(contador ==  0 && resposta.ToUpper() == "N")
                    {
                        listaItemVendas.Add(new ItensVenda { Qtd = 0, Vunitario = 0, Titem = 0, Produto = "" });
                        listaItemVendas.Add(new ItensVenda { Qtd = 0, Vunitario = 0, Titem = 0, Produto = "" });
                    }                  

                }
                else
                {
                    Console.WriteLine("\nEsgotou seu limite de compra de produtos!\n");
                    Console.WriteLine("\nCompra finalizada! Aperta qualquer tecla para voltar para o menu!\n");
                    Console.ReadKey();
                }
                contador++;

                somaTotal += precoItens;

            } while (resposta.ToUpper() != "N" && contador < 3);

            Console.WriteLine("\nO valor final é de: " + somaTotal);
            Console.WriteLine("\nVenda finalizada!!!");

            return new Venda()
            {
                Id = id,
                Data = DateTime.Now,
                ClienteCpf = cliente.Cpf,
                ListaItensVendas = listaItemVendas,
                ValorTotal = somaTotal
            }; 
        }

        public static Venda Localizar(List<Venda> acharVenda)
        {
            int id;

            Console.WriteLine("Informe o id da compra: ");
            id = int.Parse(Console.ReadLine());

            Venda encontrada = acharVenda.Find(venda => venda.Id.Equals(id));

            Console.WriteLine("-------------> LOCALIZANDO ------------->>>>");

            Console.WriteLine(encontrada);
            Console.WriteLine("APERTE QUALQUER TECLA PARA CONTINUAR\n");
            Console.ReadKey();
            return encontrada;
        }
        public static void ExcluirVenda(List<Venda> encontrar, FileManipulator arquivoVenda)
        {             
            encontrar.Remove(Localizar(encontrar));
            Console.WriteLine("Venda excluida do sistema!");
            FileManipulatorController.EscreverNoArquivo(arquivoVenda, ConverterParaSalvar(encontrar));

        }

        public static void PrintVenda(List<Venda> encontrouVenda)
        {
            int cont=0;
            string resposta;
            do
            {                 
                Console.WriteLine("\n------->>> Vendas SysBil <<<-------");
                Console.WriteLine("\n1 - Inicio da fila" +
                            "\n2 - Fim da fila" +
                            "\n3 - Próximo da fila" +
                            "\n4 - Anterior da fila" +
                            "\n0 - Encerrar impressão");

                resposta = Console.ReadLine();

                switch (resposta)
                {
                    case "1": // inicio                         
                        Console.WriteLine(encontrouVenda[0]);
                        cont = 0; 
                        break;

                    case "2": // fim 
                        Console.WriteLine(encontrouVenda[encontrouVenda.Count-1]);
                        cont = encontrouVenda.Count - 1;
                        break;

                    case "3": // próximo 
                            cont++;
                        if (cont < encontrouVenda.Count)
                        {
                            Console.WriteLine(encontrouVenda[cont]);
                        }                                                
                        break;

                    case "4": // anterior 
                        cont--;
                        if (cont >= 0)
                        {
                            Console.WriteLine(encontrouVenda[cont]);
                        }                       
                        break;

                    case "5": //encerrar  

                        break;
                }

            } while (resposta != "0");            
        }
        

        public static string[] ConverterParaSalvar(List<Venda> listaVenda)
        {

            StringBuilder vendaSB = new StringBuilder();
            listaVenda.ForEach(venda =>
            {
                vendaSB.Append(venda.Id.ToString().PadRight(5, ' '));
                vendaSB.Append(venda.ClienteCpf.PadRight(11, ' '));
                vendaSB.Append(venda.Data.ToString("dd/MM/yyyy").PadRight(10, ' '));
                vendaSB.Append(venda.ValorTotal.ToString().PadRight(8, ' '));

                venda.ListaItensVendas.ForEach(itemVenda => 
                {
                    vendaSB.Append(itemVenda.Produto.PadRight(13, ' '));
                    vendaSB.Append(itemVenda.Qtd.ToString().PadRight(3, ' '));
                    vendaSB.Append(itemVenda.Vunitario.ToString().PadRight(6, ' '));
                    vendaSB.Append(itemVenda.Titem.ToString().PadRight(8, ' '));
                });

                vendaSB.AppendLine();

            });

            return vendaSB.ToString().Split('\n');
        }

        public static List<Venda> ConverterParaList(string[] LerDados)
        {
            List<Venda> vendas = new List<Venda>();
            foreach (var venda in LerDados)
            {
                string id = venda.Substring(0, 5).Trim();
                string clienteCpf = venda.Substring(5,11).Trim();
                string data = venda.Substring(16, 10).Trim();
                string valorTotal = venda.Substring(26,8).Trim();

                Venda novaVenda = new Venda {Id= int.Parse(id), ClienteCpf = clienteCpf , Data = DateTime.ParseExact(data, "d", new CultureInfo(name: "pt-BR")), ValorTotal = double.Parse(valorTotal)}; 
                string[] itemProdutos = new string[3];
                
                for (int i = 0,j = 34; i < 3; i++,j+=30)
                {
                    itemProdutos[i] = venda.Substring(j, 30);
                }

                List<ItensVenda> ListItensVendas = new List<ItensVenda>();
                  
                foreach (var objeto in itemProdutos)
                {

                    string produto = objeto.Substring(0, 13).Trim();
                    string qtd = objeto.Substring(13, 3).Trim();
                    string vunitario = objeto.Substring(16, 6).Trim();
                    string titem = objeto.Substring(22, 8).Trim();

                
                   ListItensVendas.Add(new ItensVenda {Produto = produto, Qtd =int.Parse(qtd), Vunitario = double.Parse(vunitario), Titem = double.Parse(titem)});
                   
                }
                novaVenda.ListaItensVendas = ListItensVendas;
                vendas.Add(novaVenda);
            }
            return vendas;
        }
    }
}
