using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_Machine : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject electricity;

    public GameObject Character;

    public Animator self;

    public int blockedMachine = 0;

    private GameObject electricityExists;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ShootElectricity());
    }

    public IEnumerator ShootElectricity()
    {
        if(!electricityExists)
        {
            electricityExists = Instantiate(electricity);
            self.SetTrigger("Shoot");
            yield return new WaitForSeconds(0.7f);
            electricityExists.SetActive(true);
            electricityExists.transform.position += Vector3.left;
        }
    }
}
