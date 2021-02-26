using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuloVendas
{
    class FileManipulator
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }


        public void InitializeFile(List<Cliente> contactList)
        {
            if (!File.Exists($@"{FilePath}\{FileName}.{FileExtension}"))
            {
                using (File.Create($@"{FilePath}\{FileName}.{FileExtension}"))
                {
                    Console.WriteLine($"Arquivo {FileName}.{FileExtension} criado!");
                }
            }            
        }

        public void WriteInFile(List<Venda> listaContato)
        {
            using (StreamWriter streamWriter = File.CreateText($@"{FilePath}\{FileName}.{FileExtension}"))
            {


            }
        }
        public void WriteInFile(Venda)
        {
            using (StreamWriter streamWriter = File.AppendText($@"{FilePath}\{FileName}.{FileExtension}"))
            {
                streamWriter.WriteLine(contato.);
            }
        }
    }
}
