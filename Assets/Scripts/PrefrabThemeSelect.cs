﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefrabThemeSelect : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter(){
    	GetComponent<Renderer>().material.color = Color.red;

    }
    void OnMouseExit(){
    	GetComponent<Renderer>().material.color = Color.yellow;
    }
    void OnMouseDown(){
        StaticClass.ThemeSelection = GetComponent<TextMesh>().text.ToLower().Replace(" ","");
    	UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectionScene");
    }
}
