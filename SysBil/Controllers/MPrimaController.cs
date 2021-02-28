using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Controllers
{
    public class MPrimaController
    {
        public static void LocalizarMPrima()
        {
            bool itemEncontrado = false;

            Console.Write("Informe o ID da materia prima para localizar: ");
            string buscarId = TratamentoString();

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
                Console.WriteLine("Materia prima não cadastrada");
        }

        public static List<MPrima> ConverterParaLista()
        {
            List<MPrima> MPrimas = new List<MPrima>();
            try
            {
                FileManipulator file = new FileManipulator() { Path = @"C:\Users\ferna\Google Drive\Estagio Five\Repositorio\biltiful\SysBil\files", Name = "Materia.txt" };
                string[] mprimaArquivadas = FileManupulatorController.LerArquivo(file);

                
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
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERRO!!!!: "+ ex.Message);
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
                while ((escolha != "A") && (escolha != "P")&&(escolha != "I")&&(escolha != "U")&& (escolha != "S"));

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

                if ((escolha == "A") && (cont <= MPrimas.Count() - 1)&&(cont > 0))
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
                if (Stratamento == "") Console.WriteLine("O campo não pode ser vazio!!! Digite novamente");
            }
            while (Stratamento == "");

            return Stratamento;
        }
    }
}
