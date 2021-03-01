using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Producao
    {
        public int Id { get; set; }
        public DateTime DProducao { get; set; }
        public string CBarras { get; set; }
        public int Qtd { get; set; }
        public string[] IdMP{ get; set; }
        public double[] QtdMP { get; set; }

        public Producao()
        {

        }

        public Producao(int id, DateTime dp, string cb, int qtd, string[] idmp, double[] qtdmp)
        {
            Id = id;
            DProducao = dp;
            CBarras = cb;
            Qtd = qtd;
            IdMP = idmp;
            QtdMP = qtdmp;
        }

        public override string ToString()
        {
            string materiaPrima = "";
            for (int i = 0; i < QtdMP.Length; i++)
            {
                if (QtdMP[i] != null)
                {
                    materiaPrima += IdMP[i].ToString() + "  -  " + QtdMP[i].ToString() + "\n";
                }
            }
            return $"Id: {Id}\nData produção: {DProducao.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)}\nCódigo de Barras: {CBarras}\nQuantidade: {Qtd}\nMatéria Prima:\n  Id  -  Quantidade\n{materiaPrima}";
        }
    }
}
