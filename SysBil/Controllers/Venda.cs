using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    class Venda
    { 
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

            Console.WriteLine("Informe o Id do produto que vai ser vendido: "); id = int.Parse(Console.ReadLine());
            Console.WriteLine("Informe o nome do produto que esta sendo comprado: "); nome = Console.ReadLine();
            Console.WriteLine("Informe o valor do produto a ser vendido"); valor = double.Parse(Console.ReadLine());
            produto = new Produto
            {
                Id = id,
                Nome = nome,
                Vvenda = valor
            };

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
