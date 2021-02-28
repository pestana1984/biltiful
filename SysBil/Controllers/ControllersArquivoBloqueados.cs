using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers {
    public class ControllersArquivoBloqueados {
        private static string path = @"..\..\..\Bloqueado.dat";
        private static List<string> listaCnpj = new List<string>();
        private static string MenuString = "\n>>> Menu - Fornecedor Bloqueado <<<\n" + "1- Inserir CNPJ\n" +
                    "2- Localizar Fornecedor Bloqueado\n" + "3- Liberar CNPJ\n" +
                    "4- Voltar ao Menu Principal\n\n" + "Digite sua escolha: ";

        public static void Menu() {
            string op = "";

            while (op != "4") {
                Console.Write(MenuString);
                op = Console.ReadLine();
                Console.Clear();

                switch (op) {
                    case "1":
                        InserirNoArquivo();
                        break;
                    case "2":
                        LocalizarNoArquivo();
                        break;
                    case "3":
                        Liberar();
                        break;

                    case "4": break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nDIGITE UM NÚMERO DE 1 A 4\n");
                        Console.ResetColor();
                        break;
                }
            }
        }
        private static void Liberar() {
            string cnpj;
            int i = 0;
            bool encontrou = false;
            if (File.Exists(path)) {
                Console.Write("CNPJ que deseja liberar: ");
                cnpj = Console.ReadLine();

                using (StreamReader streamReader = new StreamReader(path)) {
                    while (!streamReader.EndOfStream) {
                        listaCnpj.Add(streamReader.ReadLine());
                        if (listaCnpj[i] == cnpj) {
                            encontrou = true;
                        }
                        i++;
                    }
                }
                if (!encontrou) {
                    Console.WriteLine("\nCNPJ não encontrado\n");
                    return;
                }

                listaCnpj.Remove(cnpj);
                using (StreamWriter streamWriter = new StreamWriter(path)) {
                    for (int l = 0; l < listaCnpj.Count; l++) {
                        streamWriter.WriteLine(listaCnpj[l]);
                    }
                }
                Console.WriteLine("\nCNPJ Liberado com sucesso!");
            }
            else Console.WriteLine("\nNinguém no arquivo de bloqueados!!");
        }
        private static void LocalizarNoArquivo() {
            string cnpj;
            bool encontrou = false;

            if (File.Exists(path)) {
                Console.Write("Digite o CNPJ que deseja verificar se está bloqueado: ");
                cnpj = Console.ReadLine();

                using (StreamReader streamReader = new StreamReader(path)) {
                    while (!streamReader.EndOfStream) {
                        string linha = streamReader.ReadLine();
                        if (linha == cnpj) {
                            Console.WriteLine("\nCNPJ bloqueado!!");
                            encontrou = true;
                        }
                    }
                    if (!encontrou) {
                        Console.WriteLine("\nCNPJ não bloqueado!!");
                    }
                }
            }
            else Console.WriteLine("\nNinguém no arquivo de bloqueados!!");
        }
        private static bool LocalizarNoArquivo(string cnpj) {
            bool encontrou = false;
            if (File.Exists(path)) {
                using (StreamReader streamReader = new StreamReader(path)) {
                    while (!streamReader.EndOfStream) {
                        string linha = streamReader.ReadLine();
                        if (linha == cnpj) {
                            encontrou = true;
                        }
                    }
                }
            }
            return encontrou;
        }
        private static void InserirNoArquivo() {
            string cnpj;
            while (true) {
                Console.Write("CNPJ que deseja bloquear: ");
                cnpj = Console.ReadLine();

                if (ValidacaoCNPJ(cnpj)) break;
                Console.WriteLine("\nDigite um CNPJ válido!!\n");
            }

            if (LocalizarNoArquivo(cnpj)) {
                Console.WriteLine("\nCNPJ já está bloqueado!!");
                return;
            }

            using (StreamWriter streamWriter = new StreamWriter(path, true)) {
                streamWriter.WriteLine(cnpj);
            }
            Console.WriteLine("\nCNPJ bloqueado com sucesso!!\n");

        }
        private static bool ValidacaoCNPJ(string cnpj) {
            int[] m1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] m2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0, resto;
            string digito, cnpj2;

            if (cnpj.Length != 14)
                return false;

            cnpj2 = cnpj.Substring(0, 12);

            for (int i = 0; i < 12; i++) soma += int.Parse(cnpj2[i].ToString()) * m1[i];
            resto = (soma % 11);

            if (resto < 2)
                resto = 0;

            else
                resto = 11 - resto;

            digito = resto.ToString();
            cnpj2 += digito;

            soma = 0;

            for (int i = 0; i < 13; i++) soma += int.Parse(cnpj2[i].ToString()) * m2[i];
            resto = (soma % 11);

            if (resto < 2)
                resto = 0;

            else
                resto = 11 - resto;

            digito += resto.ToString();

            if (cnpj.EndsWith(digito)) return true;

            return false;
        }
    }
}
