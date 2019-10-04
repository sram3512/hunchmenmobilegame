using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSelection : MonoBehaviour
{
	public GameObject blank;
	private TextMesh chr; 
    // Start is called before the first frame update
    void Start()
    {
        chr = blank.GetComponent<TextMesh>();
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
        chr.text = gameObject.GetComponent<TextMesh>().text;
    }
}
