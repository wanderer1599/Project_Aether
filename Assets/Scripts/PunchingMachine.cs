using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PunchingMachine : MonoBehaviour
{
    // Start is called before the first frame update

    public Player self;

    private int hitsBlocked = 0;

    private Animator punchingMachineAnimator, characterAnimator;

    private bool punching = false;

    public AudioClip punchSFX, blockedSFX, hitSFX, machineSFX;

    void Start()
    {
        punchingMachineAnimator = GetComponent<Animator>();
        characterAnimator = self.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Punch());
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("scene 1 unity");
    }
    public IEnumerator Punch()
    {
        if (Vector2.Distance(gameObject.transform.position, self.gameObject.transform.position) <= 1.2f)
        {
            //self.SetAnimation("punch");

            //Debug.Log(self.initiatedBlocking);
            if (punching) yield break;
            punching = true;
            self.GetComponent<AudioSource>().PlayOneShot(machineSFX);
            //punchingMachineAnimator.SetBool("punch", true);
            yield return new WaitForSeconds(.4f);
            punchingMachineAnimator.SetTrigger("punch2");
            self.beingHit = true;
            yield return new WaitForSeconds(.4f);
            punchingMachineAnimator.SetBool("punch", false);
            hitSFX = punchSFX;
            if (self.initiatedBlocking)
            {
                self.blocking = true;
                self.SetAnimation("block");
                hitSFX = blockedSFX;
                hitsBlocked++;
                Debug.Log("Blocked");
                if (hitsBlocked == 20)
                {
                    self.SP++;
                    hitsBlocked = 0;
                }
            }
            Vector2 hitDistance = self.gameObject.transform.position - gameObject.transform.position;
            hitDistance.Normalize();
            self.GetComponent<Rigidbody2D>().AddForce(hitDistance * 70);
            self.GetComponent<AudioSource>().PlayOneShot(hitSFX);
            yield return new WaitForSeconds(0.5f);
            self.blocking = false;
            self.beingHit = false;
            self.SetAnimation("idle");
            yield return new WaitForSeconds(1f);
            punching = false;
        }
        else punchingMachineAnimator.SetBool("punch", false);
    }
}
