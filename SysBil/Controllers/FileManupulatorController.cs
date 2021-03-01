﻿using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    public class FileManupulatorController
    {
        public static void InicializarArquivo(FileManipulator arquivo)
        {
            if (!File.Exists($@"{arquivo.Path}\{arquivo.Name}"))
            {
                using (File.Create($@"{arquivo.Path}\{arquivo.Name}"))
                {
                    Console.WriteLine($"Arquivo {arquivo.Name} Criado com sucesso!");
                }
            }
        }
        public static void EscreverNoArquivo(FileManipulator arquivo, string[] conteudo)
        {
            using (StreamWriter streamWriter = File.CreateText($@"{arquivo.Path}\{arquivo.Name}"))
            {
                foreach (var linha in conteudo)
                {
                    streamWriter.Write(linha);
                }
            }
        }
        public static string[] LerArquivo(FileManipulator arquivo)
        {
            return File.ReadAllLines($@"{arquivo.Path}\{arquivo.Name}");
        }
    }
}