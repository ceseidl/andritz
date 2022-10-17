using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Graph
{
    public class No
    {
        string no = String.Empty;
        List<List<No>> rotas = new List<List<No>>();
        List<No> conexoes = new List<No>();
        bool ativo = false;

        public bool Ativo { get; set; }
      

        public List<No> Conexoes { get; set; }
       
        public No(string No)
        {
            no = No;
            conexoes = new List<No>();
        }

        public No(string No, List<No> Nos)
        {
            no = No;
            conexoes = Nos;

        }
        public No(string No, string NoSeguinte)
        {
            no = No;
            conexoes.Add(new No(NoSeguinte));

        }
        public No(string No, string NoSeguinte, string NoSeguinte1)
        {
            this.no = No;
            this.conexoes.Add(new No(NoSeguinte));
            this.conexoes.Add(new No(NoSeguinte1));

        }
        public List<string> GetConexoes()
        {
            return conexoes.Select(p => p.ToString()).ToList();

        }

        public List<List<No>> Rotas { get; set; }
        
        public override string ToString()
        {
            return no.ToString();

        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public IDisposable Subscribe(IObserver<No> observer)
        {
            throw new NotImplementedException();
        }
    }
}
