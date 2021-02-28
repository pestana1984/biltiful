using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Cliente
    {
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime DNascimento { get; set; }
        public char Sexo { get; set; }
        public DateTime UCompra { get; set; }
        public DateTime DCadastro { get; set; }
        public char Situacao { get; set; }
    }
}

