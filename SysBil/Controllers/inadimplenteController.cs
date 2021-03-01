using Model;
using System;
using Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class inadimplenteController
    {
        static public Inadimplente CadastrarInadimplentes()
        {

            Console.WriteLine("Insira o Cpf :");
            long cpf = long.Parse(Console.ReadLine());

            return new Inadimplente()
            {
                Cpf = cpf
            };
        }
        static public List<Inadimplente> ConverterParaList(string[] dadosCru)
        {
            List<Inadimplente> novaLista = new List<Inadimplente>();
            foreach (var novoInadimplente in dadosCru)
            {
                if(novoInadimplente.Length == 11)
                {
                    string cpf = novoInadimplente.Substring(0, 11);
                    novaLista.Add(new Inadimplente { Cpf = long.Parse(cpf) });
                }
                

            }
            return novaLista;
        }
        static public string[] ConverterParaSalvar(List<Inadimplente>inadimplentes)
        {
            StringBuilder inadimplentesSb = new StringBuilder();
            inadimplentes.ForEach(inadimplente => {
                inadimplentesSb.Append(inadimplente.Cpf);
                inadimplentesSb.AppendLine();
            });
            return inadimplentesSb.ToString().Split('\n');
        }
        static public bool VerificarInadimplente(Inadimplente novoInadimplente , List<Inadimplente> inadimplentes)
        {
            if (inadimplentes.Exists(inadimplente => inadimplente.Cpf.Equals(novoInadimplente.Cpf)))
            {
                return true;
            }
            return false;
        }
        static public List<Inadimplente> DeletarInadimplente(List<Inadimplente> inadimplentes)
        {
            string cpf;
            Console.WriteLine("Informe o CPF que deseja retirar da lista de inadimplente");
            cpf = Console.ReadLine();

            if (inadimplentes.Remove(inadimplentes.Find(inadimplente => inadimplente.Cpf.Equals(long.Parse(cpf)))))
            {
               
            Console.WriteLine("\n>>>Excluido com Sucesso<<<\n");
                return inadimplentes;
            }
            else
            {
                Console.WriteLine("\n>>>Cpf não excluido,pois não se encontra no registro<<<\n");
                return inadimplentes;
            }
        }
        static public List<Inadimplente> LocalizarInadimplentes(List<Inadimplente> inadimplentes, string pesqCpf)
        {
            
            Console.WriteLine("Iforme o Cpf para ser localizado:");
            pesqCpf =Console.ReadLine();

            if(inadimplentes.Exists(inadimplente => inadimplente.Cpf.Equals(pesqCpf)))
            {
                Console.WriteLine("CPF Encontrado na lista de risco");
            }
            else
            {
                Console.WriteLine("CPF não se encontra na lista!");
            }
            return inadimplentes;

        }
    }
}

