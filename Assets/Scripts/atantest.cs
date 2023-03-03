using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atantest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Mathf.Atan2(1, 2));
        Debug.Log(Mathf.Atan2(-1, 2));
        Debug.Log(Mathf.Atan2(-1, -2));
        Debug.Log(Mathf.Atan2(1, -2));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
