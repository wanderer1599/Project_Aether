using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBoost : Ability
{

    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override IEnumerator Use(CharacterBase character1, CharacterBase character2)
    {
        InitiateAbility(character1);
        yield return new WaitForSeconds(1f);
        //character1.selfCharacter.spd = (int)(character1.selfCharacter.spd * 1.3f);
        //character1.selfCharacter.atk = (int)(character1.selfCharacter.atk * 1.3f);
        //character1.selfCharacter.def = (int)(character1.selfCharacter.def * 1.3f);
        yield return new WaitForSeconds(1f);
        EndAbility(character1);
        //add more stat boosts
    }
}