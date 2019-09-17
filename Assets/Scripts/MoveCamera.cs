using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
	private float movementSpeed = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.GetKeyDown(KeyCode.RightArrow) && gameObject.transform.position.x<0.0){
    		transform.Translate(Vector3.right * Time.deltaTime * movementSpeed);
    	}
    	if(Input.GetKeyDown(KeyCode.LeftArrow) && gameObject.transform.position.x > -15.0){
    		Debug.Log(gameObject.transform.position.x);
    		transform.Translate(Vector3.left * Time.deltaTime * movementSpeed);
    	}
        
    }
}
