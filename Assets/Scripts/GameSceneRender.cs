using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;

public class GameSceneRender : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hud;
    public GameObject imageHolder;
    public UnityEngine.UI.InputField inputField;

    private SpriteRenderer image1;
    private SpriteRenderer image2;
    private SpriteRenderer image3;
    private SpriteRenderer image4;

     private class Level
    {
        public string Stage;
        public int level;
    }

    private StreamReader file;
    private StreamWriter output;
    private Level lvs;
    private float levelTimer;

    private TextMesh timeDisplay;
    private TextMesh numChars;
    private TextMesh outputMessage;
    private TextMesh infoMessage;

    void Start()
    {

    	 Debug.Log("Rendering 4 pics Enter the required word");
        file = new StreamReader("Assets/Resources/jsonData/user_info.json");
        string contents = file.ReadToEnd();
        lvs = JsonUtility.FromJson<Level>(contents);
        Debug.Log(lvs.level);
        

        file.Close();


        var levelIndicator = Instantiate(hud,new Vector3(0.88f,2.06f,-5.0f),Quaternion.identity);
        var textmeshLevel = levelIndicator.GetComponent<TextMesh>();

        timeDisplay = Instantiate(hud, new Vector3(-1.42f,2.06f,-5.0f),Quaternion.identity).GetComponent<TextMesh>();
        timeDisplay.GetComponent<Transform>().Rotate(new Vector3(0,180,0));

        numChars = Instantiate(hud, new Vector3(-3.42f,0.06f,-5.0f),Quaternion.identity).GetComponent<TextMesh>();
        numChars.GetComponent<Transform>().Rotate(new Vector3(0,180,0));

        numChars.text = "6";


        infoMessage = Instantiate(hud, new Vector3(2.82f,0.06f,-5.0f),Quaternion.identity).GetComponent<TextMesh>();
        infoMessage.GetComponent<Transform>().Rotate(new Vector3(0,180,0));



        levelIndicator.GetComponent<Transform>().Rotate(new Vector3(0,180,0));

        image1=Instantiate(imageHolder, new Vector3(-1.58f,0.51f,-5.0f),Quaternion.identity).GetComponent<SpriteRenderer>();
        image2=Instantiate(imageHolder, new Vector3(0.0f,0.51f,-5.0f),Quaternion.identity).GetComponent<SpriteRenderer>();
        image3=Instantiate(imageHolder, new Vector3(-1.58f,-0.83f,-5.0f),Quaternion.identity).GetComponent<SpriteRenderer>();
        image4=Instantiate(imageHolder, new Vector3(0.0f,-0.83f,-5.0f),Quaternion.identity).GetComponent<SpriteRenderer>();


        image1.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);
        image2.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);
        image3.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);
        image4.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);

        
        textmeshLevel.text = StaticClass.LevelSelection;
        textmeshLevel.color = Color.red;


        inputField.characterLimit = 6;
        inputField.onValueChanged.AddListener(characterReducer);

       

        levelTimer = 20;

    }
    void SubmitAnswerAction(string answer)
   {
   	Debug.Log(answer);
   	if (string.Compare(answer,"Sports")==0){
     	
   		Debug.Log("Correct");
      	infoMessage.text = "Correct";
      	infoMessage.color = Color.green;
      	timeDisplay.text="";
   		lvs.level +=1;
   		Debug.Log(lvs.level);
   		output = new StreamWriter("Assets/Resources/jsonData/user_info.json");
   		output.WriteLine(JsonUtility.ToJson(lvs));
   		output.Close();
   		StartCoroutine(ChangeScene());
      //UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectionScene");
   	}
   }

    void characterReducer(string ans){
    	Debug.Log(ans);
    	var remaining = 6-ans.Length;
    	numChars.text = remaining.ToString();
    	if (remaining==0){
    		SubmitAnswerAction(ans);
    	}
    }
   
    // Update is called once per frame
    void Update()
    {


    	timeDisplay.text = ":"+levelTimer.ToString();
    	timeDisplay.color = Color.black;
    	levelTimer -=Time.deltaTime;
    	if (levelTimer<0){
    		image1.sprite=null;
    		image2.sprite=null;
    		image3.sprite=null;
    		image4.sprite=null;
    		timeDisplay.text="";
    		
    		infoMessage.text = "GAME OVER!!!!";
    		
    		
    		infoMessage.color = Color.red;
    		StartCoroutine(ChangeScene());
    		//UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectionScene");

    	}
    	else{
    		image1.sprite = Resources.Load<Sprite>("soccer");
    		if(levelTimer<15){
    			image2.sprite = Resources.Load<Sprite>("baseball");
    		}
    		if(levelTimer<10){
    			image3.sprite = Resources.Load<Sprite>("football");
    		}
    		if(levelTimer<5){
    			image4.sprite = Resources.Load<Sprite>("basketball");
    		}
    	}
        
    }
     IEnumerator ChangeScene(){
     	yield return new WaitForSeconds(1);
     	UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectionScene");
     }

}
