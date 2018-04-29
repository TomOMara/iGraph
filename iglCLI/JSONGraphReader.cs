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
      graph.ValueAxis = GetJSONValueAxis(parsed_file);
      graph.CategoryAxis = GetJSONCategoryAxis(parsed_file);
      graph.Series = GetSeries(parsed_file);


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

      if (chart_type == "LineGraph")
      {
        return "4";  // code for linegraph
      }
      else
      {
        // OTHER CODES USED ON English.nv
        return "0";
      }

    }

    private SGCategoryAxis GetJSONCategoryAxis(Dictionary<string, object> file)
    {

      SGCategoryAxis x_axis = new SGCategoryAxis();
      x_axis.Title = Convert.ToString(jo_parsed_file["encoding"]["x"]["axis"]["title"]);

      // STUBBED
      x_axis.Origin = 0.0;
      x_axis.Width = 500;
      x_axis.PosX = 1;
      x_axis.PrimaryCategoryType = CategoryUnit.MONTH;
      x_axis.CategoriesTypeAxis = TypeAxis.CONTINUOS;

      x_axis.Categories = new List<String>(); // empty list of categories
      string x_axis_labels_str = Convert.ToString(jo_parsed_file["encoding"]["x"]["scale"]["labels"]);
      object x_axis_labels = JsonConvert.DeserializeObject(x_axis_labels_str);

      JArray labels_array = (JArray)x_axis_labels;

      // Add each categoryto our list of categories
      foreach (var category in labels_array.Children())
      {
        x_axis.Categories.Add(category.Value<string>());
      }

      x_axis.StartsAt = x_axis.Categories.First();
      x_axis.EndsAt = x_axis.Categories.Last();
      x_axis.Stepping = Convert.ToString((Double)labels_array.ElementAt(1) - (Double)labels_array.ElementAt(0));
      return x_axis;

    }

    private SGValueAxis GetJSONValueAxis(Dictionary<string, object> file)
    {

      SGValueAxis value_axis = new SGValueAxis();
      var values = jo_parsed_file["encoding"]["y"]["scale"]["values"].AsEnumerable();

      value_axis.Title = Convert.ToString(jo_parsed_file["encoding"]["y"]["axis"]["title"]);
      value_axis.StartsAt = (Double)values.First();
      value_axis.EndsAt = (Double)values.Last();
      value_axis.ScaleUnit = 1;
      value_axis.Stepping = (Double)values.ElementAt(1) - (Double)values.ElementAt(0);

      return value_axis;
    }

    private string GetJSONMainTitle(Dictionary<string, object> file)
    {
      return Convert.ToString(jo_parsed_file["title"]);
    }

    private SGSeriesCollection GetSeries(Dictionary<string, object> file)
    {

      SGSeriesCollection series_collection = new SGSeriesCollection();
      JObject all_series = (JObject)jo_parsed_file["series"];

      foreach (var series in all_series)
      {
        SGSeries parsed_series = new SGSeries();

        parsed_series.Type = 0;
        parsed_series.ID = 1;
        parsed_series.Status = 4;
        parsed_series.Name = series.Key;
        JObject values = (JObject)series.Value;


        List<Object> val_range = new List<Object>();
        List<Object> key_range = new List<Object>();
        List<Object> val_range_copy = new List<object>();
        int last_value_index = -1;
        foreach (var data in values)
        {
          try
          {
            val_range.Add((Double)data.Value);
            key_range.Add((String)data.Key);

            val_range_copy.Add((Double)data.Value);

            last_value_index += 1;
          }
          catch (System.ArgumentException e)
          {
            val_range.Add("none");
            val_range_copy.Add("none");
          }
        }
        parsed_series.Values = val_range;
        parsed_series.EndsAt = (Double)val_range.OfType<Double>().Last();
        parsed_series.CategoryEndsAt = Convert.ToDouble(key_range[last_value_index]);
        parsed_series.CategoryStartsAt = Convert.ToDouble(key_range.First());
        parsed_series.StartsAt = (Double)val_range.OfType<Double>().First();
        parsed_series.Trend = (string)jo_parsed_file["trends"][series.Key];
        parsed_series.ValuesIncludingNones = val_range_copy;

        // add series to collection
        series_collection.Add(parsed_series);
      }

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
      // STUBBED - apparently dont need this?

      SGPlotArea plot_area = new SGPlotArea();
      plot_area.Geometry = new SGGeometry(50, 50, 500.0, 500.0);

      return plot_area;

    }

  }


}
