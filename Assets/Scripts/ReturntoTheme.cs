using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturntoTheme : MonoBehaviour
{
    // Start is called before the first frame update
   void OnMouseDown(){
   		if (StaticClass.backposition==0){
   			UnityEngine.SceneManagement.SceneManager.LoadScene("LoadScene");
   		}
   		else{
   			StaticClass.backposition=0;
   			UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectionScene");
   		}
    	
    }
}
