using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CharacterBase
{
    public int movementSpeed = 100;

    private int guardBreak;//load these too
    public int AP, SP, maxExp, origGuardBreak, lvl, exp;

    public SavingAndLoading savingAndLoading;

    // Start is called before the first frame update
    void Start()
    {
        savingAndLoading.LoadGame(this, this);
        //SetStats();
        animator = GetComponent<Animator>();
        Debug.Log(animator);
        myRegularAbilities = FindObjectOfType<RegularAbilities>();
        selfCharacter.SetSelf(this);
        LoadAbilities();
        //abilities.Add(new GeneralBoost());
    }

    // Update is called once per frame
    void Update()
    {
        
        if(enemyCharacter && Input.GetKeyDown(KeyCode.E)) StartCoroutine(myRegularAbilities.InitiateAttack(this, enemyCharacter)); //update this attack thing
        //applyHitEffectNow = GetComponent<RegularAbilities>().punchedEffect;
        Block();
        if (Input.GetKeyDown(KeyCode.Q) && !CantMove()) StartCoroutine(abilities[0].Use(this, enemyCharacter));
        InitiateMovement();
    }

    public void PlayerApplyAttack() { myRegularAbilities.ApplyAttack(this, enemyCharacter); }
    
    public void InitiateMovement() //remove below the inputs and add to CharacterBase
    {
        Debug.Log("being hit " + beingHit);
        Debug.Log("Can't Move " + CantMove());
        //if (!CantMove()) Debug.Log("Can't Move " + !CantMove()); //Debug.Log("CAN'T MOVE"); //return;
        if (CantMove()) return;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(x, y);
        gameObject.GetComponent<Rigidbody2D>().velocity = movement * movementSpeed;
        //Debug.Log("Movement " + movement + " velocity " + gameObject.GetComponent<Rigidbody2D>().velocity);
        int direction;
        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x <= 0) direction = 4;
            else direction = 2;
        }
        else
        {
            if (y <= 0) direction = 3;
            else direction = 1;
       }
        Debug.Log(animator);
        Debug.Log("direction " + direction);

        if (movement != Vector2.zero && direction != 0) { SetAnimation("walk"); this.direction = direction; }
        if (movement == Vector2.zero && !CantMove())
        {
            SetAnimation("idle");
        }
    }
    public void Block()
    {
        if(Input.GetKeyDown(KeyCode.F))
        StartCoroutine(InitiateBlock(this));
    }
    public IEnumerator InitiateBlock(CharacterBase characterBase)
    {
        initiatedBlocking = true;
        yield return new WaitForSeconds(0.5f);
        initiatedBlocking = false;
    }
    private void LoadAbilities()
    {
        if (PlayerPrefs.GetString("Energy Ball").Equals("true") && !abilities.Contains(EnergyBall)) abilities.Add(EnergyBall);
        if (PlayerPrefs.GetString("General Boost").Equals("true") && !abilities.Contains(generalBoost)) abilities.Add(generalBoost);
        if (PlayerPrefs.GetString("Super Block").Equals("true") && !abilities.Contains(SuperBlock)) abilities.Add(SuperBlock);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Taking " + damage + " damage");
    }
}