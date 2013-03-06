﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using GraphSharp.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using MicroMvvm;
using MicrosoftJava.Shared;

namespace WordCloud {
    class GraphViewModel : ObservableObject {
        #region Members

        private string layoutAlgorithmType;
        private PocGraph graph = new PocGraph(true);
        private List<PocVertex> existingVertices = new List<PocVertex>();
        private List<String> layoutAlgorithmTypes = new List<string>();
        private ProjectViewModel parent;

        #endregion

        #region Constructor

        public GraphViewModel(ProjectViewModel parent) {
            this.parent = parent;

        }

        #endregion 

        #region Private Methods

        public void RunGraph(string startWord, List<Word> wordList) {
            var qualifiedWords = EditDistance.GetShortestLevenshtein(startWord, wordList.Select(w => w.Name).ToList());
            foreach(KeyValuePair<string, int> w in qualifiedWords) {
                existingVertices.Add(new PocVertex(w.Key, w.Value));
            }

            foreach (PocVertex vertex in existingVertices)
                graph.AddVertex(vertex);

            //add some edges to the graph
            for (int i = 1; i < existingVertices.Count; i++) {
                AddNewGraphEdge(existingVertices[0], existingVertices[i]);
            }

            //Add Layout Algorithm Types
            layoutAlgorithmTypes.Add("BoundedFR");
            layoutAlgorithmTypes.Add("Circular");
            layoutAlgorithmTypes.Add("CompoundFDP");
            layoutAlgorithmTypes.Add("EfficientSugiyama");
            layoutAlgorithmTypes.Add("FR");
            layoutAlgorithmTypes.Add("ISOM");
            layoutAlgorithmTypes.Add("KK");
            layoutAlgorithmTypes.Add("LinLog");
            layoutAlgorithmTypes.Add("Tree");

            //Pick a default Layout Algorithm Type
            LayoutAlgorithmType = "Tree";
        }

        private PocEdge AddNewGraphEdge(PocVertex from, PocVertex to) {
            string edgeString = string.Format("{0}-{1} Connected", from.word, to.word);

            PocEdge newEdge = new PocEdge(edgeString, from, to);
            Graph.AddEdge(newEdge);
            return newEdge;
        }

        public void ReLayoutGraph() {
            //graph = new PocGraph(true);
            //List<PocVertex> existingVertices = new List<PocVertex>();
            //existingVertices.Add(new PocVertex("aaabbbac", 0));
            //existingVertices.Add(new PocVertex("aaabbbacbb", 2));
            //existingVertices.Add(new PocVertex("abbbac", 2));
            //existingVertices.Add(new PocVertex("aaabbb", 2));

            //foreach (PocVertex vertex in existingVertices)
            //    Graph.AddVertex(vertex);


            ////add some edges to the graph
            //AddNewGraphEdge(existingVertices[0], existingVertices[1]);
            //AddNewGraphEdge(existingVertices[0], existingVertices[2]);

            //AddNewGraphEdge(existingVertices[0], existingVertices[3]);

            //count++;



            //List<PocVertex> existingVertices = new List<PocVertex>();
            //existingVertices.Add(new PocVertex(String.Format("Barn Rubble{0}", count), true)); //0
            //existingVertices.Add(new PocVertex(String.Format("Frank Zappa{0}", count), false)); //1
            //existingVertices.Add(new PocVertex(String.Format("Gerty CrinckleBottom{0}", count), true)); //2


            //foreach (PocVertex vertex in existingVertices)
            //    Graph.AddVertex(vertex);


            ////add some edges to the graph
            //AddNewGraphEdge(existingVertices[0], existingVertices[1]);
            //AddNewGraphEdge(existingVertices[0], existingVertices[2]);


            //        NotifyPropertyChanged("Graph");




        }

        #endregion

        #region Public Properties

        public string Title {
            get {
                return "Graph";
            }
        }


        public List<String> LayoutAlgorithmTypes {
            get { return layoutAlgorithmTypes; }
        }


        public string LayoutAlgorithmType {
            get { return layoutAlgorithmType; }
            set {
                layoutAlgorithmType = value;
                RaisePropertyChanged("LayoutAlgorithmType");
            }
        }

        public PocGraph Graph {
            get { return graph; }
            set {
                graph = value;
                //RaisePropertyChanged("Graph");
            }
        }
        #endregion

        #region Commands

        public ICommand GraphTextBlockClick { get { return new RelayCommand<object>((param) => this.GraphTextBlockClickExecute(param)); } }

        void GraphTextBlockClickExecute(object parameter) {
            TextBlock clickedItem = parameter as TextBlock;
            int pos = 0;
            int prevItemsCount = existingVertices.Count;

            /* Replace that with algorithm return */
            Dictionary<string, int> words = new Dictionary<string, int>()
            {
                 {"test1", 2},
                 {"test2", 2},
                 {"test3", 2},
            };

            for (int i = 0; i < existingVertices.Count; i++) {
                if (existingVertices[i].word.Equals(clickedItem.Text)) {
                    pos = i;
                    break;
                }

            }

            foreach (KeyValuePair<string, int> item in words)
                existingVertices.Add(new PocVertex(item.Key, item.Value));

            foreach (PocVertex vertex in existingVertices)
                graph.AddVertex(vertex);


            //add some edges to the graph

            for (int i = 0; i < words.Count; i++) {
                AddNewGraphEdge(existingVertices[pos], existingVertices[prevItemsCount + i]);

            }


        }

        #endregion

    }
}