using Model;
using Models;
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

		public static void MenuProducao()
		{
			int escolha = -1;
			Producao producao = new Producao();
			List<Producao> listaProducao = new List<Producao>();
			do
			{
				Console.Clear();
				Console.WriteLine(" 1 - Cadastrar");
				Console.WriteLine(" 2 - Localizar");
				Console.WriteLine(" 3 - Impressão por Registro");
				Console.WriteLine(" 4 - Excluir");
				Console.WriteLine(" 0 - Para sair");
				Console.WriteLine();
				Console.Write("Escolha uma das opções acima: ");
				if (int.TryParse(Console.ReadLine(), out escolha))
				{
					if (escolha < 0 || escolha >= 6)
					{
						Console.WriteLine("Digite um Valor entre 1 e 5");
						Console.ReadKey();
					}
				}
				else
				{
					Console.WriteLine("Digite um Valor entre 1 e 5");
					escolha = -1;
					Console.ReadKey();
				}
				switch (escolha)
				{
					case 1:
						producao = Cadastrar();
						listaProducao.Add(producao);
						EscrevendoArquivo(listaProducao);
						Console.ReadKey();
						break;                                               
					case 2:
						LocalizarProducao();
						Console.ReadKey();
						break;
					case 3:
						ImpressaoPorRegistro();
						Console.ReadKey();
						break;
					case 4:
						ExcluirProducao();
						Console.ReadKey();
						break;

				}
			} while (escolha != 0);
			Console.WriteLine("Precione Qualquer Tecla para Finalizar");
			Console.ReadKey();
		}
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
				else Console.Write("Informe data da produção (dd/MM/yyyy): ");
			} while (!deuCerto);

			string cbarra;
			do
			{
				Console.Write("Informe o Código de Barras: ");
				cbarra = Console.ReadLine();
			} while (!ProdutoController.LocalizarProduto(cbarra));

			//Localizar produto para validação
			int qtd = -1;
			do
			{
				Console.Write("Informe a quantidade a ser produzida: ");

				try
				{
					qtd = int.Parse(Console.ReadLine());
				}
				catch (Exception)
				{

					Console.WriteLine("Insira um numero");
				}
			} while (qtd <= 0);

			int quantidadeMP = 3;
			string[] idMp = new string[3] { " ", " ", " " };
			double[] qtdMP = new double[3] { 0, 0, 0 };
			string decisao = "s";

			//Localizar materia prima para validação	
			for (int i = 0; i < quantidadeMP; i++)
			{
				do
				{
					Console.Write("Informe o Id da matéria prima que deseja utilizar: ");
					idMp[i] = Console.ReadLine();
				} while (!MPrimaController.LocalizarMPrima(idMp[i]));

				do
				{
					Console.Write("Informe a quantidade de matéria prima: ");
					try
					{
						qtdMP[i] = double.Parse(Console.ReadLine());
					}
					catch (Exception)
					{

						Console.WriteLine("Insira um numero maior 0: ");
					}

				} while (qtdMP[i] <= 0);

				if (i < 2)
				{

					do
					{
						Console.Write("Deseja inserir mais matéria prima (s/n): ");
						decisao = TratamentoString().ToLower();
					} while (decisao != "s" && decisao != "n");
				}
				if (decisao.Equals("n")) break;
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

			using (StreamWriter fileWrite = new StreamWriter(@"C:\Arquivos\Producao.dat"))
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
				Console.Write("Informe o ID da produção para localizar: ");
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
				FileManipulator file = new FileManipulator() { Path = @"C:\Arquivos\", Name = "Producao.dat" };
				string[] producaoArquivos = FileManipulatorController.LerArquivo(file);

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
		public static void ExcluirProducao()
		{
			List<Producao> listaProducao;
			int idProducao;

			do
			{
				Console.Write("Informe o ID da Producao que deseja excluir: ");
			} while (!int.TryParse(Console.ReadLine(), out idProducao));


			if (LocalizarProducao(idProducao))
			{
				string continuar;
				do
				{

					Console.Write("Tem certeza dessa exclusão?? s/n: ");
					continuar = TratamentoString();
				} while (continuar != "s" && continuar != "n");


				if (continuar.ToLower() == "s")
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
		static public string TratamentoString()
		{
			string Stratamento;
			do
			{
				Stratamento = Console.ReadLine();
				if (Stratamento == "") Console.WriteLine("O campo não pode ser vazio!!! Digite novamente");
			}
			while (Stratamento == "");

			return Stratamento;
		}
		public static void ImpressaoPorRegistro()//bloco imprime os registro do arquivo
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
	}
}
