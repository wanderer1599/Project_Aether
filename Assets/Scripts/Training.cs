using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Training : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterBase self;

    public Player player;

    private int hitBag = 0;

    private Animator punchingBagAnimator, characterAnimator;

    private RegularAbilities myreg;

    public AudioClip machineSFX;

    public GameObject punchEffects;

    void Start()
    {
        punchingBagAnimator = GetComponent<Animator>();
        characterAnimator = self.GetComponent<Animator>();
        myreg = FindObjectOfType<RegularAbilities>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(RegularAttack());
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("scene 1 unity");
    }
    public IEnumerator RegularAttack()
    {
        if (Input.GetKeyDown(KeyCode.E) && Vector2.Distance(gameObject.transform.position, self.gameObject.transform.position) <= 1f)
        {
            self.SetAnimation("punch");
            self.attacking = true;
            yield return new WaitForSeconds(0.5f);
            //self.GetComponent<AudioSource>().PlayOneShot(machineSFX);
            punchEffects.SetActive(true);
            punchEffects.transform.position = transform.position;
            punchEffects.transform.SetParent(transform);
            //yield return new WaitForSeconds(.3f);
            self.GetComponent<AudioSource>().PlayOneShot(myreg.punchSFX);
            self.attacking = false;
            if (Mathf.Abs(self.transform.position.x - transform.position.x) >= 0.2f)
            {
                if (self.transform.position.x > transform.position.x) punchingBagAnimator.SetTrigger("Punched Left");
                else punchingBagAnimator.SetTrigger("Punched Right");
            }
            yield return new WaitForSeconds(0.3f);
            punchEffects.SetActive(false);
            hitBag++;
            if(hitBag == 20)
            {
                player.SP++;
                hitBag = 0;
            }
        }
    }
}