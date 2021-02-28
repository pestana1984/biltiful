using System;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class MPrimaController
    {
        public static MPrima Cadastrar()
        {

            //Gerador de id para matéria prima
            //MP1234 -> formato | guardando resultados
            int[] valores = new int[100];
            Random randNum = new Random();
            int num;
            string id = " ";

            num = randNum.Next(1000, 9999);

            for (int i = 0; i < valores.Length; i++)
            {

                if (num != valores[i])
                {
                    id = "MP" + num;
                    valores[i] = num;
                }
                else
                {
                    num = randNum.Next(1000, 9999);
                    valores[i] = num;
                    id = "MP" + num;
                }
            }

            string nome;
            do
            {
                Console.Write("Informe o nome (máximo 20 caracteres): ");
                nome = Console.ReadLine();

            } while (nome.Length > 20);

            //leitura da data
            bool deuCerto = false;
            Console.Write("Informe data da última compra (dd/MM/yyyy): ");
            DateTime ucompra;// = DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture);
            do
            {
                if (DateTime.TryParse(Console.ReadLine(), out ucompra))
                {
                    deuCerto = true;
                }
            } while (!deuCerto);

            //guardando data local
            DateTime dcadastro = DateTime.Now;
            Console.Write("Data do cadastro: " + dcadastro + "\n");

            //leitura e teste de situação
            char situacao;
            do
            {
                Console.Write("Situação (A ou I): ");
                situacao = char.Parse(Console.ReadLine().ToUpper());
            } while (situacao != 'A' && situacao != 'I');

            //instancia de obj materia prima
            MPrima materiaPrima = new MPrima(id, nome, ucompra, dcadastro, situacao);

            return materiaPrima;
        }
    }
}
