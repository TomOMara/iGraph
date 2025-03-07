﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace IGraph.StatGraph
{
  public class GraphPrologue
  {

    private string file,origin, type;
    private int id, sheet;
    double gheight, gwidth;

    public GraphPrologue(string fileName,
                         string graphOrigin,
                         int graphID,
                         int sheetName,
                         string graphType,
                         double graphHeight,
                         double graphWidth)
    {
      file = fileName;
      origin = graphOrigin;
      id = graphID;
      type = graphType;
      sheet = sheetName;
      gheight = graphHeight;
      gwidth = graphWidth;
    }

    public string GetOrigin()
    {
      return origin;
    }

    public string GetGraphType()
    {
      return type;
    }

    public double GetGraphWidth()
    {
      return gwidth;
    }

    public double GetGraphHeight()
    {
      return gheight;
    }

    public string GetGraphName()
    {
      return Path.GetFileName(file).Split('.')[0];
    }
    public string GetLanguageByFilename()
    {
      if (GetOriginalExcelFile().StartsWith("c"))
      {
        return "English";
      }

      if (GetOriginalExcelFile().StartsWith("g"))
      {
        return "French";
      }

      return "None";
    }

    public string GetAbsolutePathFile()
    {
      return Path.GetFullPath(file);
    }

    public string GetOriginalExcelFile()
    {
      return Path.GetFileName(file);
    }

    public string GetOriginalExcelFilenNoExtension()
    {
      return Path.GetFileName(file).Split('.')[0];
    }

    public string GetGraphOriginalDirectory()
    {
      string path = Path.GetDirectoryName(GetAbsolutePathFile()) + @"\";
      return path = replacePathSlashesIfNecessary(path);
    }

    public string GetGifName()
    {
      return this.GetGraphOriginalDirectory() + this.GetGraphName() + ".gif";
    }

    public string PrologueToString()
    {
      string s = String.Format("Graph name is {0}, created with {1}, ",
        this.GetGraphName(), this.origin);
      s += String.Format("embedded in sheet {0}", this.sheet);
      return s;
    }

    private string replacePathSlashesIfNecessary(string path)
    {

      int unix_code = (int)Environment.OSVersion.Platform;

      // If the platform is unix based replace the path, replace path slashes
      if (unix_code == 4 || unix_code == 6 || unix_code == 128)
      {
        path = path.Replace("\\", "/");
      };

      return path;

    }

  }
}
