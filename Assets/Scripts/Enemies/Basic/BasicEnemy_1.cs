using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Basic Enemy 1
 * 
 *  Basic melee enemy
 *  When the player enters within 10 units, the enemy will chase the player until within 3 units
 *  When within 3 units, the enemy will perform a lunge attack
 *  After the attack, the enemy will move back to its original position prior to the attack
 * 
 */
public class BasicEnemy_1 : MonoBehaviour
{
    // public variables
    public Rigidbody2D rigid;
    public Transform Player;
    public float attackSpeed = 0.5f;
    public float speed = 2f;
    public float maxHealth = 10f;
    public float aggroDistance = 10f;
    public float attackingDistance = 3f;

    // private variables
    private float currHealth;
    private bool isAttacking = false;
    private bool isRecoveringFromAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
        if (Player == null)
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        currHealth = maxHealth;
    }

    void FixedUpdate()
    {
        // check if player is within 10 units
        if (Vector2.Distance(transform.position, Player.position) < aggroDistance)
        {
            // move towards player until within 3 units then attack
            if (Vector2.Distance(transform.position, Player.position) > attackingDistance && !isAttacking)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
            } else 
            {
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        Vector3 posPriorToAttack = transform.position;
        Vector3 playerPos = Player.position;

        yield return new WaitForSeconds(attackSpeed);

        // move to player position until within 1 unit
        while (Vector2.Distance(transform.position, playerPos) > 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerPos, speed * 3 * Time.deltaTime);
            yield return null;
        }

        isRecoveringFromAttack = true;
        // move back to original position prior to attack
        while (transform.position != posPriorToAttack)
        {
            transform.position = Vector2.MoveTowards(transform.position, posPriorToAttack, speed * 3 * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(attackSpeed * 2);
        isRecoveringFromAttack = false;
        isAttacking = false;
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
        if (currHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // when the enemy hits the player, deal damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((isAttacking && !isRecoveringFromAttack) && collision.gameObject.tag == "Player")
        {
            Debug.Log("Attacked");
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
        }
    }
}
