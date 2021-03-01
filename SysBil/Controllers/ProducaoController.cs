using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class ProducaoController
    {
        public static int idSequencial = 0;
        public static Producao Cadastrar()
        {

            idSequencial++;
            if (idSequencial > 99999) Console.WriteLine("Id fora dos limites suportados");

            bool deuCerto = false;
            Console.Write("Informe data da produção (dd/MM/yyyy): ");
            DateTime dproducao;
            do
            {
                if (DateTime.TryParse(Console.ReadLine(), out dproducao))
                {
                    deuCerto = true;
                }
            } while (!deuCerto);

            Console.WriteLine("Informe o Código de Barras: ");
            string cbarra = Console.ReadLine();
            //Localizar produto para validação

            Console.WriteLine("Informe a quantidade a ser produzida: ");
            int qtd = int.Parse(Console.ReadLine());
            //ERRO----------------ERRO
            double[] qtdMP = new double[3];
            string decisao;
            int cont = 1;
            do
            {
                Console.WriteLine("Informe a quantidade de matéira prima: ");
                qtdMP[cont-1] = double.Parse(Console.ReadLine());
                if(cont < 3)Console.WriteLine("Deseja inserir mais matéria prima (s/n):");
                decisao = Console.ReadLine();
                cont++;                
            } while (decisao != "n" || cont == 3);


            string[] idMp = new string[3];
            for (int i = 0; i < cont; i++)
            {
                do
                {
                    Console.WriteLine("Informe o Código da Matéria Prima: ");
                    idMp[i] = Console.ReadLine();
                } while (!MPrimaController.LocalizarMPrima(idMp[i]));
            }


            //guardando data local
            DateTime dabertura = DateTime.Now;
            Console.Write("Data da abertura da produção: " + dabertura + "\n");


            //instancia de obj materia prima
            Producao producao = new Producao(idSequencial, dabertura, cbarra, qtd, idMp, qtdMP);

            return producao;
        }
        public static void EscrevendoArquivo(List<Producao> listProdtion)
        {
            string linha = "";
            string idSeque, dproducao, produto, qtd, mprima, qtdMP;
            for (int i = 0; i < listProdtion.Count; i++)
            {
                idSeque = listProdtion[i].Id.ToString().PadRight(5, ' ');
                dproducao = listProdtion[i].DProducao.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                produto = listProdtion[i].CBarras;
                qtd = listProdtion[i].Qtd.ToString().PadRight(3, ' ');
                mprima = listProdtion[i].IdMP[0].ToString().PadRight(6, ' ') + listProdtion[i].IdMP[1].ToString().PadRight(6, ' ') + listProdtion[i].IdMP[2].ToString().PadRight(6, ' ');
                qtdMP = listProdtion[i].QtdMP[0].ToString().PadRight(6, ' ') + listProdtion[i].QtdMP[1].ToString().PadRight(6, ' ') + listProdtion[i].QtdMP[2].ToString().PadRight(6, ' ');

                linha += idSeque + dproducao + produto + qtd + mprima + qtdMP + "\n";
            }

            using (StreamWriter fileWrite = new StreamWriter(@"C:\Users\55169\Documents\Projeto\Projeto Biltiful\biltiful\files\Producao.txt"))
            {
                fileWrite.WriteLine(linha);
                fileWrite.Close();
            }
        }
    }
}
