using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class VendasController
    {
        public static void CadastrarVenda(List<Venda> vendaProdutos)
        {
            Cliente client = new Cliente();
            string buscaCpf;
            List<ItensVenda> listaItemVendas = new List<ItensVenda>();

            Console.WriteLine("\nInforme o cpf do cliente: \n");
            buscaCpf = Console.ReadLine();

            if (client.Cpf == buscaCpf)
            {
                Console.WriteLine("\nCliente ja esta cadastrado!\n");

                //ISABELA E IRWING VER SE É INADIMPLENTE
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
                listaItemVendas.Add(new ItensVenda { Qtd = quantidade, Vunitario = vunitario, Titem = precoItens, produto = nome });

                if (contador < 3)
                {
                    Console.WriteLine("\nQuer informar mais produtos? S ou N \n");
                    resposta = Console.ReadLine();

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

            Venda novaVenda = new Venda()
            {
                Id = id,
                Data = DateTime.Now,
                ClienteCpf = buscaCpf,
                ListaItensVendas = listaItemVendas,
                ValorTotal = somaTotal


            };
            vendaProdutos.Add(novaVenda);
            Console.WriteLine(novaVenda.ToString());

            return;
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
                vendaSB.Append(venda.Id);
                vendaSB.Append(venda.ClienteCpf);
                vendaSB.Append(venda.Data.ToString("dd/MM/yyyy"));
                vendaSB.Append(venda.ValorTotal);

                venda.ListaItensVendas.ForEach(itemVenda => 
                {
                    vendaSB.Append(itemVenda.produto);
                    vendaSB.Append(itemVenda.Qtd);
                    vendaSB.Append(itemVenda.Vunitario);
                    vendaSB.Append(itemVenda.Titem);
                });

                vendaSB.AppendLine();

            });

            return vendaSB.ToString().Split('\n');
        }




    }
}
