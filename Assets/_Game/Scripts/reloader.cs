﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class reloader : MonoBehaviour
{
    
    private void Start() {
        SceneManager.LoadScene("Game");
    }

}
