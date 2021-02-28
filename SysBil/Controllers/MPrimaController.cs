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
		public static void LocalizarMPrima()
		{
			bool itemEncontrado = false;

			Console.Write("Informe o ID da materia prima para localizar: ");
			string buscarId = Console.ReadLine();

			List<MPrima> MPrimas = ConverterParaLista();

			foreach (var mprima in MPrimas)
			{
				if (mprima.Id == buscarId)
				{
					itemEncontrado = true;
					Console.WriteLine(mprima.ToString());
					break;
				}
			}

			if (!itemEncontrado)
				Console.WriteLine("Impossível localizar! Item não cadastrado.");
		}
		public static MPrima LocalizarParaEditarMP()
		{
			bool itemEncontrado = false;

			Console.Write("Informe o ID da materia prima para localizar: ");
			string buscarId = Console.ReadLine();

			List<MPrima> MPrimas = ConverterParaLista();
			MPrima objmprima = new MPrima();
			foreach (var mprima in MPrimas)
			{
				if (mprima.Id == buscarId)
				{
					itemEncontrado = true;
					Console.WriteLine(mprima.ToString());
					objmprima = mprima;
					break;
				}
			}

			if (!itemEncontrado)
				Console.WriteLine("Impossível localizar! Item não cadastrado.");

			return objmprima;
		}
		public static List<MPrima> ConverterParaLista()
		{
			FileManipulator file = new FileManipulator() { Path = @"C:\Users\Thiago\Desktop\PROJETO\biltiful\SysBil\files", Name = "Materia.txt" };
			string[] mprimaArquivadas = FileManupulatorController.LerArquivo(file);

			List<MPrima> MPrimas = new List<MPrima>();

			foreach (var mprima in mprimaArquivadas)
			{
				if (mprima.Length == 47)
				{
					string id = mprima.Substring(0, 6);
					string nome = mprima.Substring(6, 20);
					string uCompra = mprima.Substring(26, 10);
					string dCompra = mprima.Substring(36, 10);
					char situacao = char.Parse(mprima.Substring(46, 1));

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
			return MPrimas;
		}
	}
}
