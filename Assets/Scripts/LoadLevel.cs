using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string levelLoad;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevelFun(levelLoad));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator LoadLevelFun(string level)
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(level);
    }
}
