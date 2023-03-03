using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHandler : MonoBehaviour
{
    private int number = 0;
    private Text myText;
    // Start is called before the first frame update
    void Start()
    {
        myText = GameObject.FindObjectOfType<Text>();

        myText.text = number.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey) { Add(ref number, ref myText);  Debug.Log(number); }
    }
    public void Add(ref int num, ref Text txt) { num++; txt.text = num.ToString(); }
}
