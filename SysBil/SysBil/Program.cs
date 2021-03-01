using System;
using Controllers;

namespace SysBil {
    class Program {
        static void Main(string[] args)
        {
            GraficoMenu();
        }
        static void GraficoMenu()
        {
            string menuPrincipal = "";
            Console.WriteLine("Menu Principal");
            Console.Write("1-Menu de Produção\n" +
                          "2-Menu de Venda\n" +
                          "3-Menu de Compra\n" +
                          "0-Sair");
            do
            {
                Console.Write("\n>>>");
                menuPrincipal = Console.ReadLine();
                switch (menuPrincipal)
                {
                    case "1":

                        break;
                    case "2":

                        break;
                    case "3":
                        MenuCompra();
                        break;
                    default:
                        break;
                }
            } while (menuPrincipal != "0");

        }
        static void MenuCompra()
        {
            string menu = "1 - Menu fornecedor\n" +
                          "2 - Menu fornecedor bloqueado\n" +
                          "3 - Menu compra\n" +
                          "4 - Encerrar\n";
            string op;
            do
            {
                Console.WriteLine(menu);
                Console.Write(">>>");
                op = Console.ReadLine();


                switch (op)
                {
                    case "1":
                        ControllersFornecedor.Menu();
                        break;

                    case "2":
                        ControllersArquivoBloqueados.Menu();
                        break;

                    case "3":
                        ControllersCompra.Menu();
                        break;

                    default:
                        break;
                }
                Console.Write(">>>");
                op = Console.ReadLine();
            } while (op != "4");
        }
    }
}
