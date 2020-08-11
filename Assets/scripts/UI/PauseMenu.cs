using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    float lasTimeScale = 1;
    
    private void OnEnable()
    {
        lasTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = lasTimeScale;
    }


}
