using UnityEngine;
using System.Collections;

public class KomaUtil
{
    public static string GetClassName(string name, string turn, string status)
    {
        if(status == "Raw")
        {
            return turn + name;
        }

        if(name == "Hisha")
        {
            name = "Ryu";
        }
        else if(name == "Kaku")
        {
            name = "Uma";
        }
        else if(name == "Gin")
        {
            name = "Narigin";
        }
        else if(name == "Keima")
        {
            name = "Narikei";
        }
        else if(name == "Kyosha")
        {
            name = "Narikyo";
        }
        else if(name == "Fu")
        {
            name = "Tokin";
        }

        return turn + name;

    }
}
