using System;
using System.Collections.Generic;
using System.IO;

using log4net;

using IGraph.Utils;
using IGraph.StatGraph;
using IGraph.GraphReaders;
using IGraph.Cleaners;
using IGraph.LanguageGeneration;

namespace IGraph
{
  public static class IGraphLite
  {

    private static readonly ILog log = LogManager.
      GetLogger(typeof(IGraphLite));

    private static LanguageGenerator nlg;
    private static IGraphOptions ops;
    private static JSONGraphReader json_reader;

    public static void Execute(IGraphOptions _ops)
    {
      json_reader = new JSONGraphReader();
      nlg = new LanguageGenerator();

      ops = _ops;

      string dir = ops.DirectoryName;
      string csvFilename = ops.CsvFilename;
      string inputFilename = ops.InputFilename;
      string[] json_files = Directory.GetFiles(dir, "*.json");

      nlg.output_path = ops.OutputDir;

      processFile(inputFilename, null, null);
      log.Info("Successfully released the Excel handle. This is awesome.");
      IGraphConsole.WriteLine("Done.");
      Environment.Exit(IGraphConstants.EXIT_SUCCESS);
    }

    private static void processFile(string f, string lang, string title)
    {
      IGraphConsole.WriteLine("Processing file: " + f);

      List<StatisticalGraph> sgList = json_reader.BuildGraphList(f, ops.writeGIF);

      // was there at least one graph in that file?
      if (sgList == null || sgList.Count == 0 )
      {
        log.Info("No valid graph to process here.");
      }
      else
      {
        foreach (StatisticalGraph sg in sgList)
        {

          // Graph cleaning goes first.
          CleaningManager.CleanGraph(sg);

          // NLG goes last.
          if (nlg.Generate(sg))
          {
            log.Error("This file has wounded me deeply. Please fix it.");
          }
        }
      }
    }
  }
}