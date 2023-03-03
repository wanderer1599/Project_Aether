using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Stat_Manager : MonoBehaviour
{

    public Player player;

    public int iHP = 80, iAtk = 5, iDef = 3, iSpd = 4, iAcc, ipoints = 5, blockMPReward, origGuardBreak; //initial stats can be altered in the
                                                                          //Unity Editor.
    private int HP = 80, Atk = 5, Def = 3, Spd = 4, Acc = 4, AP = 5, guardBreak, SP; //The stats to save

    private int dHP, dAtk, dDef, dSpd, dAcc, dPoints; //In case of reset, the above stats return to these

    public Text pointsText, HPText, AtkText, DefText, SpdText;

    public InputField HPIF, AtkIF, DefIF, AccIF, SpdIF;

    public Text HPPlaceHolder, AtkPlaceHolder, DefPlaceHolder, SpdPlaceHolder;

    public SavingAndLoading savingAndLoading;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("first set stats " + firstSetStats);
        //if (PlayerPrefs.GetInt("firstSetStats", 0) == 1) firstSetStats = false;
        //if (firstSetStats) //checks is this the first time I am doing this?
        //if (!File.Exists(Application.persistentDataPath + "/Save.sav"))
        savingAndLoading.LoadGame(player, this);



        //else //if not, then get the stats from the saved locations
        //{
        //
        /*
            Debug.Log("Call else statement start");
            HP = PlayerPrefs.GetInt("HP");
            dHP = HP;
            points = PlayerPrefs.GetInt("Points");
            dPoints = points;
            Atk = PlayerPrefs.GetInt("Atk");
            dAtk = Atk;
            Def = PlayerPrefs.GetInt("Def");
            dDef = Def;
            Spd = PlayerPrefs.GetInt("Spd");
            dSpd = Spd;
            DisplayStats();*/

        }
    


    public void AddStat(string whichStat)
    {
        if (int.Parse(AtkIF.text) > SP)
        {
            Debug.LogError("Not enough points for this operation!");
            return;
        }
        if (int.Parse(AtkIF.text) <= 0)
        {
            Debug.LogError("Less than 0 points inputted!");
            return;
        }
        switch (whichStat) {
            case "HP":
                if (HPIF.text == "") HPIF.text = "0";
                HP += int.Parse(HPIF.text) * 10;
                break;
            case "Atk":
                if (AtkIF.text == "") AtkIF.text = "0";
                Atk += int.Parse(AtkIF.text);
                break;
            case "Def":
                if (DefIF.text == "") DefIF.text = "0";
                Def += int.Parse(DefIF.text);
                break;
            case "Acc":
                if (AccIF.text == "") AccIF.text = "0";
                Acc += int.Parse(AccIF.text);
                break;
            case "Spd":
                if (SpdIF.text == "") SpdIF.text = "0";
                Spd += int.Parse(SpdIF.text);
                break;

            default:
                Debug.LogError("Wrong stat!");
                break;

        }
        SP -= int.Parse(HPIF.text);
        DisplayStats();
    }



    public void AddAtk()
    {
        if (AtkIF.text == "") AtkIF.text = "0";
        if (int.Parse(AtkIF.text) > SP || int.Parse(AtkIF.text) <= 0) return;
        Atk += int.Parse(AtkIF.text) * 1;
        AtkText.text = "Atk: " + Atk.ToString();
        SP -= int.Parse(AtkIF.text);
        DisplayStats();
    }

    public void AddDef()
    {
        if (DefIF.text == "") DefIF.text = "0";
        if (int.Parse(DefIF.text) > SP || int.Parse(DefIF.text) <= 0) return;
        Def += int.Parse(DefIF.text) * 1;
        DefText.text = "Def: " + Def.ToString();
        SP -= int.Parse(DefIF.text);
        DisplayStats();
    }

    public void AddSpd()
    {
        if (SpdIF.text == "") SpdIF.text = "0";
        if (int.Parse(SpdIF.text) > SP || int.Parse(SpdIF.text) <= 0) return;
        Spd += int.Parse(SpdIF.text) * 1;
        SpdText.text = "Spd: " + Spd.ToString();
        SP -= int.Parse(SpdIF.text);
        DisplayStats();
    }

    public void ResetStats()
    {
        SP = dPoints;
        HP = dHP;
        Atk = dAtk;
        Def = dDef;
        Spd = dSpd;
        DisplayStats();
    }

    public void Submit() //submits changes into the memory and loads the home base screen
    {
        //AP, SP, maxExp, origGuardBreak, lvl, exp, maxHP, origAtk, origDef, origAcc, origSpd, origGuardBreak, blockMPReward = 2;
        Character character = player.selfCharacter;
        character.maxHP = HP;
        character.origAtk = Atk;
        character.origDef = Def;
        character.origSpd = Spd;
        character.origAcc = Acc;
        character.blockMPReward = blockMPReward;
        player.AP = AP;
        player.SP = SP;
        player.origGuardBreak = guardBreak;

        /*PlayerPrefs.SetInt("HP", HP);
        PlayerPrefs.SetInt("Atk", Atk);
        PlayerPrefs.SetInt("Def", Def);
        PlayerPrefs.SetInt("Spd", Spd);
        PlayerPrefs.SetInt("Points", points);
        PlayerPrefs.SetInt("firstSetStats", 1);
        */

        //saves this so that it will know that it's not the first
        //time into the character customization screen
        SceneManager.LoadScene("home base");
    }

    public void DisplayStats()
    {
        pointsText.text = "Points: " + SP;
        HPText.text = "HP: " + HP.ToString();
        AtkText.text = "Atk: " + Atk.ToString();
        DefText.text = "Def: " + Def.ToString();
        SpdText.text = "Speed: " + Spd.ToString();
    }

    [ContextMenu("Delete All Saves")] //this creates the menu when you click the "gadget" of your script
                                      //component and you can from the editor delete the stats and reset the stats.
    public void DeleteAllSaves()
    {
        SP = ipoints;
        Atk = iAtk;
        Def = iDef;
        Spd = iSpd;
        HP = iHP;
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void InitializeStats()
    {
        Debug.Log("Call if statement stats");
        dHP = HP = iHP;
        dAtk = Atk = iAtk;
        dDef = Def = iDef;
        dSpd = Spd = iSpd;
        dAcc = Acc = iAcc;
        dPoints = SP = ipoints;
        DisplayStats();
    }
    public void SetSavedData(PlayerData playerData)
    {
        HP = playerData.hp;
        Atk = playerData.atk;
        Def = playerData.def;
        Spd = playerData.spd;
        Acc = playerData.acc;
        AP = playerData.AP;
        SP = playerData.SP;
        blockMPReward = playerData.blockMPReward;
    }
}