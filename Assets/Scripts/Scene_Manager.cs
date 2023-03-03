using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadScene(string whichScene) { Debug.Log("Test"); SceneManager.LoadScene(whichScene); }
    public void Hey() { Debug.Log("Test 2"); }
}
