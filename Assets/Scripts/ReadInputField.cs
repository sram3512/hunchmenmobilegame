using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class ReadInputField : MonoBehaviour
{
	private class Level
	{
    	public string Stage;
    	public int level;
	}
	private StreamReader file;
	private StreamWriter output;
	private Level lvs;
  public TextMesh userResponse;
  

    // Start is called before the first frame update
    void Start()
    {
        file = new StreamReader("Assets/Resources/jsonData/user_info.json");
        string contents = file.ReadToEnd();
        lvs = JsonUtility.FromJson<Level>(contents);
        Debug.Log(lvs.Stage);
        file.Close();
    	var input = gameObject.GetComponent<InputField>();
    	input.onEndEdit.AddListener(SubmitAnswerAction);
    	

    }

   void SubmitAnswerAction(string answer)
   {
   	Debug.Log(answer);
   	if (string.Compare(answer,"Sports")==0){
     
   		Debug.Log("Correct");
      userResponse.text = "Correct";
      userResponse.color = Color.green;
   		lvs.level +=1;
   		Debug.Log(lvs.level);
   		output = new StreamWriter("Assets/Resources/jsonData/user_info.json");
   		output.WriteLine(JsonUtility.ToJson(lvs));
   		output.Close();
      UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
   	}
   }
}
