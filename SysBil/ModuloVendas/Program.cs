using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloVendas
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] linhaDigital = new string[100];
            linhaDigital[0] = "Jaqueline Mistron"; // 25 caracteres
            linhaDigital[50] = "";
            StringBuilder teste = new StringBuilder();
            string nome50Pos = "Jaqueline Mistron".PadRight(50, ' ');
            string resultado = string.Concat(nome50Pos);
            Console.WriteLine("{0}", nome50Pos);
            //teste.Insert(0, nome25Pos);
            //teste.Insert(25, "123456789");
            Console.WriteLine(resultado.ToString());
        }
    }
}
