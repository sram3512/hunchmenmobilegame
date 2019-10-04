using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicBlankSelection : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseDown(){

        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("BLANK");
        gameObject.GetComponent<SpriteRenderer>().name = "Blank";
    	
    }
}
