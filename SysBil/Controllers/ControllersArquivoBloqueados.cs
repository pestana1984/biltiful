using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers {
    public class ControllersArquivoBloqueados {
        private static string DirectoryPath = @"C:\temp\ws-c#\5by5-ativ03\biltiful\SysBil\Controllers\";
        private static string path = $"{DirectoryPath}Bloqueado.dat";

        private static List<string> listaCnpj = new List<string>();

        private static string MenuString = "\n>>> Menu - Fornecedor Bloqueado <<<\n" + "1- Inserir CNPJ\n" +
                                           "2- Localizar Fornecedor Bloqueado\n" + "3- Liberar CNPJ\n" +
                                           "4- Voltar ao Menu Principal\n\n" + "Digite sua escolha: ";
        public static bool CriarArquivo() {
            bool criou = false;

            if (!Directory.Exists(DirectoryPath)) {
                Directory.CreateDirectory(DirectoryPath);
                criou = true;
            }
            return criou;
        }
        public static void Menu() {
            string op = "";

            CriarArquivo();

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

        public static List<string> LerBloqueados() {
            List<string> bloqueados = new List<string>();

            using (StreamReader streamReader = new StreamReader(path)) {
                while (!streamReader.EndOfStream) {
                    bloqueados.Add(streamReader.ReadLine());
                }
            }
            return bloqueados;
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

            Console.Write("CNPJ que deseja bloquear: ");
            cnpj = Console.ReadLine();

            if (!CnpjCadastrados(cnpj)) {
                Console.WriteLine("\nCNPJ não encontrado nos cadastros!\n");
                return;
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
        private static bool CnpjCadastrados(string cnpj) {
            return ControllersFornecedor.ProcuraCNPJ(cnpj);
        }
    }
}
