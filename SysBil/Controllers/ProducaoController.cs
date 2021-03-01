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

            string cbarra;
            do
            {
                Console.WriteLine("Informe o Código de Barras: ");
                cbarra = Console.ReadLine();
            } while (!ProdutoController.LocalizarProduto(cbarra));
            
            //Localizar produto para validação

            Console.Write("Informe a quantidade a ser produzida: ");
            int qtd = int.Parse(Console.ReadLine());

            int quantidadeMP = 3;
            string[] idMp = new string[3] {" ", " ", " "};
            double[] qtdMP = new double[3] {0, 0, 0};
            string decisao = "s";
            

            for(int i = 0; i < quantidadeMP; i++)
            {
                do
                {
                    Console.Write("Informe o Id da matéria prima que deseja utilizar: ");
                    idMp[i] = Console.ReadLine();
                } while (!MPrimaController.LocalizarMPrima(idMp[i]));
                
                Console.Write("Informe a quantidade de matéria prima: ");
                qtdMP[i] = double.Parse(Console.ReadLine());

                if(i < 2)
                {
                    Console.Write("Deseja inserir mais matéria prima (s/n): ");
                    decisao = Console.ReadLine().ToLower();
                }
                if (decisao.Equals("n"))break;
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
                idSeque = listProdtion[i].Id.ToString().PadLeft(5, '0');
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
        
        public static void ExcluirProducao() 
        {
            List<Producao> listaProducao;
            int idProducao;

            do
            {
                Console.Write("Informe o ID da Producao que deseja excluir: ");
            }while(!int.TryParse(Console.ReadLine(), out idProducao));


            if (LocalizarProducao(idProducao))
            {
                string continuar;
				Console.Write("Tem certeza dessa exclusão?? s/n: ");
                continuar = Console.ReadLine();
                if(continuar.ToLower() == "s")
                {
                    listaProducao = ConverterParaLista();

                    if (listaProducao.Remove(listaProducao.Find(producao => producao.Id.Equals(idProducao))))
                    {
                        EscrevendoArquivo(listaProducao);
						Console.WriteLine("Registro de produção excluido com sucesso!!!");
                    }
                }
                else
					Console.WriteLine("Exclusão cancelada.");
            }
        }
        
    }
}
