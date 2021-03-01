using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Models;

namespace Controllers
{
	public class MPrimaController
	{

		public static void MenuMateriaPrima()
		{
			int escolha = -1;
			MPrima maprima = new MPrima();
			List<MPrima> listaMateriaPrima = new List<MPrima>();
			do
			{
				Console.Clear();
				Console.WriteLine(" 1 - Cadastrar");
				Console.WriteLine(" 2 - Localizar");
				Console.WriteLine(" 3 - Impressão por Registro");
				Console.WriteLine(" 4 - Editar");
				Console.WriteLine(" 5 - Editar Situação");
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
						maprima = Cadastrar();
						listaMateriaPrima.Add(maprima);
						EscrevendoArquivo(listaMateriaPrima);
						Console.ReadKey();
						break;                                                                                            
					case 2:
						LocalizarMPrima();
						Console.ReadKey();
						break;
					case 3:
						ImpressaoPorRegistro();
						Console.ReadKey();
						break;
					case 4:
						EditMP();
						Console.ReadKey();
						break;
					case 5:
						EditMPSituation();
						Console.ReadKey();
						break;
				}
			} while (escolha != 0);
			Console.WriteLine("Precione Qualquer Tecla para Finalizar");
			Console.ReadKey();
		}
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
				Console.Write("Informe o nome da matéria prima (máximo 20 caracteres): ");
				nome = TratamentoString();

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
				else Console.WriteLine("Informe data da última compra (dd/MM/yyyy): ");
			} while (!deuCerto);

			//guardando data local
			DateTime dcadastro = DateTime.Now;
			Console.Write("Data do cadastro: " + dcadastro + "\n");

			//leitura e teste de situação
			string situacao;
			do
			{
				Console.Write("Situação (A ou I): ");
				situacao = TratamentoString().ToUpper();
			} while (situacao != "A" && situacao != "I");

			//instancia de obj materia prima
			MPrima materiaPrima = new MPrima(id, nome, ucompra, dcadastro, situacao);

			return materiaPrima;
		}
		public static void LocalizarMPrima()
		{
			bool itemEncontrado = false;
			string escolha = " ", escolha2 = " ";

			do
			{
				escolha = "";
				escolha2 = "";
				Console.Write("Informe o ID da materia prima para localizar: ");
				string buscarId = TratamentoString();

				List<MPrima> MPrimas = ConverterParaLista();

				foreach (var mprima in MPrimas)
				{
					if (mprima.Id == buscarId)
					{
						itemEncontrado = true;
						Console.WriteLine(mprima.ToString());
						Console.WriteLine();

						break;
					}
				}
				if (!itemEncontrado)
				{
					Console.WriteLine("Materia prima não cadastrada. Deseja sair?(S/N)");
					escolha = TratamentoString().ToUpper();
				}
				if (itemEncontrado)
				{
					Console.WriteLine("Deseja Continuar Localizando?(S/N)");
					escolha2 = TratamentoString().ToUpper();
					itemEncontrado = false;
				}
			} while ((escolha == "N") || (escolha2 == "S"));

		}
		public static List<MPrima> ConverterParaLista()
		{
			List<MPrima> MPrimas = new List<MPrima>();
			try
			{
				FileManipulator file = new FileManipulator() { Path = @"C:\Arquivos", Name = "Materia.dat" };
				string[] mprimaArquivadas = FileManipulatorController.LerArquivo(file);


				foreach (var mprima in mprimaArquivadas)
				{
					if (mprima.Length == 47)
					{
						string id = mprima.Substring(0, 6);
						string nome = mprima.Substring(6, 20);
						string uCompra = mprima.Substring(26, 10);
						string dCompra = mprima.Substring(36, 10);
						string situacao = mprima.Substring(46, 1);

						MPrima mPrima = new MPrima
						{
							Id = id,
							Nome = nome,
							Ucompra = Convert.ToDateTime(uCompra),
							Dcadastro = Convert.ToDateTime(dCompra),
							Situacao = situacao
						};

						MPrimas.Add(mPrima);
					}
				}
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine("ERRO!!!!: " + ex.Message);
				Console.ReadKey();
			}

			return MPrimas;
		}
		public static void ImpressaoPorRegistro()
		{

			List<MPrima> MPrimas = ConverterParaLista();

			Console.WriteLine("Faça a escolha do item que deseja imprimir");
			int cont = 0;
			string escolha;
			do
			{
				Console.Clear();

				if ((cont > 0) && (cont < MPrimas.Count() - 1))
					Console.WriteLine("A <- | -> P   I <-- | --> U :");
				if (cont == 0) Console.WriteLine("-> P  --> U :");
				if (cont == MPrimas.Count() - 1) Console.WriteLine("<- A  <-- I :");
				Console.WriteLine("S para sair");

				if (cont <= MPrimas.Count() - 1)
				{
					Console.WriteLine(MPrimas[cont].ToString());
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

				if ((escolha == "P") && (cont < MPrimas.Count() - 1))
				{
					cont++;
				}
				else if ((escolha == "U") && (cont < MPrimas.Count() - 1))
				{
					cont = MPrimas.Count() - 1;
				}

				if ((escolha == "A") && (cont <= MPrimas.Count() - 1) && (cont > 0))
				{
					cont--;
				}

				Console.ReadKey();
			}
			while (escolha != "S");

		}
		static public string TratamentoString()
		{
			string Stratamento;
			do
			{
				Stratamento = Console.ReadLine();
				if (Stratamento == "") Console.Write("O campo não pode ser vazio!!! Digite novamente: ");
			}
			while (Stratamento == "");

			return Stratamento;
		}
		public static void EditMP()
		{
			List<MPrima> listToEdit = new List<MPrima>();
			listToEdit = MPrimaController.ConverterParaLista();
			string escolha;
			bool itemEncontrado = false;

			Console.Write("Informe o ID da materia prima para alterar: ");
			string buscarId = Console.ReadLine();

			for (int i = 0; i < listToEdit.Count; i++)
			{
				if (listToEdit[i].Id == buscarId)
				{
					MPrima mp = new MPrima();
					itemEncontrado = true;
					Console.WriteLine("Essa é a matéria prima atual:");
					Console.WriteLine(listToEdit[i].ToString());

					do
					{
						Console.WriteLine("\nDeseja editar?(s/n):");
						escolha = Console.ReadLine().ToLower();
					} while (escolha != "n" && escolha != "s");


					if (escolha == "s")
					{
						#region leitura
						//alterar dados
						string nome;
						do
						{
							Console.Write("Informe o nome (máximo 20 caracteres): ");
							nome = Console.ReadLine();
						} while (nome.Length > 20);
						//data ucompra
						bool deuCerto = false;
						Console.Write("Informe data da última compra (dd/MM/yyyy): ");
						DateTime ucompra;
						do
						{
							if (DateTime.TryParse(Console.ReadLine(), out ucompra))
							{
								deuCerto = true;
							}
							else Console.Write("Informe data da última compra (dd/MM/yyyy): ");
						} while (!deuCerto);

						#endregion

						mp.Id = listToEdit[i].Id;
						mp.Nome = nome;
						mp.Ucompra = ucompra;
						mp.Dcadastro = listToEdit[i].Dcadastro;
						mp.Situacao = listToEdit[i].Situacao;

						listToEdit[i] = mp;
						MPrimaController.EscrevendoArquivo(listToEdit);

						Console.WriteLine("Essa é a matéria prima após alterações:");
						Console.WriteLine(listToEdit[i].ToString());
					}
					break;
				}
			}

			if (!itemEncontrado)
				Console.WriteLine("Materia prima não cadastrada");
		}
		public static void EditMPSituation()
		{
			List<MPrima> listToEdit = new List<MPrima>();
			listToEdit = MPrimaController.ConverterParaLista();
			string escolha;
			bool itemEncontrado = false;

			Console.Write("Informe o ID da materia prima para alterar: ");
			string buscarId = Console.ReadLine();

			for (int i = 0; i < listToEdit.Count; i++)
			{
				if (listToEdit[i].Id == buscarId)
				{
					MPrima mp = new MPrima();
					itemEncontrado = true;
					Console.WriteLine("Essa é a matéria prima atual:");
					Console.WriteLine(listToEdit[i].ToString());
					do
					{
						Console.WriteLine("\nDeseja editar?(s/n):");
						escolha = Console.ReadLine().ToLower();
					} while (escolha != "n" && escolha != "s");


					if (escolha == "s")
					{

						mp.Id = listToEdit[i].Id;
						mp.Nome = listToEdit[i].Nome;
						mp.Ucompra = listToEdit[i].Ucompra;
						mp.Dcadastro = listToEdit[i].Dcadastro;
						if (listToEdit[i].Situacao == "A") mp.Situacao = "I";
						else mp.Situacao = "A";

						listToEdit[i] = mp;
						MPrimaController.EscrevendoArquivo(listToEdit);

						Console.WriteLine("Situação alterada com sucesso:");
						Console.WriteLine(listToEdit[i].ToString());
					}
					break;
				}
			}

			if (!itemEncontrado)
				Console.WriteLine("Materia prima não cadastrada");
		}
		public static void EscrevendoArquivo(List<MPrima> listMP)
		{
			string nome20pos, linha = "";
			string ucompra, dcadastro;
			for (int i = 0; i < listMP.Count; i++)
			{

				nome20pos = listMP[i].Nome.PadRight(20, ' ');
				ucompra = listMP[i].Ucompra.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
				dcadastro = listMP[i].Dcadastro.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

				linha += listMP[i].Id + nome20pos + ucompra + dcadastro + listMP[i].Situacao.ToString() + "\n";
			}

			using (StreamWriter fileWrite = new StreamWriter(@"C:\Arquivos\Materia.dat"))
			{
				fileWrite.WriteLine(linha);
				fileWrite.Close();
			}
		}
		public static bool LocalizarMPrima(string buscarId)
		{
			bool itemEncontrado = false;

			List<MPrima> MPrimas = ConverterParaLista();

			foreach (var mprima in MPrimas)
			{
				if ((mprima.Id == buscarId) && (mprima.Situacao == "A"))
				{
					itemEncontrado = true;
					break;
				}
			}

			if (!itemEncontrado)
				Console.WriteLine("Materia prima não cadastrada ou Inativa");
			return itemEncontrado;
		}
	
	}
}
