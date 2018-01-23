using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using log4net;


using IGraph.StatGraph;
using System.Collections;
using IGraph.Utils;

namespace IGraph.GraphReaders
{

    class JSONGraphReader : IIGraphReader
    {
        private StatisticalGraph graph;

        public JSONGraphReader()
        {

        }

        public List<StatisticalGraph> BuildGraphList(string file, bool gif)
        {
            List<StatisticalGraph> sg_collection = new List<StatisticalGraph>();

            graph = new StatisticalGraph();

            graph.Prologue = new GraphPrologue(file,
                                            "JSON",
                                            1,
                                            1,
                                            GetGraphType(),
                                            getGraphHeight(),
                                            GetGraphWidth());

            graph.PlotArea = GetJSONPlotArea(file);
            graph.Textboxes = GetJSONTextBoxes(file);
            graph.Series = GetJSONSeries(file);
            graph.MainTitle = GetJSONMainTitle(file);
            graph.ValueAxis = GetJSONValueAxis(file);
            graph.CategoryAxis = GetJSONCategoryAxis(file);

            sg_collection.Add(graph); 

        }

        private SGCategoryAxis GetJSONCategoryAxis(string file)
        {
            throw new NotImplementedException();
        }

        private SGValueAxis GetJSONValueAxis(string file)
        {
            throw new NotImplementedException();
        }

        private string GetJSONMainTitle(string file)
        {
            throw new NotImplementedException();
        }

        private SGSeriesCollection GetJSONSeries(string file)
        {
            throw new NotImplementedException();
        }

        private SGTextBoxCollection GetJSONTextBoxes(string file)
        {
            throw new NotImplementedException();
        }

        private SGPlotArea GetJSONPlotArea(string file)
        {
            throw new NotImplementedException();
        }

        private double GetGraphWidth()
        {
            throw new NotImplementedException();
        }

        private double getGraphHeight()
        {
            throw new NotImplementedException();
        }

        private int GetGraphType()
        {
            throw new NotImplementedException();
        }


    }


}
