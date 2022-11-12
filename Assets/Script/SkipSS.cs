using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipSS : MonoBehaviour
{
    [SerializeField]
    private string loadSS;

    // Start is called before the first frame update
    void Start()
    {
       if(Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene(loadSS);
        }
    }
}
