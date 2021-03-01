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
        
	    public static void LocalizarProducao()
        {
            bool itemEncontrado = false;
            
            int buscarId = 0;
            do
            {
                Console.Write("Informe o ID da produção prima para localizar: ");
            }
            while (!int.TryParse(Console.ReadLine(), out buscarId));

                List<Producao> listProducao = ConverterParaLista();

            foreach (var producao in listProducao)
            {
                if (producao.Id == buscarId)
                {
                    itemEncontrado = true;
                    Console.WriteLine(producao.ToString());
                    
                }
            }

            if (!itemEncontrado) Console.WriteLine("Produção não cadastrada");
            
        }

        public static bool LocalizarProducao(int buscarId)
        {
            bool itemEncontrado = false;
            List<Producao> listProducao = ConverterParaLista();

            foreach (var producao in listProducao)
            {
                if (producao.Id == buscarId)
                {
                    itemEncontrado = true;
                    Console.WriteLine(producao.ToString());
                    return itemEncontrado;

                }
            }

            if (!itemEncontrado) Console.WriteLine("Produção não cadastrada");
            return itemEncontrado;
        }
	    
	    public static List<Producao> ConverterParaLista()
        {
            List<Producao> listProducao = new List<Producao>();
            try
            {
                FileManipulator file = new FileManipulator() { Path = @"C:\Users\ferna\Google Drive\Estagio Five\Repositorio\biltiful\SysBil\files", Name = "Producao.txt" };
                string[] producaoArquivos = FileManupulatorController.LerArquivo(file);

                foreach (var arqProducao in producaoArquivos)
                {
                    if (arqProducao.Length == 67)
                    {
                        string id = arqProducao.Substring(0, 5);
                        string dProducao = arqProducao.Substring(5, 10);
                        string cBarrasProduto = arqProducao.Substring(15, 13);
                        string qtdProducao = arqProducao.Substring(28, 3);
                        string MPrima0 = arqProducao.Substring(31, 6);
                        string MPrima1 = arqProducao.Substring(37, 6);
                        string MPrima2 = arqProducao.Substring(43, 6);
                        string qtdMPrima0 = arqProducao.Substring(49, 6);
                        string qtdMPrima1 = arqProducao.Substring(55, 6);
                        string qtdMPrima2 = arqProducao.Substring(61, 6);

                        Producao producao = new Producao
                        {
                            Id = int.Parse(id),
                            DProducao = Convert.ToDateTime(dProducao),
                            CBarras = cBarrasProduto,
                            Qtd = int.Parse(qtdProducao),
                            IdMP = new string[] { MPrima0, MPrima1, MPrima2 },
                            QtdMP = new Double[] { double.Parse(qtdMPrima0), double.Parse(qtdMPrima1), double.Parse(qtdMPrima2) }

                        };

                        listProducao.Add(producao);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERRO!!!!: " + ex.Message);
                Console.ReadKey();
            }

            return listProducao;
        }
	    
	    public static void ImpressaoPorRegistro()
		{

			List<Producao> Produtos = ConverterParaLista();

			Console.WriteLine("Faça a escolha do item que deseja imprimir");
			int cont = 0;
			string escolha;
			do
			{
				Console.Clear();

				if ((cont > 0) && (cont < Produtos.Count() - 1))
					Console.WriteLine("A <- | -> P   I <-- | --> U :");
				if (cont == 0) Console.WriteLine("-> P  --> U :");
				if (cont == Produtos.Count() - 1) Console.WriteLine("<- A  <-- I :");
				Console.WriteLine("S para sair");

				if (cont <= Produtos.Count() - 1)
				{
					Console.WriteLine(Produtos[cont].ToString());
				}

				do
				{
					escolha = TratamentoString().ToUpper();
					if ((escolha != "A") && (escolha != "P") && (escolha != "I") && (escolha != "U") && (escolha != "S"))
					{
						Console.WriteLine("Digite apenas as opçoes que o menu dispoe no momento!!!");
					}

				}
				while ((escolha != "A") && (escolha != "P") && (escolha != "I") && (escolha != "U") && (escolha != "S"));

				if (escolha == "I")
				{
					cont = 0;
				}

				if ((escolha == "P") && (cont < Produtos.Count() - 1))
				{
					cont++;
				}
				else if ((escolha == "U") && (cont < Produtos.Count() - 1))
				{
					cont = Produtos.Count() - 1;
				}

				if ((escolha == "A") && (cont <= Produtos.Count() - 1) && (cont > 0))
				{
					cont--;
				}

				Console.ReadKey();
			}
			while (escolha != "S");

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
