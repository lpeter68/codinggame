using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MinMaxTrace
{
    public int Profondeur { get; set; }
    public List<MinMaxTrace> Childrens { get; set; }

    public Coup Choix { get; set; }
    public int Eval { get; set; }

    public bool? Max { get; set; }

    public MinMaxTrace(int profondeur, bool? max, bool hadChildren = true)
    {
        Profondeur = profondeur;
        Max = max;
        if (hadChildren)
        {
            Childrens = new List<MinMaxTrace>();
        }
        else
        {
            Childrens = null;
        }
    }

    public override string ToString()
    {
        var result = "{\r\n \"Max\": " + (Max == null ? "null" : Max.ToString().ToLower()) + ",\r\n"
           + "\"Eval\": " + Eval + ",\r\n"
           + "\"Choix\": \"" + Choix + "\",\r\n"
           + "\"Profondeur\": " + Profondeur + ",\r\n"
           + "\"Childrens\" : [";
        if (Childrens != null && Childrens.Any())
        {
            result += "\r\n";
            foreach (var item in Childrens)
            {
                if (item != Childrens.First())
                {
                    result += ",";
                }
                result += item.ToString();
            }
        }
        result += "] \r\n}\r\n";
        return result;
    }
}

