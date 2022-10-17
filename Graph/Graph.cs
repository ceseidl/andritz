using Graph;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Runtime.Serialization;
using System.IO;
using Force.DeepCloner.Helpers;
using Force.DeepCloner;
using System.Xml.Linq;


namespace Graph
{
    public interface IGraph<T>
    {
        IObservable<IEnumerable<T>> RoutesBetween(T source, T target);
    }

    public class Graph<T> : IGraph<T>
    {
        private IEnumerable<ILink<T>> _links;

        public Graph(IEnumerable<ILink<T>> links)
        {
            _links = links;
        }

        public virtual IObservable<IEnumerable<T>> RoutesBetween(T source, T target)
        {
            List<No> nodes = new List<No>();
            List<string> points = new List<string>();
            List<List<string>> routesBetween = new List<List<string>>();

            List<T> rotas = new List<T>();


            foreach (ILink<T> l in _links)
            {
                if (!points.Contains(l.Source.ToString()))
                {
                    points.Add(l.Source.ToString());

                }
                if (!points.Contains(l.Target.ToString()))
                {
                    points.Add(l.Target.ToString());

                }


                int indexNode = nodes.FindIndex(p => p.ToString().Equals(l.Source.ToString()));
                if (indexNode < 0)
                    nodes.Add(new No(l.Source.ToString(), l.Target.ToString()));

                indexNode = nodes.FindIndex(p => p.ToString().Equals(l.Target.ToString()));
                if (indexNode < 0)
                    nodes.Add(new No(l.Target.ToString(), l.Source.ToString()));



            }
            foreach (No node in nodes)
            {
                node.Conexoes = new List<No>();
            }
            //Cria as conexões
            foreach (No node in nodes)
            {
                List<ILink<T>> listaLinks = _links.Where(p => p.Target.Equals(node.ToString()) || p.Source.Equals(node.ToString())).ToList();

                foreach (ILink<T> l in listaLinks)
                {
                    if (node.Conexoes.FindIndex(p => p.Equals(l.Source)) < 0 && !l.Source.Equals(node.ToString()))
                        node.Conexoes.Add(nodes.Find(p => p.ToString().Equals(l.Source.ToString())));

                    if (node.Conexoes.FindIndex(p => p.Equals(l.Target)) < 0 && !l.Target.Equals(node.ToString()))
                        node.Conexoes.Add(nodes.Find(p => p.ToString().Equals(l.Target.ToString())));


                }

            }


            routesBetween = Util.BuscarCaminhos(source.ToString(), target.ToString(), nodes);




            return (IObservable<IEnumerable<T>>)routesBetween;





        }
    }
}
