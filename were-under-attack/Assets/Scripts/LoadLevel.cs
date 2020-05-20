using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("StartGame", 5f);
        //DontDestroyOnLoad(this);    
    }

    // Update is called once per frame
    void Update()
    {
    }

    void StartGame() {
        SceneManager.LoadScene("Scenes/SampleScene");
    }
}
