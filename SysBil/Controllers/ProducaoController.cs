using Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class ProducaoController
    {
        public static int idSequencial = 0;
        public static Producao Cadastrar()
        {

            idSequencial++;
            if (idSequencial > 99999) Console.WriteLine("Id fora dos limites suportados");

            bool deuCerto = false;
            Console.Write("Informe data da produção (dd/MM/yyyy): ");
            DateTime dproducao;
            do
            {
                if (DateTime.TryParse(Console.ReadLine(), out dproducao))
                {
                    deuCerto = true;
                }
            } while (!deuCerto);

            Console.WriteLine("Informe o Código de Barras: ");
            string cbarra = Console.ReadLine();
            //Localizar produto para validação

            Console.WriteLine("Informe a quantidade a ser produzida: ");
            int qtd = int.Parse(Console.ReadLine());
            //ERRO----------------ERRO
            double[] qtdMP = new double[3];
            string decisao;
            int cont = 1;
            do
            {
                Console.WriteLine("Informe a quantidade de matéira prima: ");
                qtdMP[cont-1] = double.Parse(Console.ReadLine());
                if(cont < 3)Console.WriteLine("Deseja inserir mais matéria prima (s/n):");
                decisao = Console.ReadLine();
                cont++;                
            } while (decisao != "n" || cont == 3);


            string[] idMp = new string[3];
            for (int i = 0; i < cont; i++)
            {
                do
                {
                    Console.WriteLine("Informe o Código da Matéria Prima: ");
                    idMp[i] = Console.ReadLine();
                } while (!MPrimaController.LocalizarMPrima(idMp[i]));
            }


            //guardando data local
            DateTime dabertura = DateTime.Now;
            Console.Write("Data da abertura da produção: " + dabertura + "\n");


            //instancia de obj materia prima
            Producao producao = new Producao(idSequencial, dabertura, cbarra, qtd, idMp, qtdMP);

            return producao;
        }
        public static void EscrevendoArquivo(List<Producao> listProdtion)
        {
            string linha = "";
            string idSeque, dproducao, produto, qtd, mprima, qtdMP;
            for (int i = 0; i < listProdtion.Count; i++)
            {
                idSeque = listProdtion[i].Id.ToString().PadRight(5, ' ');
                dproducao = listProdtion[i].DProducao.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                produto = listProdtion[i].CBarras;
                qtd = listProdtion[i].Qtd.ToString().PadRight(3, ' ');
                mprima = listProdtion[i].IdMP[0].ToString().PadRight(6, ' ') + listProdtion[i].IdMP[1].ToString().PadRight(6, ' ') + listProdtion[i].IdMP[2].ToString().PadRight(6, ' ');
                qtdMP = listProdtion[i].QtdMP[0].ToString().PadRight(6, ' ') + listProdtion[i].QtdMP[1].ToString().PadRight(6, ' ') + listProdtion[i].QtdMP[2].ToString().PadRight(6, ' ');

                linha += idSeque + dproducao + produto + qtd + mprima + qtdMP + "\n";
            }

            using (StreamWriter fileWrite = new StreamWriter(@"C:\Users\55169\Documents\Projeto\Projeto Biltiful\biltiful\files\Producao.txt"))
            {
                fileWrite.WriteLine(linha);
                fileWrite.Close();
            }
        }
    
     public static void LocalizarProduto()
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
        public static List<Produto> ConverterParaLista()
        {
            List<Produto> Produtos = new List<Produto>();
            try
            {
                FileManipulator file = new FileManipulator() { Path = @"C:\Users\Thiago\Desktop\biltiful-grupo1\SysBil\file\", Name = "Produto.txt" };
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
        public static void ImpressaoPorRegistro()
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
        static public string TratamentoString()
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
        public static void EditP()
        {
            List<Produto> listToEdit = new List<Produto>();
            listToEdit = ProdutoController.ConverterParaLista();

            bool itemEncontrado = false;

            Console.Write("Informe o Codigo de barra para alterar: ");
            string buscarId = Console.ReadLine();

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
        public static void EditPSituation()
        {
            List<Produto> listToEdit = new List<Produto>();
            listToEdit = ProdutoController.ConverterParaLista();

            bool itemEncontrado = false;

            Console.Write("Informe o Codigo de barras que deseja alterar: ");
            string buscarId = Console.ReadLine();

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
        public static void EscrevendoArquivo(List<Produto> listP)
        {
            string nome20pos, linha = "";
            string  uvenda, dcadastro;
            for (int i = 0; i < listP.Count; i++)
            {

                nome20pos = listP[i].Nome.PadRight(20, ' ');
                uvenda = listP[i].UVenda.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                dcadastro = listP[i].DCadastro.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                linha += listP[i].CBarras + nome20pos +listP[i].Vvenda + uvenda + dcadastro + listP[i].Situacao.ToString() + "\n";
            }

            using (StreamWriter fileWrite = new StreamWriter(@"C:\Users\Thiago\Desktop\biltiful-grupo1\SysBil\file\Produto.txt"))
            {
                fileWrite.WriteLine(linha);
                fileWrite.Close();
            }
        }
    }
}
