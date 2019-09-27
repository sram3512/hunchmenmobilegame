using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeRender : MonoBehaviour
{
	public GameObject themelabels;
    // Start is called before the first frame update
    void Start()
    {
    	var theme1 = Instantiate(themelabels, new Vector3(-18.67f,3.5f,-3.0f),Quaternion.identity);
        var theme2 = Instantiate(themelabels, new Vector3(-12.94f,3.5f,-3.0f),Quaternion.identity);
        var theme3 = Instantiate(themelabels, new Vector3(-8.51f,3.5f,-3.0f),Quaternion.identity);
    	
    	theme1.GetComponent<TextMesh>().text = "Theme 1";
    	theme2.GetComponent<TextMesh>().text = "Theme 2";
    	theme3.GetComponent<TextMesh>().text = "Theme 3";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
