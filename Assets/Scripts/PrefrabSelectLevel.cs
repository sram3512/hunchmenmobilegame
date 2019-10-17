using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PrefrabSelectLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter(){
    	GetComponent<Renderer>().material.color = Color.red;

    }
    void OnMouseExit(){
    	GetComponent<Renderer>().material.color = Color.black;
    }
    void OnMouseDown(){
        StaticClass.LevelSelection = GetComponent<TextMesh>().text.Split()[1];
    	UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
