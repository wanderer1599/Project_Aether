using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SavingAndLoading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveGame()
    {
        PlayerData playerData = new PlayerData();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/Save.sav");
        bf.Serialize(file, playerData);
        file.Close();
    }
    public void LoadGame(Player player, MonoBehaviour whichScript)
    {
        if (File.Exists(Application.persistentDataPath + "/Save.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/Save.sav", FileMode.Open);
            PlayerData playerData = (PlayerData)bf.Deserialize(file);
            file.Close();
            if(whichScript is Stat_Manager)
            {
                Stat_Manager stat_Manager = (Stat_Manager)whichScript;
                stat_Manager.SetSavedData(playerData);
            }
            playerData.SetDataPlayer(player);
        }
        else if(whichScript is Stat_Manager)
        {
            Stat_Manager stat_Manager = (Stat_Manager)whichScript;
            stat_Manager.InitializeStats();
        }
            
        else if(whichScript is Player) player.selfCharacter.SetStats(); //make into else if whichscript is..
    }
}