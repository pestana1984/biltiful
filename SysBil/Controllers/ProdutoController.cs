using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Globalization;
using System.IO;

namespace Controllers
{
    public class ProdutoController
    {
        public static void EscrevendoArquivo(List<Produto> listP)
        {
            string nome20pos, linha = "";
            string uvenda, dcadastro, value6Pos;
            for (int i = 0; i < listP.Count; i++)
            {
                nome20pos = listP[i].Nome.PadRight(20, ' ');
                value6Pos = listP[i].Vvenda.ToString().PadRight(6, ' ');
                uvenda = listP[i].UVenda.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcadastro = listP[i].DCadastro.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                linha += listP[i].CBarras + nome20pos + value6Pos + uvenda + dcadastro + listP[i].Situacao.ToString() + "\n";
            }

            using (StreamWriter fileWrite = new StreamWriter(@"C:\Users\55169\Desktop\projeto\biltiful\files\Cosmetico.txt"))
            {
                fileWrite.WriteLine(linha);
                fileWrite.Close();
            }
        }
        public static Produto Cadastrar()
        {
            #region Barra
            //Gerador de código de barras para produto
            //789_________1 -> digito -> formato
            string[] valores = new string[13];
            valores[0] = "7";
            valores[1] = "8";
            valores[2] = "9";
            
            

            //entrada de valores aleatórios
            Random randNum = new Random();
            int num;
            for (int i = 3; i < 12; i++)
            {
                num = randNum.Next(0, 9);
                valores[i] = num.ToString();
            }

            //soma de números impar
            int soma1 = 0, soma2 = 0, soma3 = 0, multiplo=0 ;
            for (int i = 0; i < 12; i++)
            {

                if (!(i % 2 == 0))
                {
                    soma1 += int.Parse(valores[i]);
                }
                else
                {
                    soma2 += int.Parse(valores[i]);
                }
            }
            //calculo gerador de digito
            soma1 = soma1 * 3;
            soma3 = soma1 + soma2;
            multiplo = soma3 % 10;
            multiplo = 10 - multiplo;
            valores[12] = multiplo.ToString();
            #endregion

            string cbarras="";
            foreach (string v in valores) cbarras += v;

            string nome;
            do
            {
                Console.Write("Informe o nome (máximo 20 caracteres): ");
                nome = Console.ReadLine();

            } while (nome.Length > 20);

            double valueProduct;
            do
            {
                Console.WriteLine("Informe o valor de venda: ");
                valueProduct = double.Parse(Console.ReadLine());
            } while (valueProduct <= 0 || valueProduct >= 1000);

            //leitura da data
            bool deuCerto = false;
            Console.Write("Informe data da última venda (dd/MM/yyyy): ");
            DateTime uvenda;// = DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture);
            do
            {
                if (DateTime.TryParse(Console.ReadLine(), out uvenda))
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

            //instancia de obj produto
            Produto produto = new Produto(cbarras, nome, valueProduct, uvenda, dcadastro, situacao);

            return produto;
        }

    }
}
