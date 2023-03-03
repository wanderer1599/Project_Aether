using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Learn_Skills : MonoBehaviour
{
    // Start is called before the first frame update

    public Player player;

    public Text pointsText;

    //public int points;

    void Start()
    {
        pointsText.text = "Ability Points: " + player.SP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LearnSkill(string whichSkill)
    {
        if (player.SP < 5) return;
        if (whichSkill.Equals("Energy Ball")) PlayerPrefs.SetString("Energy Ball", "true");
        if (whichSkill.Equals("General Boost")) PlayerPrefs.SetString("General Boost", "true");
        if (whichSkill.Equals("Super Block")) PlayerPrefs.SetString("Super Block", "true");
        player.SP -= 5;
        pointsText.text = "Points: " + player.SP.ToString();
    }
}