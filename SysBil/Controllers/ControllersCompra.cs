using System;
using Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
     public class ControllersCompra
    {
        public static void Menu()
        {
            string path = @"C:\temp\Biltiful\biltiful\SysBil\mprima.txt";
            string menu = "";
            string cnpj = "";
            do
            {
                MenuGrafico();
                Console.Write("\nDigite um numero para acessar o menu: ");
                menu = Console.ReadLine();
                switch (menu)
                {
                    case "1":
                        GravarInfo(cnpj, ref path);
                        ProcuraCnpj(cnpj, ref path);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
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
        public static void GravarInfo(string cnpj, ref string path)
        {
            string continuar = "";
            int cont = 0;
            float qtd = 0, vunitario, titem; 
            Console.Write("Digite o CNPJ da empresa: ");
            cnpj = Console.ReadLine();
            if (true)//Verificar status do fornecedor
            {
                
                Compra compraMP = new Compra();
                do
                {
                    if (procurarMateriaPrima(ref path) == 1)
                    {
                        cont++;
                        do
                        {
                            Console.Write("Digite a quantidade que deseja desse item: ");
                            qtd = int.Parse(Console.ReadLine());

                            Console.Write("Valor unitário do item: ");
                            vunitario = float.Parse(Console.ReadLine());

                            titem = vunitario * qtd;
                        } while (qtd <= 0 || qtd > 999.99);
                        Console.WriteLine("Deseja inserir outro item? [S/N]: ");
                        continuar = Console.ReadLine();

                        Console.WriteLine();
                    }    
                    if (cont == 3)
                    {
                        Console.WriteLine("Limite de 3 produtos atingido");
                    }
                } while (continuar.ToUpper() != "N" && cont < 3);
            }
        }
        public static void ProcuraCnpj(string cnpj, ref string path)
        {
            
        }
        public static void Impressao()
        {
            for (int i = 0; i < list.Count;)
            {
                string op1;
                Console.WriteLine($"\n{list.ElementAt(i)}");
                do
                {
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
                                Console.WriteLine("Você esta na ultima posicao");
                            }
                            else
                            {
                                i++;
                                op1 = "5";
                            }
                            break;
                        case "2":
                            if (i == 0)
                            {
                                Console.WriteLine("Você esta no inicio da lista");
                            }
                            else
                            {
                                i--;
                                op1 = "5";
                            }
                            break;
                        case "3":
                            i = 0;
                            op1 = "5";
                            break;
                        case "4":
                            i = list.Count - 1;
                            op1 = "5";
                            break;
                        case "5":
                            break;
                        default:
                            Console.WriteLine("Digite uma opção do menu");
                            break;
                    }
                } while (op1 != "5");
            }
        }
        private static int procurarMateriaPrima(ref string path)
        {
            string id;
            int disponivel = 0;

            if (!File.Exists(path))
            {
                Console.WriteLine("\nArquivo de materia prima não encontrado!!\n");
                return 0;
            }

            Console.Write("ID da matéria prima que deseja comprar: ");
            id = Console.ReadLine();

            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    string linha = streamReader.ReadLine();
                    if (linha.Substring(0, 6) == id && linha.Substring(46, 1) == "A")
                    {
                        Console.WriteLine("\nSucesso\n");
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
    }
}
