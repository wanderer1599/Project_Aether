using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : MonoBehaviour
{
    public Rigidbody2D rigid;

    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
            rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.up * 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(1);
        }

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Terrain")
        {
            Destroy(gameObject);
        }
    }
}
