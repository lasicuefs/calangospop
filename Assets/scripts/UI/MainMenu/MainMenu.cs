﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Use this for initialization
    void Start() {

    }

    public void StartTutorial1()
    {
        SceneManager.LoadScene("Tutorial1", LoadSceneMode.Single);
    }

    public void StartTutorial2()
    {
        SceneManager.LoadScene("Tutorial2", LoadSceneMode.Single);
    }
}
