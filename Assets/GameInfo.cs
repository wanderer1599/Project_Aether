using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameInfo
{
    public string foo = "bar";
    public string bar = "foo";
    public string Gene = "Jean";

    private int hi = 1;

    public int ReturnHi() { return hi; }
    public void SetHi(int hey) { hi = hey; }
}


