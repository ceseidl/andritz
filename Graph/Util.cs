using Force.DeepCloner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace Graph
{
    internal static class Util
    {
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }
        public static bool Equals(this List<string> lista, List<string> listaCompare)
        {
            foreach (string str in lista)
            {
                if (str.Equals(listaCompare[lista.IndexOf(str)]))
                    return true;

            }
            return false;

        }
        public static bool Contains(this List<List<string>> rotas, List<string> listaCompare)
        {
            foreach (List<string> l in rotas)
            {
                if (l.Equals(listaCompare))
                    return true;
            }
            return false;

        }
        public static  List<List<string>> BuscarCaminhos(string origem, string destino, List<No> Nos)
        {
            List<List<string>> paths = new List<List<string>>();
            List<List<string>> rotas = new List<List<string>>();
            Dictionary<string, List<string>> dicVisiteds = new Dictionary<string, List<string>>();
           


            List<string> path = new List<string>();
            string next = origem;
            int indice = 0;
            while (Nos.Count >= indice )
            //while(true)
            {
               
                No node = Nos.FirstOrDefault(p => p.ToString().Equals(next));
                node.Ativo = true;

                if ((path.Contains(destino) || path.Contains(origem)) && !rotas.Contains(path))
                {
                    rotas.Add(path);

                    path = new List<string>();
                    foreach (No n in Nos)
                    {
                        n.Ativo = false;
                    }

                }
                foreach (No nextNode in node.Conexoes)
                {
                    if (VerificarSequencia(nextNode.ToString(), rotas, path)) 
                        continue;
                    if (!nextNode.Ativo && !path.Contains(nextNode.ToString()) && !nextNode.ToString().Equals(origem))
                    {
                        path.Add(nextNode.ToString());
                        nextNode.Ativo = true;
                        next = nextNode.ToString();

                        break;
                    }
                }
                indice++;
               
            }

            return rotas;

        }
        private static bool VerificarSequencia(string conexaoAtual, List<List<string>> rotas, List<string> caminhoAtual)
        {
            if (rotas.Count == 0 || caminhoAtual.Count == 0) 
                return false;
            List<string> newList = caminhoAtual;
            newList.Add(conexaoAtual);

            foreach (List<string> path in rotas)
            {
                List<string> listCompare = new List<string>();
                listCompare = DeepClonerExtensions.DeepClone(path);
                listCompare.Add(conexaoAtual);

                if (listCompare.SequenceEqual(newList)) 
                    return true;
            }
            return false;
        }
    }
}
