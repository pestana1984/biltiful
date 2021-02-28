using System;
using System.IO;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Controllers {
    public class ControllersFornecedor {
        public static string Path = @"C:\temp\ws-c#\5by5-ativ03\biltiful\SysBil\Controllers";
        public static string ArquivoPath = $@"{Path}\Fornecedor.dat";

        public static string MenuString = "1 - Cadastrar novo fornecedor\n" +
                                          "2 - Localizar fornecedor\n" +
                                          "3 - Editar\n" +
                                          "4 - Impressão\n" +
                                          "5 - Voltar ao menu anterior\n";

        public static string MenuEditar = "1 - Editar razao social\n" +
                                          "2 - Editar situacao\n" +
                                          "3 - Excluir\n" +
                                          "4 - Voltar ao menu anterior\n";
        public static bool CriarArquivo() {
            bool criou = false;

            if (!Directory.Exists(Path)) {
                Directory.CreateDirectory(Path);
                criou = true;
            }
            if (!File.Exists(ArquivoPath)) {
                FileStream sw = File.Create(ArquivoPath);
                sw.Close();
            }
            return criou;
        }
        public static void SalvarArquivo(Fornecedor fornecedor) {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{fornecedor.Cnpj}");
            sb.Append($"{fornecedor.RSocial.PadRight(50, ' ')}");
            sb.Append($"{fornecedor.DAbertura:ddMMyyyy}");
            sb.Append($"{fornecedor.UCompra:ddMMyyyy}");
            sb.Append($"{fornecedor.DCadastro:ddMMyyyy}");
            sb.Append($"{fornecedor.Situacao}");
            string convertido = sb.ToString();

            using (StreamWriter streamWriter = new StreamWriter(ArquivoPath, true)) {
                streamWriter.WriteLine(convertido);
            }
        }
        public static List<Fornecedor> LerArquivo() {
            List<Fornecedor> list = new List<Fornecedor>();
            string[] lines = File.ReadAllLines(ArquivoPath);
            for (int i = 0; i < lines.Length; i++) {
                string cnpj = lines[i].Substring(0, 14);
                string rsocial = lines[i].Substring(14, 50);
                DateTime dabertura = DateTime.ParseExact(lines[i].Substring(64, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                DateTime ucompra = DateTime.ParseExact(lines[i].Substring(72, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                DateTime dcadastro = DateTime.ParseExact(lines[i].Substring(80, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                char situacao = char.Parse(lines[i].Substring(88, 1));

                Fornecedor fornecedor = new Fornecedor {
                    Cnpj = cnpj,
                    RSocial = rsocial,
                    DAbertura = dabertura,
                    UCompra = ucompra,
                    DCadastro = dcadastro,
                    Situacao = situacao
                };
                list.Add(fornecedor);
            }
            return list;
        }
        public static bool ProcuraCNPJ(string verificaCnpj) {
            string[] lines = File.ReadAllLines(ArquivoPath);
            for (int i = 0; i < lines.Length; i++) {
                string cnpj = lines[i].Substring(0, 14);
                if (cnpj == verificaCnpj) {
                    return true;
                }
            }
            return false;
        }
        public static Fornecedor CadastrarFornecedor(string cnpj) {
            Console.Write("Digite o nome social: ");
            string rsocial = Console.ReadLine();

            Console.Write("Data de abertura(dd/MM/aaaa): ");
            string abertura = Console.ReadLine().Replace("/", "");
            DateTime dabertura = DateTime.ParseExact(abertura, "ddMMyyyy", CultureInfo.InvariantCulture);

            Fornecedor fornecedor = new Fornecedor {
                Cnpj = cnpj,
                RSocial = rsocial,
                DAbertura = dabertura,
                UCompra = DateTime.Now,
                DCadastro = DateTime.Now,
                Situacao = 'a'
            };
            return fornecedor;
        }
        public static void Menu() {
            List<Fornecedor> fornecedores = new List<Fornecedor>();
            List<Fornecedor> list = new List<Fornecedor>();
            Fornecedor fornecedor = new Fornecedor();
            string cnpj;

            if (!CriarArquivo()) {
                fornecedores = LerArquivo();
            }

            string op;
            string op1;
            do {
                Console.WriteLine(MenuString);
                Console.Write(">>>");
                op = Console.ReadLine();

                switch (op) {
                    case "1": //Cadastrar novo fornecedor

                        Console.Write("Digite o CNPJ: ");
                        cnpj = Console.ReadLine();

                        if (!VerificaCNPJ(cnpj)) {
                            Console.WriteLine("\nCNPJ inválido\n");
                            break;
                        }
                        
                        if (ProcuraCNPJ(cnpj)) {
                            fornecedor = fornecedores.Find(l => l.Cnpj == cnpj);
                            Console.WriteLine(fornecedor);
                        }
                        else {
                            fornecedor = CadastrarFornecedor(cnpj);
                            fornecedores.Add(fornecedor);
                            SalvarArquivo(fornecedor);
                        }

                        break;

                    case "2": //Localizar fornecedor
                        Console.WriteLine("Digite o CNPJ");
                        cnpj = Console.ReadLine();

                        if (ProcuraCNPJ(cnpj)) {
                            fornecedor = fornecedores.Find(l => l.Cnpj == cnpj);
                            Console.WriteLine(fornecedor);
                        }
                        else {
                            Console.WriteLine("CNPJ nao encontrado!");
                        }
                        break;

                    case "3"://Editar
                        Console.WriteLine("Digite o CNPJ que deseja editar");
                        cnpj = Console.ReadLine();
                        do {
                            Console.WriteLine($"\n{MenuEditar}");
                            op1 = Console.ReadLine();

                            switch (op1) {
                                case "1"://Editar razao social
                                    fornecedor = fornecedores.Find(l => l.Cnpj == cnpj);
                                    if (fornecedor != null) {
                                        Console.WriteLine("Digite a nova razao social");
                                        fornecedor.RSocial = Console.ReadLine();
                                    }
                                    else {
                                        Console.WriteLine("CNPJ nao encontrado");
                                    }
                                    break;

                                case "2"://Editar situacao
                                    fornecedor = list.Find(l => l.Cnpj == cnpj);
                                    if (fornecedor != null) {
                                        Console.Write("Digite a nova situacao(a/i): ");
                                        fornecedor.Situacao = char.Parse(Console.ReadLine());
                                    }
                                    else {
                                        Console.WriteLine("CNPJ nao encontrado");
                                    }

                                    break;

                                case "3"://Excluir
                                    int i = 0;
                                    bool encontrou = false;
                                    List<string> excluir = new List<string>();

                                    using (StreamReader streamReader = new StreamReader(ArquivoPath)) {
                                        while (!streamReader.EndOfStream) {
                                            excluir.Add(streamReader.ReadLine());
                                            if (excluir[i].Substring(0, 14) == cnpj) {
                                                encontrou = true;
                                                break;
                                            }
                                            i++;
                                        }
                                    }
                                    if (!encontrou) {
                                        Console.WriteLine("\nCNPJ não encontrado\n");
                                        return;
                                    }

                                    list.RemoveAt(i);
                                    using (StreamWriter streamWriter = new StreamWriter(ArquivoPath)) {
                                        for (int l = 0; l < list.Count; l++) {
                                            streamWriter.WriteLine(list[l]);
                                        }
                                    }
                                    Console.WriteLine("\nCNPJ Liberado com sucesso!");

                                    break;

                                case "4"://Voltar ao menu anterior
                                    break;

                                default:
                                    break;
                            }

                        } while (op1 != "4");
                        break;

                    case "4":
                        List<string> bloqueados = ControllersArquivoBloqueados.LerBloqueados();
                        list = fornecedores;

                        for (int i = 0; i < bloqueados.Count; i++) {
                            if (list.ElementAt(i).Situacao == 'i') {
                                list.RemoveAt(i);
                            }
                            else {
                                for (int k = 0; k < bloqueados.Count; k++) {
                                    if (list.ElementAt(i).Cnpj == bloqueados.ElementAt(k)) {
                                        list.RemoveAt(i);
                                    }
                                }
                            }
                        }

                        int j = 0;
                        do {
                            if (list.Count == 0) {
                                Console.WriteLine("Nenhum fornecedor ativo e não bloqueado!");
                                op1 = "5";
                            }
                            else {
                                Console.WriteLine($"\n{list.ElementAt(j)}");
                                Console.WriteLine("\nO que deseja fazer a seguir?\n" +
                                                  "1 - Proximo\n" +
                                                  "2 - Anterior\n" +
                                                  "3 - Primeiro\n" +
                                                  "4 - Ultimo\n" +
                                                  "5 - Sair\n");
                                op1 = Console.ReadLine();

                                switch (op1) {
                                    case "1":
                                        if (j == (list.Count - 1)) {
                                            Console.WriteLine("Você esta no fim da lista");
                                        }
                                        else {
                                            j++;
                                        }
                                        break;

                                    case "2":
                                        if (j == 0) {
                                            Console.WriteLine("Você esta no inicio da lista");
                                        }
                                        else {
                                            j--;
                                        }
                                        break;

                                    case "3":
                                        j = 0;
                                        break;

                                    case "4":
                                        j = list.Count - 1;
                                        break;

                                    case "5":
                                        break;

                                    default:
                                        Console.WriteLine("Digite uma opção do menu");
                                        break;
                                }
                            }
                        } while (op1 != "5");
                        break;

                    case "5":
                        Console.WriteLine("Voltando menu anterior !");
                        break;

                    default:
                        Console.WriteLine("Digite uma opção do Menu !");
                        break;
                }
            } while (op != "5");
        }
        public static bool VerificaCNPJ(string cnpj) {
            int[] m1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] m2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0, resto;
            string digito, cnpj2;

            if (cnpj.Length != 14) {
                return false;
            }

            cnpj2 = cnpj.Substring(0, 12);

            for (int i = 0; i < 12; i++) {
                soma += int.Parse(cnpj2[i].ToString()) * m1[i];
            }

            resto = (soma % 11);

            if (resto < 2) {
                resto = 0;
            }
            else {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            cnpj2 += digito;

            soma = 0;

            for (int i = 0; i < 13; i++) {
                soma += int.Parse(cnpj2[i].ToString()) * m2[i];
            }
            resto = (soma % 11);

            if (resto < 2) {
                resto = 0;
            }
            else {
                resto = 11 - resto;
            }
            digito += resto.ToString();

            if (cnpj.EndsWith(digito)) {
                return true;
            }
            return false;
        }
    }
}