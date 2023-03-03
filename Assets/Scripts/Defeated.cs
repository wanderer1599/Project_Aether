using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeated : MonoBehaviour
{
    // Start is called before the first frame update

    public CharacterBase character, enemy;
    private void Awake()
    {
        character = FindObjectOfType<Player>();
        enemy = FindObjectOfType<Enemy>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DoWhatDefeated(CharacterBase character1)
    {
        //if(character1 == character)
        //else 
    }

}
