using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace IGraph.StatGraph
{


  public class SGSeries
  {

    public int Type { get; set; }               // log
    public int ID { get; set; }                 // - 
    public int Status { get; set; }             // -
    public string Name { get; set; }            // 
    public List<object> Values { get; set; }
    public List<object> ValuesIncludingNones { get; set; }
    public double EndsAt { get; set; }
    public double StartsAt { get; set; }
    public double CategoryEndsAt { get; set; }
    public double CategoryStartsAt { get; set; }
    public string Trend { get; set; }
    private object _minValue;
    private object _maxValue;

    private List<int> _maxValuesAt;
    private List<int> _minValuesAt;

    // set _minValue by getting min value in Values 
    public object minValue
    {
      get {
        if (_minValue == null)
          _minValue = Values.Min();
        return _minValue;
      }
    }

    // set _maxValue by getting min value in Values  
    public object maxValue
    {
      get
      {
        if (_maxValue == null)
          _maxValue = Values.Max();
        return _maxValue;
      }
    }

    
    public List<int> getMaxValuesAt()
    {
      if (_maxValuesAt == null)
      {
        int i;
        _maxValuesAt = new List<int>();
        for (i = 0; i < Values.Count; i++)
        {
          if (Values[i].ToString() == maxValue.ToString())
            _maxValuesAt.Add(i);
        }
      }
      return _maxValuesAt;
    }

    public List<int> getMinValuesAt()
    {
      if ( _minValuesAt == null )
      {
        int i;
        _minValuesAt = new List<int>();
        for (i = 0; i < Values.Count; i++)
        {
          if (Values[i].ToString() == minValue.ToString())
            _minValuesAt.Add(i);
        }
      }
      return _minValuesAt;
    }

    public object getValueAt(int idx)
    {
      if (Values.ElementAt(idx) != null)
        return Values.ElementAt(idx);
      else
        return "";
    }

    public int minValueAt()
    {
      return Values.IndexOf(this.minValue);
    }

    public int maxValueAt()
    {
      return Values.IndexOf(this.maxValue);
    }

    public string ValuesToString()
    {
      string vals = "";
      foreach (object o in this.Values)
      {
        if (o != null)
        {
          vals +=  o.ToString() ;
        } else
        {
          vals += "[none, none] ";
        }
      }
      return vals;
    }

    public override string ToString()
    {
      return String.Format("Legal series found: "
        + "ID={0}, name={1}, type={2}, {3} points",
        this.ID, this.Name, this.Type, this.Values.Count);
    }

  }
}
