using System;
using Controllers;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysBil
{
    class Program
    {
        static void Main(string[] args)
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
