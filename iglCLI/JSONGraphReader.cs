using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using log4net;


using IGraph.StatGraph;
using System.Collections;
using IGraph.Utils;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace IGraph.GraphReaders
{

    class JSONGraphReader : IIGraphReader
    {
        private StatisticalGraph graph;
        private JObject jo_parsed_file;

        public JSONGraphReader()
        {

        }

        public List<StatisticalGraph> BuildGraphList(string file, bool gif)
        {
            List<StatisticalGraph> sg_collection = new List<StatisticalGraph>();

            graph = new StatisticalGraph();

            jo_parsed_file = JObject.Parse(File.ReadAllText(file));
            var parsed_file = jo_parsed_file.ToObject<Dictionary<string, object>>();

            graph.Prologue = new GraphPrologue(file,
                                                "JSON",
                                                1,
                                                1,
                                                GetGraphType(parsed_file),
                                                GetGraphHeight(parsed_file),
                                                GetGraphWidth(parsed_file));



            Console.ReadLine();
            graph.MainTitle = GetJSONMainTitle(parsed_file);
            //graph.PlotArea = GetJSONPlotArea(parsed_file);
            //graph.Textboxes = GetJSONTextBoxes(parsed_file);
            graph.Series = GetJSONSeries(parsed_file);
            graph.ValueAxis = GetJSONValueAxis(parsed_file);
            graph.CategoryAxis = GetJSONCategoryAxis(parsed_file);

            sg_collection.Add(graph);

            return sg_collection;
        }

        private double GetGraphWidth(Dictionary<string, object> file)
        {
            object output;
            file.TryGetValue("width", out output);
            Console.WriteLine("width: {0}", output);

            return Convert.ToDouble(output);
        }

        private double GetGraphHeight(Dictionary<string, object> file)
        {
            object output;
            file.TryGetValue("height", out output);
            Console.WriteLine("height {0}", output);

            return Convert.ToDouble(output);   
        }   

        private string GetGraphType(Dictionary<string, object> file)
        {
            object output;
            file.TryGetValue("chart_type", out output );
            Console.WriteLine("Graph type: {0}", output);

            return Convert.ToString(output);
        }

        private SGCategoryAxis GetJSONCategoryAxis(Dictionary<string, object> file)
        {
            object output;
            Console.WriteLine(file.TryGetValue("chart_type", out output));
            throw new NotImplementedException();
        }

        private SGValueAxis GetJSONValueAxis(Dictionary<string, object> file)
        {
            object output;
            Console.WriteLine(file.TryGetValue("chart_type", out output));
            throw new NotImplementedException();
        }

        private string GetJSONMainTitle(Dictionary<string, object> file)
        {
            object output;
            Console.WriteLine(file.TryGetValue("chart_type", out output));
            throw new NotImplementedException();
        }

        private SGSeriesCollection GetJSONSeries(Dictionary<string, object> file)
        {
            object output;
            Console.WriteLine(file.TryGetValue("chart_type", out output));
            throw new NotImplementedException();
        }

        private SGTextBoxCollection GetJSONTextBoxes(Dictionary<string, object> file)
        {
            object output;
            Console.WriteLine(file.TryGetValue("chart_type", out output));
            throw new NotImplementedException();
        }

        private SGPlotArea GetJSONPlotArea(Dictionary<string, object> file)
        {
            object output;
            Console.WriteLine(file.TryGetValue("chart_type", out output));
            throw new NotImplementedException();
        }

    }


}
