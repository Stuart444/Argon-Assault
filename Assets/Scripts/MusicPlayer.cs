using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); // Could be done in Start but following Unity Doc example
    }

    // Use this for initialization
    void Start()
    {
        Invoke("LoadFirstLevel", 2f);
	}

    void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}
