using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBil
{
    class Venda
    {
        public int Id { get; set; }

        public string Produto { get; set; }

        public int Qtd { get; set; }

        public double Vunitario { get; set; }

        public double Titem { get; set; }

        public Produto CadastrarVenda()
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
                Console.WriteLine("O cliente ainda não está cadastrado!");
                //CHAMADA METODO LUIZ DENTRO DO ELSE
            }
            else
            {
                Console.WriteLine("O cliente ja está cadastrado!\n");                
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

                contador++;

                if(contador != 3)
                {
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

        public void Localizar()
        {

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

        }

    }
}
