using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Venda
    {
        public int Id { get; set; }


        public DateTime Data { get; set; }
        public List<ItensVenda> ListaItensVendas { get; set; }

        public double ValorTotal { get; set; }

        public string ClienteCpf{ get; set; }

        public override string ToString()
        {
            string itemVendas = "";
            foreach (ItensVenda vend in ListaItensVendas)
            {
                itemVendas = itemVendas + vend.ToString();
            }
            return $"\n>>>>>DADOS DO CLIENTE<<<<<\nId: {Id}\nData: {Data.ToString("dd/MM/yyyy")}\n\n>>>>>ITENS COMPRADOS<<<<<{itemVendas}" +
                $"\nValor total da compra: {ValorTotal}\n"; //\nNome: {Produto}
        }

        /*public Produto CadastrarVenda()
        {

            Produto produto = new Produto();
            long cpf;
            int id;
            string nome;
            double valor;

            Console.WriteLine("Informe o CPF do cliente: ");
            cpf = long.Parse(Console.ReadLine());


            if (cpf != 48993591873)
            {
                Console.WriteLine("\nO cliente ainda não está cadastrado!");
                //CHAMADA METODO LUIZ DENTRO DO ELSE
            }
            else
            {
                Console.WriteLine("\nO cliente ja está cadastrado!\n");                
            }
            string resposta;
            int contador = 0;

            
            do
            {
               

                Console.WriteLine("Informe o Id do produto que vai ser vendido: "); id = int.Parse(Console.ReadLine());
                Console.WriteLine("Informe o nome do produto que esta sendo comprado: "); nome = Console.ReadLine();
                Console.WriteLine("Informe o valor do produto a ser vendido"); valor = double.Parse(Console.ReadLine());

                produto = new Produto
                {
                    Id = id,
                    Nome = nome, ////PEGAR COM PESSOAL DE PRODUTO (GRUPO 1)
                    Vvenda = valor
                };

                produtos.Add(produto);

                contador++;

                if(contador != 3)
                {
                    Console.WriteLine("Produto inserido com sucesso!\n");
                    Console.WriteLine("\nQuer inserir mais produtos? sim(s) ou não(n)");
                    resposta = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("\nJá atingiu o limite\n");
                    break;
                }
                
            } while (contador < 3 && resposta.ToLower() != "n");
           
            return produto;
        }

        public Produto Localizar(List<Produto> produtos)
        {
            int id_localizar;
            Produto prod = new Produto();

            if (produtos.Count == 0)
            {
                Console.WriteLine("\nInsira produtos na lista antes de querer localizar\n");                
            }
            else
            {
                Console.WriteLine("Informe o ID do produto que voce deseja localizar? ");
                id_localizar = int.Parse(Console.ReadLine());

                prod = produtos.Find(i => i.Id == id_localizar);

                if(prod != null)
                {
                    Console.WriteLine("\nO produto foi encontrado\n" + prod.ToString());
                }
                else
                {
                    Console.WriteLine("\nNão foi encontrado nenhum produto com esse ID\n");
                }
                
            }
            return prod;
        }

        public void Editar()
        {

        }

        public void Excluir()
        {

        }

        public void ImpressaoPorRegistro()
        {

        }

        public void Inadimplente()
        {

        }

        public void LimiteProduto()
        {

        }*/

    }
}
