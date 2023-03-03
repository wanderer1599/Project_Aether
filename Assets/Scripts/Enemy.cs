using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase
{
    // Start is called before the first frame update

    public int movementSpeed = 5;

    public enum AIModeNow { FollowPlayer, Punch, Ability}; //What it's doing now
    public AIModeNow aiModeNow;

    public enum AIModePlan { Punch, Ability, Undecided}; //What the overall plan
    public AIModePlan aiModePlan;

    public float distanceTillAttack = 1.2f;

    [SerializeField]
    public int APReward, SPReward, pointsToWinner = 5, guard;
    
    //public List<Ability> enemyAbilities = new List<Ability>();

    void Start()
    {
        //SetStats();
        //selfCharacter.hp = selfCharacter.maxHP;
        selfCharacter.SetStats();
        selfCharacter.SetSelf(this);
        StartCoroutine(InitiateAI());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator InitiateAI()
    {
        yield return new WaitForSeconds(1f);
        int punchedTimes = 0, maxAmountPunch = 3;
        while (gameObject && enemyCharacter) {
            Debug.Log("attacking " + attacking);
            Debug.Log(CantMove());
            //Debug.Log()
            while (CantMove()) yield return new WaitForSeconds(0.1f);
            Debug.Log("1");
            if (aiModePlan == AIModePlan.Ability && !usingAbility) aiModePlan = AIModePlan.Undecided;
            Debug.Log("2");
            yield return new WaitForSeconds(0.1f);
            if (abilities.Count > 0 && aiModePlan == AIModePlan.Undecided)
            {
                Debug.Log(3);
                int shouldUseAbility = Random.Range(0, abilities.Count + 1);
                if (shouldUseAbility < abilities.Count)
                {
                    StartCoroutine(abilities[shouldUseAbility].Use(this, enemyCharacter));
                    aiModePlan = AIModePlan.Ability;
                }
                else aiModePlan = AIModePlan.Punch;
            }
            else aiModePlan = AIModePlan.Punch;
                while (Vector2.Distance(gameObject.transform.position, enemyCharacter.gameObject.transform.position) > distanceTillAttack && aiModePlan == AIModePlan.Punch){
                Debug.Log("4");
                while (CantMove()) yield return new WaitForSeconds(0.1f);
                Debug.Log(5);
                Vector2 movement = enemyCharacter.gameObject.transform.position - gameObject.transform.position;
                    int direction;
                    
                    if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
                    {
                        
                        if (movement.x <= 0f) direction = 4;
                        else direction = 2;
                    }
                    else
                    {
                        if (movement.y <= 0f) direction = 3;
                        else direction = 1;
                    }
                this.direction = direction;
                    movement.Normalize();
                //animator.SetInteger("walk", this.direction);
                    SetAnimation("walk");
                    yield return new WaitForSeconds(0.1f);
                    gameObject.GetComponent<Rigidbody2D>().velocity = movement * movementSpeed;

                //if (movement == Vector2.zero && !CantMove())
                //{
                //    SetAnimation("idle");
                //}
                bool breakLoop = false;
                for (int i = 1; i <= 5; i++)
                {
                    yield return new WaitForSeconds(0.2f);
                    Debug.Log(Vector2.Distance(gameObject.transform.position, enemyCharacter.gameObject.transform.position));
                    if (Vector2.Distance(gameObject.transform.position, enemyCharacter.gameObject.transform.position) <= distanceTillAttack)
                    {
                        breakLoop = true;
                        break;
                    }
                }
                if (breakLoop)
                {
                    Debug.Log("Break loop");
                    BecomeIdle();
                    break;
                }
            }
            if (punchedTimes < maxAmountPunch && Vector2.Distance(gameObject.transform.position, enemyCharacter.gameObject.transform.position) <= distanceTillAttack)
            {
                StartCoroutine(myRegularAbilities.InitiateAttack(this, enemyCharacter));
                punchedTimes++;
            }
            else { punchedTimes = 0; aiModePlan = AIModePlan.Undecided; }
            yield return new WaitForSeconds(2f);
            //Debug.Log("Punch here");
        }
    }
    public void EnemyApplyAttack() { myRegularAbilities.ApplyAttack(this, enemyCharacter); }
}