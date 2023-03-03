using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Training_Electricity : MonoBehaviour
{
    public GameObject player;

    public int movementSpeed = 5;

    public Energy_Machine energyMachine;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FollowPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator FollowPlayer()
    {
        while(gameObject)
        {
            Vector2 direction = player.transform.position - transform.position;
            GetComponent<Rigidbody2D>().velocity = direction.normalized * movementSpeed;
            yield return new WaitForSeconds(0.5f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Player>())
        {
            if(collision.gameObject.GetComponent<Player>().initiatedBlocking)
            {
                energyMachine.blockedMachine++;
                if (energyMachine.blockedMachine == 12)
                {
                    collision.gameObject.GetComponent<Player>().AP++;
                    energyMachine.blockedMachine = 0;
                }
            }
            Destroy(gameObject);
        }
    }
}