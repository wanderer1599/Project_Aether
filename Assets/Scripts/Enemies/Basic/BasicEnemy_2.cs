using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  
 * Basic Enemy 2
 * 
 *  Basic ranged enemy
 *  When the player enters within 10 units, the enemy will chase the player until within 5 units
 *  When within 5 units, the enemy will circle around the player
 *  And attack the player with projectiles
 *  When within 4 units, the enemy will move away from the player
 *
 *  Uses BasicProjectile.cs as the projectile
*/
public class BasicEnemy_2 : MonoBehaviour
{
    // public variables
    public Rigidbody2D rigid;
    public Transform Player;
    public GameObject ProjectileObject;
    public float attackSpeed = 0.5f;
    public float speed = 0.75f;
    public float maxHealth = 10f;
    public int numProjectiles = 1;
    public float aggroDistance = 10f;
    public float attackingDistance = 4f;

    // private variables
    private float currHealth;
    private bool isAttacking = false;
    private float attackTimer = 0;

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
        if (Vector2.Distance(transform.position, Player.position) < aggroDistance){

            // move towards player until 5 units
            if (Vector2.Distance(transform.position, Player.position) > 5f && !isAttacking)
            {
                isAttacking = false;
                transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);

            } else if(Vector2.Distance(transform.position, Player.position) < attackingDistance)
            {
                // move away from the player
                isAttacking = false;
                transform.position = Vector2.MoveTowards(transform.position, Player.position, -speed * Time.deltaTime);
                Attack();
                
            } else 
            {
                isAttacking = true;
                // circle around the player
                transform.RotateAround(Player.position, Vector3.forward, 20 * Time.deltaTime);

                // prevent enemy from rotating upside down
                transform.eulerAngles = new Vector3(0, 0, 0);

                Attack();
            }
        }
    }

    void Attack()
    {
        // fire projectile based on attack speed
        if (attackTimer > attackSpeed)
        {
            for (int i = 0; i < 30 * numProjectiles; i += 30){
                // Instantiate projectile
                GameObject projectile = Instantiate(ProjectileObject, transform.position, Quaternion.identity);

                // rotate projectile to face the player
                projectile.transform.up = Player.position - projectile.transform.position;

                // rotate projectile by i degrees and adjust spread based on number of projectiles
                projectile.transform.Rotate(0, 0, i - 15 * (numProjectiles - 1));
            }
            attackTimer = 0;
        } else 
        {
            attackTimer += Time.deltaTime;
        }
    }

    // ignore collision if colliding with player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
