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

            // Prologue which holds metadata about graph, mainly name and size
            graph.Prologue = new GraphPrologue(file,
                                                "JSON",
                                                graphID,
                                                sheetName,
                                                GetGraphType(),
                                                GetGraphHeight(),
                                                GetGraphWidth());



            graph.MainTitle = GetJSONMainTitle(parsed_file);
            graph.PlotArea = GetJSONPlotArea(parsed_file);
            graph.Textboxes = GetJSONTextBoxes(parsed_file);
            graph.Series = GetSeriesFromSeriesService(parsed_file);
            graph.ValueAxis = GetJSONValueAxis(parsed_file);
            graph.CategoryAxis = GetJSONCategoryAxis(parsed_file);

            sg_collection.Add(graph);

            return sg_collection;
        }

        private double GetGraphWidth()
        {
            return Convert.ToDouble(jo_parsed_file["width"]);
        }

        private double GetGraphHeight()
        {
            return Convert.ToDouble(jo_parsed_file["height"]);
        }

        private string GetGraphType()
        {
            string chart_type = Convert.ToString(jo_parsed_file["chart_type"]);

            if (chart_type == "LineGraph") {
                return "4";  // code for linegraph 
            } else {
              // OTHER CODES USED ON English.nv 
              return "0";
            }

        }

        private SGCategoryAxis GetJSONCategoryAxis(Dictionary<string, object> file)
        {

            SGCategoryAxis x_axis = new SGCategoryAxis();
            x_axis.Title = Convert.ToString(jo_parsed_file["encoding"]["x"]["field"]);

            // STUBBED
            x_axis.Origin = 0.0;
            x_axis.Width = 500;
            x_axis.PosX = 1;
            x_axis.PrimaryCategoryType = CategoryUnit.MISC;
            x_axis.CategoriesTypeAxis = TypeAxis.DISCRETE;


            x_axis.Categories = new List<String>(); // empty list of categories
            string x_axis_labels_str = Convert.ToString(jo_parsed_file["encoding"]["x"]["scale"]["labels"]);
            object x_axis_labels = JsonConvert.DeserializeObject(x_axis_labels_str);

            JArray labels_array = (JArray)x_axis_labels;

            // Add each categoryto our list of categories
            foreach (var category in labels_array.Children())
            {
                x_axis.Categories.Add(category.Value<string>());
            }

            return x_axis;

        }

        private SGValueAxis GetJSONValueAxis(Dictionary<string, object> file)
        {

            SGValueAxis value_axis = new SGValueAxis();
            var values =  jo_parsed_file["encoding"]["y"]["scale"]["values"].AsEnumerable();

            value_axis.Title = Convert.ToString(jo_parsed_file["encoding"]["y"]["field"]);
            value_axis.StartsAt =  (Double)values.First();
            value_axis.EndsAt = (Double)values.Last();
            value_axis.ScaleUnit = 1;
            value_axis.Stepping = (Double)values.ElementAt(1) - (Double)values.ElementAt(0);

            return value_axis;
        }

        private string GetJSONMainTitle(Dictionary<string, object> file)
        {
            return Convert.ToString(jo_parsed_file["title"]);
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

            object[] point_data = { coord_1, coord_2, coord_3,
                coord_4, coord_5, coord_6 };

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
