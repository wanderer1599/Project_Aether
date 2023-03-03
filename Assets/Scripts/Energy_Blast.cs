using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_Blast : MonoBehaviour
{
    public int atkPowerMult = 1;
    private CharacterBase shooter;
    public int movementSpeed = 5;
    public int movePlayerSpeed = 5;

    public RegularAbilities myRegularAbilities;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   /*public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterBase>())
        {
            //StartCoroutine(HitEffects(collision.gameObject.GetComponent<CharacterBase>()));
            StartCoroutine(myRegularAbilities.hitEffect(shooter, collision.gameObject.GetComponent<CharacterBase>(), atkPowerMult, movePlayerSpeed));
            Destroy(gameObject);
        }
    }*/

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<CharacterBase>())
        {
            //StartCoroutine(HitEffects(collision.gameObject.GetComponent<CharacterBase>()));
            StartCoroutine(myRegularAbilities.hitEffect(shooter, collision.GetComponent<CharacterBase>(), atkPowerMult, movePlayerSpeed));
            StartCoroutine(HitEffects(collision.gameObject.GetComponent<CharacterBase>()));
        }
    }
    public void SetShooter(CharacterBase shooter) { this.shooter = shooter; }
    public IEnumerator HitEffects(CharacterBase hitCharacter)
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1f);        
        hitCharacter.BecomeIdle();
        Debug.Log("TESTING 123");
        Destroy(gameObject);
    }
}