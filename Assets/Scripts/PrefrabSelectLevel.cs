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
        Debug.Log(GetComponent<TextMesh>().text);
        StaticClass.LevelSelection = GetComponent<TextMesh>().text;
    	UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
