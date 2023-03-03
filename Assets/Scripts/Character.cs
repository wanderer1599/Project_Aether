using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Character", menuName = "Character")]
public class Character : ScriptableObject
{
    [SerializeField]
    private new string name;

    private CharacterBase self;

    public RegularAbilities regularAbilities;

    public int maxHP, origAtk, origDef, origAcc, origSpd, origGuardBreak, blockMPReward = 2;
    private int hp, atk, def, acc, spd;
    
    public int certainBlock = 3;

    

    public void SetSelf(CharacterBase characterBase) { self = characterBase; }

    public int GetHP()
    {
        return hp;
    }
    public int GetAtk()
    {
        return atk;
    }
    public int GetDef()
    {
        return def;
    }
    public int GetAcc()
    {
        return acc;
    }
    public int GetSpd()
    {
        return spd;
    }
    public void SetStats()
    {
        hp = maxHP;
        atk = origAtk;
        def = origDef;
        spd = origSpd;
        acc = origAcc;
    }
    public void SetHP(int hp, CharacterBase enemy)
    {
        this.hp -= hp;
        if (hp <= 0) regularAbilities.FinishDefeated(self, enemy);
    }
}