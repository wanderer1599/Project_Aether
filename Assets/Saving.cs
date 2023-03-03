using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Saving : MonoBehaviour
{
    public string foo2, bar2;
    // Start is called before the first frame update
    void Start()
    {
        GameInfo GI = new GameInfo();
        foo2 = GI.foo;
        bar2 = GI.bar;
        Debug.Log("foo2 is " + foo2 + ". bar2 is " + bar2);
        Debug.Log("foo2 is " + GI.foo + ". bar2 is " + GI.bar);
        GameInfo GI2 = LoadGame();
        Debug.Log("foo2 is " + GI2.foo + ". bar2 is " + GI2.bar);
        GI.foo = "foo";
        GI.bar = "bar";
        Debug.Log("foo2 is " + foo2 + ". bar2 is " + bar2);
        Debug.Log("foo2 is " + GI.foo + ". bar2 is " + GI.bar);
        SaveGame();
        Debug.Log("foo2 is " + foo2 + ". bar2 is " + bar2);
        Debug.Log("foo2 is " + GI.foo + ". bar2 is " + GI.bar);
        Debug.Log("Gene " + GI2.Gene);
        SaveGame();
        GameInfo GI3 = LoadGame();
        Debug.Log("Gene " + GI3.Gene);
        GameInfo GI4 = new GameInfo();
        Debug.Log("Gene " + GI4.Gene);
        Debug.Log("Hi " + GI3.ReturnHi());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveGame()
    {
        GameInfo GI = new GameInfo();
        GI.Gene = "J";
        GI.SetHi(7);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, GI);
        file.Close();
    }
    public GameInfo LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        GameInfo GI = (GameInfo)bf.Deserialize(file);
        //foo2 = GI.foo;
        //bar2 = GI.bar;
        file.Close();
        return GI;
    }
    public void ChgFooBar()
    {

    }
}
