using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int hp, atk, def, acc, spd, guardBreak, blockMPReward = 2, AP, SP, exp, maxExp, lvl; //add new ability that increases points returned from blocking!!

    public void SetData(Player player)
    {
        Character playerData = player.selfCharacter;
        hp = playerData.GetHP();
        atk = playerData.GetAtk();
        def = playerData.GetDef();
        acc = playerData.GetAcc();
        spd = playerData.GetSpd();
        guardBreak = playerData.origGuardBreak;
        blockMPReward = playerData.blockMPReward;
        AP = player.AP;
        SP = player.SP;
        exp = player.maxExp;
        maxExp = player.maxExp;
        lvl = player.lvl;
        blockMPReward = playerData.blockMPReward;
        //set more of the above
        //when settings like def higher make it def = orig * boost (like 1.3f)
    }
    public void SetDataPlayer(Player player) {
        Character character = player.selfCharacter;
        character.maxHP = hp;
        character.origAtk = atk;
        character.origDef = def;
        character.origAcc = acc;
        character.origSpd = spd;
        character.blockMPReward = blockMPReward;
        character.origGuardBreak = guardBreak;
        player.AP = AP;
        player.SP = SP;
        player.exp = exp;
        player.maxExp = maxExp;
        player.lvl = lvl;
    }
}