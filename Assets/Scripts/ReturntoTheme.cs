using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturntoTheme : MonoBehaviour
{
    // Start is called before the first frame update
   void OnMouseDown(){
    	UnityEngine.SceneManagement.SceneManager.LoadScene("LoadScene");
    }
}
