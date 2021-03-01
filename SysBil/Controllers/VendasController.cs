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
        public static Venda CadastrarVenda(List<Inadimplente> listaDeInadimplentes)
        {
            
            Cliente client = new Cliente();
            string buscaCpf;
            
            List<ItensVenda> listaItemVendas = new List<ItensVenda>();

            Console.WriteLine("\nInforme o cpf do cliente: \n");
            buscaCpf = Console.ReadLine();
            Inadimplente iNada = new Inadimplente()
            {
                Cpf = long.Parse(buscaCpf)
            };

            if (client.Cpf == buscaCpf)
            {
                if (inadimplenteController.VerificarInadimplente(iNada, listaDeInadimplentes))
                {
                    Console.WriteLine("Cliente inadimplente!");
                    Console.ReadKey();
                    return null;
                }
                           
            }
            else
            {
                Console.WriteLine("\n O Cliente nao esta cadastrado!\n");
                //LUIZ CHAMAR O CADASTRO DO CLIENTE
            }

            int id = 0;
            int contador = 0;
            string nome, resposta = "";
            int quantidade;
            double vunitario, somaTotal = 0, precoItens;

            do
            {
                if (contador == 0)
                {
                    /*for (int i = 0; i < 999; i++ )
                    {                       
                        
                        break;
                    }*/

                    Console.WriteLine("O id da compra é: ");
                    id = int.Parse(Console.ReadLine());

                }

                Console.WriteLine("Informe o nome do produto: "); nome = Console.ReadLine();

                do
                {
                    Console.WriteLine("Informe a quantidade: "); quantidade = int.Parse(Console.ReadLine());
                    if (quantidade > 999)
                    {
                        Console.WriteLine("\nVocê só pode levar 999 itens do mesmo produto\n");
                    }
                } while (quantidade > 999 && quantidade < 0);


                Console.WriteLine("Informe o valor unitario: "); vunitario = double.Parse(Console.ReadLine());

                precoItens = quantidade * vunitario;
                listaItemVendas.Add(new ItensVenda { Qtd = quantidade, Vunitario = vunitario, Titem = precoItens, Produto = nome });

                if (contador < 3)
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

            return new Venda()
            {
                Id = id,
                Data = DateTime.Now,
                ClienteCpf = buscaCpf,
                ListaItensVendas = listaItemVendas,
                ValorTotal = somaTotal
            }; 
        }

        public static void Localizar(List<Venda> acharVenda)
        {
            

            int id;

            Console.WriteLine("Informe o id da compra: ");
            id = int.Parse(Console.ReadLine());

            Venda encontrada = acharVenda.Find(venda => venda.Id.Equals(id));
           
            

            Console.WriteLine(encontrada);



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
                    vendaSB.Append(itemVenda.Produto.PadRight(5, ' '));
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

                Venda novaVenda = new Venda {Id= int.Parse(id), ClienteCpf = clienteCpf , Data =DateTime.ParseExact(data, "d", new CultureInfo(name: "pt-BR")), ValorTotal = double.Parse(valorTotal)}; 
                string[] itemProdutos = new string[3];
                
                for (int i = 0,j = 34; i < 3; i++,j+=22)
                {
                    itemProdutos[i] = venda.Substring(j, 22);
                }

                List<ItensVenda> ListItensVendas = new List<ItensVenda>();
                  
                foreach (var objeto in itemProdutos)
                {
                    string produto = objeto.Substring(0, 5).Trim();
                    string qtd = objeto.Substring(5, 3).Trim();
                    string vunitario = objeto.Substring(8, 6).Trim();
                    string titem = objeto.Substring(14, 8).Trim();

                
                   ListItensVendas.Add(new ItensVenda {Produto = produto, Qtd =int.Parse(qtd), Vunitario = double.Parse(vunitario), Titem = double.Parse(titem)});
                   
                }
                novaVenda.ListaItensVendas = ListItensVendas;
                vendas.Add(novaVenda);
            }

            return vendas;
        }
    }
}
