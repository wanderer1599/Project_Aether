using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Super_Block : Ability
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
        character1.selfCharacter.certainBlock += 3; //maybe also add mp reward to punch effect
        character1.selfCharacter.blockMPReward = 4;
        EndAbility(character1);
    }
}
