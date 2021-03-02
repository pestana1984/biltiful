using System;
using Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Controllers
{
    public class ControllersCompra
    {

        private static string mPrimaPath = @"C:\Arquivos\Materia.dat";
        private static string DirectoryCompraPath = @"C:\Arquivos";
        private static string CompraPath = $@"{DirectoryCompraPath}\Compra.dat";
        private static string IdPath = $@"{DirectoryCompraPath}\IdCompra.dat";
        public static bool CriarArquivo()
        {
            bool criou = false;


            if (!Directory.Exists(DirectoryCompraPath))//CRIA PASTA
            {
                Directory.CreateDirectory(DirectoryCompraPath);
                criou = true;
            }
            if (!File.Exists(CompraPath))//CRIA ARQUIVO
            {
                FileStream sw = File.Create(CompraPath);
                sw.Close();
            }
            if (!File.Exists(IdPath))//CRIA ARQUIVO
            {
                FileStream sw = File.Create(IdPath);
                sw.Close();
            }
            return criou;
        }
        public static List<Compra> LerCompra()
        {
            CriarArquivo();
            List<Compra> list = new List<Compra>();
            string[] lines = File.ReadAllLines(CompraPath);
            for (int i = 0; i < lines.Length; i++)
            {
                int quantidade = 1;

                if (lines[i].Substring(44, 6) != " ")
                {
                    quantidade = 3;
                }
                else if (lines[i].Substring(22, 6) != " ")
                {
                    quantidade = 2;
                }

                if (i < quantidade)
                {
                    int id = int.Parse(lines[i].Substring(66, 5));
                    DateTime dcompra = DateTime.ParseExact(lines[i].Substring(71, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                    string cnpj = lines[i].Substring(79, 14);
                    float vtotal = float.Parse(lines[i].Substring(93, 7)) / 100;

                    Compra compra = new Compra()
                    {
                        Id = id,
                        Dcompra = dcompra,
                        CNPJ = cnpj,
                        Vtotal = vtotal,
                    };

                    compra.Mprima = new string[quantidade];
                    compra.Qtd = new float[quantidade];
                    compra.Vunitario = new float[quantidade];
                    compra.Titem = new float[quantidade];

                    int a = 0;
                    int b = 6;
                    int c = 11;
                    int d = 16;

                    for (int j = 0; j < quantidade; j++)
                    {

                        compra.Mprima[j] = lines[i].Substring(a, 6);
                        compra.Qtd[j] = float.Parse(lines[i].Substring(b, 5)) / 100;
                        compra.Vunitario[j] = float.Parse(lines[i].Substring(c, 5)) / 100;
                        compra.Titem[j] = float.Parse(lines[i].Substring(d, 6)) / 100;

                        a += 22;
                        b += 22;
                        c += 22;
                        d += 22;
                    }
                    list.Add(compra);
                }
            }
            return list;
        }
        public static void Menu()
        {
            string menu = "";
            do
            {
                MenuGrafico();
                Console.Write("\nDigite um numero para acessar o menu: ");
                menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        GravarInfo();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2"://Localizar
                        List<Compra> list = LerCompra();
                        bool encontrou = false;

                        Console.WriteLine("Digite o ID da compra");
                        int id = int.Parse(Console.ReadLine());

                        for (int i = 0; i < list.Count; i++)
                        {
                            if (list.ElementAt(i).Id == id)
                            {
                                Console.WriteLine(list.ElementAt(i));
                                encontrou = true;
                            }
                        }
                        if (!encontrou)
                        {
                            Console.WriteLine("ID nao encontrado!");
                        }
                        break;
                    case "3":// remover
                        RemoverCompra();
                        break;
                    case "4"://Imprimir
                        Impressao();
                        break;
                    default:
                        break;
                }
            } while (menu != "0");
            Console.ReadKey();
        }
        public static void MenuGrafico()
        {
            Console.WriteLine("1 - Cadastrar Compra\n" +
                              "2 - Localizar Compra\n" +
                              "3 - Excluir Compra\n" +
                              "4 - Imprimir Compras Registradas\n" +
                              "0 - Sair");
        }
        public static void SalvarID(int id)
        {
            FileStream fl = File.Create(IdPath);
            fl.Close();
            using (StreamWriter sw = new StreamWriter(IdPath, true))
            {
                sw.Write(id);
            }
        }
        public static int LerID()
        {
            CriarArquivo();
            string[] lines = File.ReadAllLines(IdPath);
            if (lines.Length != 1)
            {
                return 0;
            }
            
            return int.Parse(lines[0]);
        }
        public static void GravarInfo()
        {
            string cnpj, mprima = "", continuar = "";
            int cont = 0;
            float vtotal = 0;
            int id = LerID(); //Precisa ler o arquivo para saber o ID
            id++;
            if (id > 99999)
            {
                Console.WriteLine("Não é possivel cadastrar mais que 99.999 compras");
            }
            else
            {
                do
                {
                    Console.Write("Digite o CNPJ da empresa: ");
                    cnpj = Console.ReadLine();
                    if (!ControllersFornecedor.VerificaCNPJ(cnpj))
                    {
                        Console.WriteLine("CNPJ inválido. Digite novamente.");
                    }
                } while (!ControllersFornecedor.VerificaCNPJ(cnpj));

                if (CnpjCadastrados(cnpj))//Verificar status do fornecedor
                {
                    if (!File.Exists(mPrimaPath))
                    {
                        Console.WriteLine("\nArquivo de materia prima não encontrado!!\n");
                        return;
                    }
                    do
                    {
                        if (procurarMateriaPrima(mPrimaPath, ref mprima) == 1)
                        {
                            InserirArquivo(ref vtotal, mprima, id);

                            cont++;

                            if (cont == 3)
                            {
                                Console.WriteLine("\nLimite de 3 produtos atingido\n");
                            }

                            else
                            {
                                Console.WriteLine("\nDeseja inserir outro item? [S/N]: ");
                                continuar = Console.ReadLine();
                                Console.WriteLine();
                            }
                        }

                    } while (continuar.ToUpper() != "N" && cont < 3);

                    for (int i = 3; i > cont; i--)
                    {
                        LinhaVazia();
                    }

                    StringBuilder sb = new StringBuilder();

                    sb.Append($"{id.ToString("D5")}");
                    sb.Append($"{DateTime.Now:ddMMyyyy}");
                    sb.Append($"{cnpj}");
                    sb.Append($"{vtotal.ToString("00000.00").Replace(".", "")}");
                    using (StreamWriter sw = new StreamWriter(CompraPath, true))
                    {
                        sw.WriteLine(sb);
                    }
                    
                }
                else
                {
                    Console.WriteLine("Fornecedor não cadastrado;");
                    return;
                }
            }
            Console.WriteLine();
        }
        private static void InserirArquivo(ref float vtotal, string mprima, int id)
        {
            float qtd, vunitario, titem;
            do
            {
                Console.Write("Digite a quantidade que deseja desse item: ");
                qtd = float.Parse(Console.ReadLine());

                if (qtd < 1 || qtd > 999.99)
                {
                    Console.WriteLine("\nA quantidade deve estar entre 1 e 999,99");
                }
                else
                {
                    Console.Write("Valor unitário do item: ");
                    vunitario = float.Parse(Console.ReadLine());
                    if (vunitario < 1000 && vunitario > 0)
                    {
                        titem = vunitario * qtd;
                        if (titem > 9999.99)
                        {
                            Console.WriteLine("\nO valor total de cada item deve ser no maximo 9999,99");
                        }
                        else
                        {
                            vtotal += titem;
                            if (vtotal < 99999.99)
                            {
                                SalvarID(id);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("\nO valor total da compra deve ser no maximo 99999,99");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nO valor deve estar entre 1 e 999,99");
                    }
                }
            } while (true);



            StringBuilder sb = new StringBuilder();

            sb.Append($"{mprima}");
            sb.Append($"{qtd.ToString("000.00").Replace(".", "")}");
            sb.Append($"{vunitario.ToString("000.00").Replace(".", "")}");
            sb.Append($"{titem.ToString("0000.00").Replace(".", "")}");

            string convertido = sb.ToString();

            using (StreamWriter streamWriter = new StreamWriter(CompraPath, true))
            {
                streamWriter.Write(convertido);
            }
        }
        private static int procurarMateriaPrima(string path, ref string mprima)
        {
            path = mPrimaPath;
            string id;
            int disponivel = 0;

            Console.Write("ID da matéria prima que deseja comprar: ");
            id = Console.ReadLine().ToUpper();

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    string linha = streamReader.ReadLine();
                    if (linha.Substring(0, 6) == id && linha.Substring(46, 1) == "A")
                    {
                        mprima = id;
                        disponivel = 1;
                    }
                    else if (linha.Substring(0, 6) == id && linha.Substring(46, 1) == "I")
                    {
                        Console.WriteLine("\nMateria Prima Inativa!\n");
                        disponivel = 2;
                    }

                }
                if (disponivel == 0)
                {
                    Console.WriteLine("\nMatéria prima não encontrada!\n");
                }
            }
            return disponivel;
        }
        private static void LinhaVazia()
        {
            using (StreamWriter streamWriter = new StreamWriter(CompraPath, true))
            {
                streamWriter.Write("".PadRight(22, '0'));
            }
        }
        private static bool CnpjCadastrados(string cnpj)
        {
            return ControllersFornecedor.ProcuraCNPJ(cnpj);
        }
        private static void Impressao()
        {
            List<Compra> list = LerCompra();
            int i = 0;

            string op1;
            do
            {
                Console.WriteLine($"\n{list.ElementAt(i)}");
                ControllersFornecedor.fornecedores = ControllersFornecedor.LerArquivo();
                Console.WriteLine(ControllersFornecedor.fornecedores.Find(l => l.Cnpj == list.ElementAt(i).CNPJ));


                Console.WriteLine("\nO que deseja fazer a seguir?\n" +
                                  "1 - Proximo\n" +
                                  "2 - Anterior\n" +
                                  "3 - Primeiro\n" +
                                  "4 - Ultimo\n" +
                                  "5 - Sair\n");
                op1 = Console.ReadLine();
                switch (op1)
                {
                    case "1":
                        if (i == (list.Count - 1))
                        {
                            Console.Clear();
                            Console.WriteLine("Você esta no fim da lista");
                        }
                        else
                        {
                            i++;
                        }
                        break;
                    case "2":
                        if (i == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Você esta no inicio da lista");
                        }
                        else
                        {
                            i--;

                        }
                        break;
                    case "3":
                        Console.Clear();
                        i = 0;

                        break;
                    case "4":
                        Console.Clear();
                        i = list.Count - 1;

                        break;
                    case "5":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Digite uma opção do menu");
                        break;
                }
            } while (op1 != "5");

        }
        private static void RemoverCompra()
        {
            string linha;
            bool achou = false;

            List<string> compras = new List<string>();

            Console.Write("\nID da compra que deseja remover: ");
            string id = Console.ReadLine();
            Console.Clear();

            using (StreamReader streamReader = new StreamReader(CompraPath))
            {
                while (!streamReader.EndOfStream)
                {
                    linha = streamReader.ReadLine();
                    if (linha.Substring(66, 5) != id)
                    {
                        compras.Add(linha);
                    }
                    else achou = true;
                }
            }
            if (!achou)
            {
                Console.WriteLine("\nNenhuma compra com esse ID!!\n");
                return;
            }
            using (StreamWriter streamWriter = new StreamWriter(CompraPath))
            {
                for (int l = 0; l < compras.Count; l++)
                {
                    streamWriter.WriteLine(compras[l]);
                }
            }
            Console.WriteLine("\nCompra excluida com sucesso!!\n");
        }
    }
}