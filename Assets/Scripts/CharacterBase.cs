using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterBase : MonoBehaviour
{
    public Animator animator;

    public Character selfCharacter;

    public CharacterBase enemyCharacter;

    public bool defeated = false;

    public RegularAbilities myRegularAbilities;

    public bool attacking = false, usingAbility = false, beingHit = false, dodging = false,  blocking = false;

    public List<Ability> abilities = new List<Ability>();

    public int direction = 2;

    public bool initiatedBlocking = false;

    public Ability generalBoost, EnergyBall, SuperBlock;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public bool CantMove()
    {
        return attacking || usingAbility || beingHit || dodging || blocking; 
    }
    public void BecomeIdle()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        SetAnimation("idle");
    }
    public void SetAnimation(string animation)
    {
        string [] animationStates = new string[7] { "charge", "walk", "release", "punch", "block", "idle", "dodging"};
        foreach (string i in animationStates) animator.SetInteger(i, 0);
        animator.SetInteger(animation, direction);
    }
}