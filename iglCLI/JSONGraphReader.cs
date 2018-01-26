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

        /* This method builds and returns a list of Statisitcal Graph objects from 
         * a (json) file.  
         */
        public List<StatisticalGraph> BuildGraphList(string file, bool gif)
        {
            // A container for the graphs to be parsed 
            List<StatisticalGraph> sg_collection = new List<StatisticalGraph>();

            StatisticalGraph graph = new StatisticalGraph();

            // Some excel flags we shouldn't need 
            const int graphID = 1;
            const int sheetName = 1;

            jo_parsed_file = JObject.Parse(File.ReadAllText(file));
            var parsed_file = jo_parsed_file.ToObject<Dictionary<string, object>>();


            int one = 1;

            // Prologue which holds metadata about graph, mainly name and size
            graph.Prologue = new GraphPrologue(file,
                                                "JSON",
                                                graphID,
                                                sheetName,
                                                GetGraphType(parsed_file),
                                                GetGraphHeight(parsed_file),
                                                GetGraphWidth(parsed_file));



            graph.MainTitle = GetJSONMainTitle(parsed_file);
            graph.PlotArea = GetJSONPlotArea(parsed_file);
            graph.Textboxes = GetJSONTextBoxes(parsed_file);
            graph.Series = GetSeriesFromSeriesService(parsed_file);
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
            file.TryGetValue("chart_type", out output);
            Console.WriteLine("Graph type: {0}", output);

            return Convert.ToString(output);
        }

        private SGCategoryAxis GetJSONCategoryAxis(Dictionary<string, object> file)
        {

            SGCategoryAxis x_axis = new SGCategoryAxis();

            x_axis.Title = "Year";
            x_axis.Origin = 0.0;
            x_axis.Width = 500;
            x_axis.PosX = 1;
            x_axis.Categories = new List<String>(); // empty list of categories
            x_axis.Categories.Add("1");
            x_axis.Categories.Add("2");
            x_axis.Categories.Add("3");
            x_axis.Categories.Add("4");
            x_axis.Categories.Add("5");
            x_axis.Categories.Add("6");
            x_axis.Categories.Add("7");
            x_axis.Categories.Add("8");

            x_axis.PrimaryCategoryType = CategoryUnit.YEAR;
            x_axis.CategoriesTypeAxis = TypeAxis.CONTINUOS;

            return x_axis;

        }

        private SGValueAxis GetJSONValueAxis(Dictionary<string, object> file)
        {
            // STUBBED 

            SGValueAxis value_axis = new SGValueAxis();

            value_axis.Title = "y axis title";
            value_axis.StartsAt = 1;
            value_axis.EndsAt = 10;
            value_axis.ScaleUnit = 1;
            value_axis.Stepping = 1;

            return value_axis;
        }

        private string GetJSONMainTitle(Dictionary<string, object> file)
        {
            // STUBBED 
            object output;
            file.TryGetValue("title", out output);
            Console.WriteLine("title: {0}", output);
            return "test";
            //return Convert.ToString(output);

        }

        private SGSeriesCollection GetSeriesFromSeriesService(Dictionary<string, object> file)
        {
            // STUBBED 

            // Create SG Series collection
            SGSeriesCollection series_collection = new SGSeriesCollection();
            SGSeries series = new SGSeries();

            // set dummy data for series
            List<object> series_values = new List<Object>();
            int[] point_one = { 1, 1 };
            int[] point_two = { 2, 2 };
            object coord_1 = 1.1;
            object coord_2 = 2.1;
            object coord_3 = 3.1;
            object coord_4 = 4.1;
            object coord_5 = 5.1;
            object coord_6 = 6.1;
            object coord_7 = 7.1;
            object coord_8 = 8.1;

            object[] point_data = { coord_1, coord_2, coord_3,
                coord_4, coord_5, coord_6, coord_7, coord_8};

            series_values.AddRange(point_data);

            // Just return one stubbed series for now. though this is the series we will get from 
            // the python script. 
            series.Type = 0;
            series.ID = 1;
            series.Status = 4;
            series.Name = "main series";
            series.Values = series_values;

            // add series to collection
            series_collection.Add(series); 

            return series_collection; 


        }

        private SGTextBoxCollection GetJSONTextBoxes(Dictionary<string, object> file)
        { 
            // STUBBED

            // return empty text box collection for now
            SGTextBoxCollection text_boxes = new SGTextBoxCollection();

            return text_boxes;
        }

        // uses SGGeometry to set plot -> xpos, ypos, width and height
        private SGPlotArea GetJSONPlotArea(Dictionary<string, object> file)
        {
            // STUBBED 

            SGPlotArea plot_area = new SGPlotArea();
            plot_area.Geometry = new SGGeometry(50, 50, 500.0, 500.0); 

            return plot_area;

        }

    }


}
