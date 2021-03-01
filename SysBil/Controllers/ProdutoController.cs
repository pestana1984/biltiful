using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Globalization;
using System.IO;

namespace Controllers
{
    public class ProdutoController
    {
        public static void MenuProduto()
        {
            int escolha = -1;
            Produto produto = new Produto();
            List<Produto> listaProduto = new List<Produto>();

            do
            {
                Console.Clear();
                Console.WriteLine(" 1 - Cadastrar");
                Console.WriteLine(" 2 - Localizar");
                Console.WriteLine(" 3 - Impressão por Registro");
                Console.WriteLine(" 4 - Editar");
                Console.WriteLine(" 5 - Editar Situação");
                Console.WriteLine(" 0 - Para sair");
                Console.WriteLine();
                Console.Write("Escolha uma das opções acima: ");

                if (int.TryParse(Console.ReadLine(), out escolha))
                {
                    if (escolha < 0 || escolha >= 6)
                    {
                        Console.WriteLine("Digite um Valor entre 1 e 5");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Digite um Valor entre 1 e 5");
                    escolha = -1;
                    Console.ReadKey();
                }


                    switch (escolha)
                    {
                        case 1:
                            produto = Cadastrar();
                            listaProduto.Add(produto);
                            EscrevendoArquivo(listaProduto);
                            Console.ReadKey();
                            break;                                                  
                        case 2:
                            LocalizarProduto();
                            Console.ReadKey();
                            break;
                        case 3:
                            ImpressaoPorRegistro();
                            Console.ReadKey();
                            break;
                        case 4:
                            EditP();
                            Console.ReadKey();
                            break;
                        case 5:
                            EditPSituation();
                            Console.ReadKey();
                            break;
                    }
                
            } while (escolha != 0);
            Console.WriteLine("Precione Qualquer Tecla para Finalizar");
            Console.ReadKey();
        }

        public static Produto Cadastrar()//modulo de cadastro
        {
            #region Barra
            //Gerador de código de barras para produto
            //789_________1 -> digito -> formato
            string[] valores = new string[13];
            valores[0] = "7";
            valores[1] = "8";
            valores[2] = "9";



            //entrada de valores aleatórios
            Random randNum = new Random();
            int num;
            for (int i = 3; i < 12; i++)
            {
                num = randNum.Next(0, 9);
                valores[i] = num.ToString();
            }

            //soma de números impar
            int soma1 = 0, soma2 = 0, soma3 = 0, multiplo = 0;
            for (int i = 0; i < 12; i++)
            {

                if (!(i % 2 == 0))
                {
                    soma1 += int.Parse(valores[i]);
                }
                else
                {
                    soma2 += int.Parse(valores[i]);
                }
            }
            //calculo gerador de digito
            soma1 = soma1 * 3;
            soma3 = soma1 + soma2;
            multiplo = soma3 % 10;
            multiplo = 10 - multiplo;
            valores[12] = multiplo.ToString();
            #endregion

            string cbarras = "";
            foreach (string v in valores) cbarras += v;

            string nome;
            do
            {
                Console.Write("Informe o nome do Produto (máximo 20 caracteres): ");
                nome = TratamentoString();

            } while (nome.Length > 20);

            double valueProduct = -1;
            do
            {
                Console.Write("Informe o valor de venda: ");
                if (double.TryParse(Console.ReadLine(), out valueProduct)) { }

            } while (valueProduct <= 0 || valueProduct >= 1000);

            //leitura da data
            bool deuCerto = false;
            Console.Write("Informe data da última venda (dd/MM/yyyy): ");
            DateTime uvenda;// = DateTime.TryParse(Console.ReadLine(), CultureInfo.InvariantCulture);
            do
            {
                if (DateTime.TryParse(Console.ReadLine(), out uvenda))
                {
                    deuCerto = true;
                }
                else Console.WriteLine("Informe data da última venda (dd/MM/yyyy):");

            } while (!deuCerto);

            //guardando data local
            DateTime dcadastro = DateTime.Now;
            Console.Write("Data do cadastro: " + dcadastro + "\n");

            //leitura e teste de situação
            char situacao;
            do
            {
                Console.Write("Situação (A ou I): ");
                if (char.TryParse(Console.ReadLine().ToUpper(), out situacao))
                {
                }

            } while (situacao != 'A' && situacao != 'I');

            //instancia de obj produto
            Produto produto = new Produto(cbarras, nome, valueProduct, uvenda, dcadastro, situacao);

            return produto;
        }
        public static void LocalizarProduto()//modulo para localizar produto
        {
            bool itemEncontrado = false;

            Console.Write("Informe o Codigo de Barra do produto para localizar: ");
            string buscarId = TratamentoString();

            List<Produto> Produtos = ConverterParaLista();

            foreach (var produto in Produtos)
            {
                if (produto.CBarras == buscarId)
                {
                    itemEncontrado = true;
                    Console.WriteLine(produto.ToString());
                    break;
                }
            }

            if (!itemEncontrado)
                Console.WriteLine("Produto não cadastrado");
        }
        public static List<Produto> ConverterParaLista()//modulo faz a leitura do arquivo e aloca em um objeto produto
        {
            List<Produto> Produtos = new List<Produto>();
            try
            {
                FileManipulator file = new FileManipulator() { Path = @"C:\Users\ferna\Google Drive\Estagio Five\Repositorio\biltiful\SysBil\files\", Name = "Cosmetico.txt" };
                string[] produtoArquivadas = FileManupulatorController.LerArquivo(file);


                foreach (var produto in produtoArquivadas)
                {
                    if (produto.Length == 60)
                    {
                        string cbarras = produto.Substring(0, 13);
                        string nome = produto.Substring(13, 20);
                        string vvenda = produto.Substring(33, 6);
                        string uvenda = produto.Substring(39, 10);
                        string dcadastro = produto.Substring(49, 10);
                        char situacao = char.Parse(produto.Substring(59, 1));

                        Produto Produto1 = new Produto()
                        {
                            CBarras = cbarras,
                            Nome = nome,
                            Vvenda = double.Parse(vvenda),
                            UVenda = Convert.ToDateTime(uvenda),
                            DCadastro = Convert.ToDateTime(dcadastro),
                            Situacao = situacao
                        };

                        Produtos.Add(Produto1);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("ERRO!!!!: " + ex.Message);
                Console.ReadKey();
            }

            return Produtos;
        }
        public static void ImpressaoPorRegistro()//localiza um objeto e faz a impressão 
        {

            List<Produto> Produtos = ConverterParaLista();

            Console.WriteLine("Faça a escolha do item que deseja imprimir");
            int cont = 0;
            string escolha;
            do
            {
                Console.Clear();

                if ((cont > 0) && (cont < Produtos.Count() - 1))
                    Console.WriteLine("A <- | -> P   I <-- | --> U :");
                if (cont == 0) Console.WriteLine("-> P  --> U :");
                if (cont == Produtos.Count() - 1) Console.WriteLine("<- A  <-- I :");
                Console.WriteLine("S para sair");

                if (cont <= Produtos.Count() - 1)
                {
                    Console.WriteLine(Produtos[cont].ToString());
                }

                do
                {
                    escolha = TratamentoString().ToUpper();
                    if ((escolha != "A") && (escolha != "P") && (escolha != "I") && (escolha != "U") && (escolha != "S"))
                    {
                        Console.WriteLine("Digite apenas as opçoes que o menu dispoe no momento!!!");
                    }

                }
                while ((escolha != "A") && (escolha != "P") && (escolha != "I") && (escolha != "U") && (escolha != "S"));

                if (escolha == "I")
                {
                    cont = 0;
                }

                if ((escolha == "P") && (cont < Produtos.Count() - 1))
                {
                    cont++;
                }
                else if ((escolha == "U") && (cont < Produtos.Count() - 1))
                {
                    cont = Produtos.Count() - 1;
                }

                if ((escolha == "A") && (cont <= Produtos.Count() - 1) && (cont > 0))
                {
                    cont--;
                }

                Console.ReadKey();
            }
            while (escolha != "S");

        }

        public static void EditP()// localiza e edita um objeto
        {
            List<Produto> listToEdit = new List<Produto>();
            listToEdit = ProdutoController.ConverterParaLista();

            bool itemEncontrado = false;

            Console.Write("Informe o Codigo de barra para alterar: ");
            string buscarId = TratamentoString();

            for (int i = 0; i < listToEdit.Count; i++)
            {
                if (listToEdit[i].CBarras == buscarId)
                {
                    Produto p = new Produto();
                    itemEncontrado = true;
                    Console.WriteLine("Essa é o produto atual:");
                    Console.WriteLine(listToEdit[i].ToString());
                    Console.WriteLine("\nDeseja editar?(s/n):");
                    if (Console.ReadLine().ToLower() != "n")
                    {
                        #region leitura
                        //alterar dados
                        string nome;
                        do
                        {
                            Console.Write("Informe o nome (máximo 20 caracteres): ");
                            nome = Console.ReadLine();
                        } while (nome.Length > 20);
                        //data ucompra
                        bool deuCerto = false;
                        Console.Write("Informe data da última compra (dd/MM/yyyy): ");
                        DateTime uvenda;
                        do
                        {
                            if (DateTime.TryParse(Console.ReadLine(), out uvenda))
                            {
                                deuCerto = true;
                            }
                        } while (!deuCerto);
                        #endregion

                        p.CBarras = listToEdit[i].CBarras;
                        p.Nome = nome;
                        p.UVenda = uvenda;
                        p.Vvenda = listToEdit[i].Vvenda;
                        p.DCadastro = listToEdit[i].DCadastro;
                        p.Situacao = listToEdit[i].Situacao;

                        listToEdit[i] = p;
                        ProdutoController.EscrevendoArquivo(listToEdit);

                        Console.WriteLine("Esse é o produto atualizado:");
                        Console.WriteLine(listToEdit[i].ToString());
                    }
                    break;
                }
            }

            if (!itemEncontrado)
                Console.WriteLine("Produto não cadastrado");
        }  
        public static void EditPSituation()// localiza e edita a situação de um objeto
        {
            List<Produto> listToEdit = new List<Produto>();
            listToEdit = ProdutoController.ConverterParaLista();

            bool itemEncontrado = false;

            Console.Write("Informe o Codigo de barras que deseja alterar: ");
            string buscarId = TratamentoString();

            for (int i = 0; i < listToEdit.Count; i++)
            {
                if (listToEdit[i].CBarras == buscarId)
                {
                    Produto p = new Produto();
                    itemEncontrado = true;
                    Console.WriteLine("Esse é o produto atual:");
                    Console.WriteLine(listToEdit[i].ToString());
                    Console.WriteLine("\nDeseja alterar a situação?(s/n):");
                    if (Console.ReadLine().ToLower() != "n")
                    {
                        p.CBarras = listToEdit[i].CBarras;
                        p.Nome = listToEdit[i].Nome;
                        p.Vvenda = listToEdit[i].Vvenda;
                        p.UVenda = listToEdit[i].UVenda;
                        p.DCadastro = listToEdit[i].DCadastro;
                        if (listToEdit[i].Situacao == 'A') p.Situacao = 'I';
                        else p.Situacao = 'A';

                        listToEdit[i] = p;
                        ProdutoController.EscrevendoArquivo(listToEdit);

                        Console.WriteLine("Situação alterada com sucesso:");
                        Console.WriteLine(listToEdit[i].ToString());
                    }
                    break;
                }
            }

            if (!itemEncontrado)
                Console.WriteLine("Produto  não cadastrado");
        }
        public static void EscrevendoArquivo(List<Produto> listP)// salva os objetos de uma lista em arquivo

        {
            string nome20pos, linha = "";
            string uvenda, dcadastro, value6Pos;
            for (int i = 0; i < listP.Count; i++)
            {

                nome20pos = listP[i].Nome.PadRight(20, ' ');
                value6Pos = listP[i].Vvenda.ToString().PadRight(6, ' ');
                uvenda = listP[i].UVenda.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcadastro = listP[i].DCadastro.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                linha += listP[i].CBarras + nome20pos + value6Pos + uvenda + dcadastro + listP[i].Situacao.ToString() + "\n";
            }

            using (StreamWriter fileWrite = new StreamWriter(@"C:\Users\ferna\Google Drive\Estagio Five\Repositorio\biltiful\SysBil\files\Cosmetico.txt"))
            {
                fileWrite.WriteLine(linha);
                fileWrite.Close();
            }
        }
        static public string TratamentoString()//faz tratamento de entrada de dados feita pelo usuario
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
