using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegularAbilities : MonoBehaviour
{
    public bool punchedEffect = false;

    public AudioClip punchSFX, blockedSFX, dodgeSFX;

    public GameObject punchEffect;

    private AudioSource audioSource;

    public Animator animator;

    public int punchHitForce = 5;

    public GameObject punchEffects;

    public float withinPunchingRange = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PunchNow() {
        punchedEffect = true;
    }
    public IEnumerator hitEffect(CharacterBase self, CharacterBase enemy, int damageMult, int hitForce) {
        Debug.Log("Test 1");
        if (Vector2.Distance(self.transform.position, enemy.transform.position) > withinPunchingRange || !ValidateDirection(self, enemy)) yield break;
        Debug.Log("Test 2");
        //punchedEffect = false;
        Character attacker = self.selfCharacter;
        Character defender = enemy.selfCharacter;
        int atkPawa = Random.Range(attacker.GetAtk() / 4, attacker.GetAtk() * 2);
        int defPawa = Random.Range(defender.GetDef() / 2, defender.GetDef());
        int damage = Mathf.Max(1, atkPawa - defPawa);
        bool enemyBlocking = enemy.selfCharacter.certainBlock > 0 || enemy.initiatedBlocking;
        AudioClip hitSFX = punchSFX;
        if (enemyBlocking)
        {
            hitSFX = blockedSFX;
            BlockEffect(enemy);
            damage /= 3;
            Debug.Log("Blocking");
            self.SetAnimation("block");
            if (enemy.selfCharacter.certainBlock > 0)
            {
                enemy.selfCharacter.certainBlock--;
                if (self.selfCharacter.certainBlock == 0) self.selfCharacter.blockMPReward = 2;
            }
        }
        //punchEffect.gameObject.transform.position = enemy.gameObject.transform.position;
        //punchEffect.transform.SetParent(enemy.gameObject.transform);
        audioSource.PlayOneShot(hitSFX);
        enemy.beingHit = true;
        enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Vector2 hitDirection = enemy.transform.position - self.transform.position;
        hitDirection.Normalize();
        Debug.Log("Hit Direction " + hitDirection);
        //enemy.SetAnimation("walk");
        enemy.GetComponent<Rigidbody2D>().AddForce(hitDirection * hitForce);
        punchEffects.SetActive(true);
        punchEffects.transform.position = enemy.transform.position;
        punchEffects.transform.SetParent(enemy.transform);
        yield return new WaitForSeconds(0.8f);
        punchEffects.SetActive(false);
        punchEffects.transform.position = Vector2.zero;
        //Debug.Log(damage);
        //audioSource.PlayOneShot(punchSFX);
        yield return new WaitForSeconds(0.1f);
        //enemy.animator.SetInteger("Block", 0);
        enemy.selfCharacter.SetHP(damage, self);
        //self.attacking = false;
        enemy.beingHit = false;
        enemy.BecomeIdle();
    }
    public IEnumerator InitiateAttack(CharacterBase self, CharacterBase enemyCharacter) //change to void
    {
        if (self.CantMove()) yield break;
        self.attacking = true;
        self.SetAnimation("punch");
        yield return new WaitForSeconds(0.7f); //change to invoke next method
        //ApplyAttack(self, enemyCharacter);
    }
    public void ApplyAttack(CharacterBase self, CharacterBase enemyCharacter) {
        {
            self.SetAnimation("idle");
            self.attacking = false;
            if (enemyCharacter.defeated || enemyCharacter.dodging) return;
            if (Vector2.Distance(self.transform.position, enemyCharacter.transform.position) <= withinPunchingRange)
            {
                bool dodged = Dodge(self, enemyCharacter);
                if (dodged) StartCoroutine(PerformDodge(enemyCharacter));
                else StartCoroutine(hitEffect(self, enemyCharacter, 1, punchHitForce));
            }
        }
}
public void SetAnimator(CharacterBase characterBase) { animator = characterBase.animator; }
    public void BlockEffect(CharacterBase characterBase)
    {
        characterBase.SetAnimation("block");
    }
    public bool Dodge(CharacterBase character1, CharacterBase character2)
    {
        int accuracy = Random.Range(character1.selfCharacter.GetAcc() / 2, character1.selfCharacter.GetAcc());
        int speed = Random.Range(character2.selfCharacter.GetSpd() / 2, character2.selfCharacter.GetSpd());
        return accuracy - speed < 0;
    }
    public IEnumerator PerformDodge(CharacterBase character)
    {
        character.dodging = true;
        character.BecomeIdle();
        //CharacterBase.GetComponent<Rigidbody2D>.
        character.SetAnimation("dodging");
        audioSource.PlayOneShot(dodgeSFX);

        //character.animator.Play("Dodge");
        yield return new WaitForSeconds(.9f);
        character.dodging = false;
        //character.SetAnimation("idle");
        character.BecomeIdle();
    }
    public void Defeated(CharacterBase character)
    {
        //if(character is Player) SceneManager.LoadScene()
    }
    public void EnemyDefeated(Enemy enemy, Player self)
    {
        enemy.defeated = true;
        self.AP += enemy.pointsToWinner;
        //animator.SetTrigger("Vanish");
        Invoke("FinishDefeated", 1f);
    }
    public void FinishDefeated(CharacterBase enemy, CharacterBase self)
    {
        //Time.timeScale = 0.1f;
        Debug.Log("Finish Defeated");
        Destroy(enemy.gameObject); // maybe make , 0.4f delay in destroying this GO
        if (enemy is Player) SceneManager.LoadScene("Lost Scene");
        else if (enemy is Enemy)
        {
            Player player = (Player)enemy;
            Enemy enemy2 = (Enemy)self;
            player.AP += enemy2.pointsToWinner;
            SceneManager.LoadScene("Won Scene"); //add method here to award points to player
        }
        else Debug.LogError("Wrong Script!");
    }
    public bool ValidateDirection(CharacterBase self, CharacterBase enemy)
    {
        Debug.Log("VALIDATE DIRECTION " + self.direction);
        if ((self.direction == 1 && enemy.transform.position.y > self.transform.position.y) ||
            (self.direction == 2 && enemy.transform.position.x > self.transform.position.x) ||
            (self.direction == 3 && enemy.transform.position.y < self.transform.position.y) ||
            (self.direction == 4 && enemy.transform.position.x < self.transform.position.x)) return true;
        else return false;
    }
}