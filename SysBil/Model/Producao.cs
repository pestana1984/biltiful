using System;
using System.Collections.Generic;
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
            return $"Id: {Id}\nData produção: {DProducao}\nCódigo de Barras: {CBarras}\nQuantidade: {Qtd}\nID Matéria Prima: {IdMP}\nQuantidade: {QtdMP}";
        }
    }
}
