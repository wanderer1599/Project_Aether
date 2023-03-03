using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_Blast_Ability : Ability
{
    // Start is called before the first frame update
    public GameObject EnergyBlast;

    public int ballSpeed = 5;

    

    void Start()
    {
        //playWhichAnimation = "release";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override IEnumerator Use(CharacterBase character1, CharacterBase character2)
    {
        if (character1.CantMove() || !character2) yield break;
        InitiateAbility(character1);
        GameObject EnergyBlastInstance = Instantiate(EnergyBlast);
        EnergyBlastInstance.SetActive(true);
        EnergyBlastInstance.GetComponent<Energy_Blast>().SetShooter(character1);
        Vector2 energyBallDirection = Vector2.zero;
        switch (character1.direction)
        {
            case 1: EnergyBlastInstance.transform.position = new Vector3(character1.gameObject.transform.position.x, character1.gameObject.transform.position.y + 2f, -1f);
            energyBallDirection = Vector2.up;
            break;
            case 2: EnergyBlastInstance.transform.position = new Vector3(character1.gameObject.transform.position.x + 2f, character1.gameObject.transform.position.y, -1f);
                energyBallDirection = Vector2.right;
                break;
            case 3: EnergyBlastInstance.transform.position = new Vector3(character1.gameObject.transform.position.x, character1.gameObject.transform.position.y - 2f, -1f);
                energyBallDirection = Vector2.down;
                break;
            case 4: EnergyBlastInstance.transform.position = new Vector3(character1.gameObject.transform.position.x - 2f, character1.gameObject.transform.position.y, -1f);
                energyBallDirection = Vector2.left;
                break;
        }
        EnergyBlastInstance.GetComponent<Rigidbody2D>().velocity = energyBallDirection * ballSpeed;
        yield return new WaitForSeconds(1f);
        EndAbility(character1);
    }
}
