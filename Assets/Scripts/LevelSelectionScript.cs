using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectionScript : MonoBehaviour
{

	public GameObject levelSign;
    // Start is called before the first frame update
    void Start()
    {
    	float x = -14.93f;
    	float y = 7.15f;
    	float z = -5.23f;
        for (int i=0;i<9;i++){
            var lvs = Instantiate(levelSign, new Vector3(x,y,z),Quaternion.identity);
            lvs.GetComponent<TextMesh>().text = "Level "+(i+1).ToString();
            x+=2.0f;
            if(i ==2 || i==5 || i==8){
                y-=1.0f;
                x=-14.93f;
            }
           
        }
        //var lvs = Instantiate(levelSign, new Vector3(x,y,z),Quaternion.identity);
        //lvs.GetComponent<TextMesh>().text = "Level 12";
        
    }

}
