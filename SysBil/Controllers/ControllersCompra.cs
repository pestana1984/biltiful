using System;
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
            string path = @"C:\Arquivos";
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
                        LeituraCNPJ(cnpj);
                        ProcuraCnpj(cnpj, ref path);
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    case "4":
                        
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
        public static void LeituraCNPJ(string cnpj)
        {
            Console.Write("Digite o CNPJ da empresa: ");
            cnpj = Console.ReadLine();
        }
        public static void ProcuraCnpj(string cnpj, ref string path)
        {
            
        }
        //public static void Impressao()
        //{
        //    for (int i = 0; i < list.Count;)
        //    {
        //        string op1;
        //        Console.WriteLine($"\n{list.ElementAt(i)}");
        //        do
        //        {
        //            Console.WriteLine("\nO que deseja fazer a seguir?\n" +
        //                              "1 - Proximo\n" +
        //                              "2 - Anterior\n" +
        //                              "3 - Primeiro\n" +
        //                              "4 - Ultimo\n" +
        //                              "5 - Sair\n");
        //            op1 = Console.ReadLine();
        //            switch (op1)
        //            {
        //                case "1":
        //                    if (i == (list.Count - 1))
        //                    {
        //                        Console.WriteLine("Você esta na ultima posicao");
        //                    }
        //                    else
        //                    {
        //                        i++;
        //                        op1 = "5";
        //                    }
        //                    break;
        //                case "2":
        //                    if (i == 0)
        //                    {
        //                        Console.WriteLine("Você esta no inicio da lista");
        //                    }
        //                    else
        //                    {
        //                        i--;
        //                        op1 = "5";
        //                    }
        //                    break;
        //                case "3":
        //                    i = 0;
        //                    op1 = "5";
        //                    break;
        //                case "4":
        //                    i = list.Count - 1;
        //                    op1 = "5";
        //                    break;
        //                case "5":
        //                    break;
        //                default:
        //                    Console.WriteLine("Digite uma opção do menu");
        //                    break;
        //            }
        //        } while (op1 != "5");
        //    }
        //}
    }
}
