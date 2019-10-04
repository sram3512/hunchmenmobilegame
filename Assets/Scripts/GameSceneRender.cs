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
    public GameObject picCharacter;
    public GameObject blankCharacter;
    

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

    private SpriteRenderer[] blankSprites;
    private char[] alphabet = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O',
                                'P','Q','R','S','T','U','V','W','X','Y','Z'};

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

        infoMessage = Instantiate(hud, new Vector3(-2.82f,0.06f,-5.0f),Quaternion.identity).GetComponent<TextMesh>();
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


       

        levelTimer = 20;

        keyBoardCreation();
        answerCreation();


    }
    char[] shuffle(char[] keyboard){
        for (int k=0;k<keyboard.Length;k++){
                char tmp = keyboard[k];
                int pos = Random.Range(0,keyboard.Length-1);
                keyboard[k]=keyboard[pos];
                keyboard[pos]=tmp;
        }
        return keyboard;
    }

    char[] genCharSet(string answer){
        char[] charAnswer = answer.ToUpper().ToCharArray();
        if (answer.Length==12){
            return charAnswer;
        }
        else{
            char[] alphaset = new char[12-charAnswer.Length];
            for(int j =0;j<12-charAnswer.Length;j++){
                alphaset[j] = alphabet[Random.Range(0,alphabet.Length-1)];
            }
            char[] keyboard = new char[12];
            charAnswer.CopyTo(keyboard,0);
            alphaset.CopyTo(keyboard,charAnswer.Length);
            return shuffle(keyboard);
        }
    }
    void keyBoardCreation(){
        //string [] KeyBoard = new string[] {"A","B","C","D","E","F","G","H","I","J"};
        char[] KeyBoard = genCharSet("ball");

        Debug.Log(KeyBoard);
        float x=3.25f;
        float y=0.0f;
        for (int i=0;i<12;i++){
            Debug.Log(KeyBoard[i]);
            var temp = Instantiate(picCharacter, new Vector3(x,y,-5.0f), Quaternion.identity);
            temp.name=KeyBoard[i].ToString();
            temp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(KeyBoard[i].ToString());
            temp.GetComponent<Transform>().Rotate(new Vector3(0,180,0));
            if (i==2 || i==5 || i==8 || i==12){
                x=3.25f;
                y-=0.5f;
            }
            else{
                x-=0.5f;
            }
        }
    }

    void answerCreation(){

        float bx=0.45f;
        float by=-1.92f;
        blankSprites = new SpriteRenderer[4];
      
        for(int i=0;i<4;i++){
            var blk = Instantiate(blankCharacter, new Vector3(bx,by,-5.0f), Quaternion.identity);
            blk.name = "Blank";
            blankSprites[i] = blk.GetComponent<SpriteRenderer>();
            blankSprites[i].GetComponent<Transform>().Rotate(new Vector3(0,180,0));
            bx-=0.5f;
        }
    }
    void SubmitAnswerAction(string answer)
   {
   	Debug.Log(answer);
   	if (string.Compare(answer,"BALL")==0){
     	
   		Debug.Log("Correct");
      	infoMessage.text = "Correct";
      	infoMessage.color = Color.green;
      	timeDisplay.text="";
   		//lvs.level +=1;
   		//Debug.Log(lvs.level);
   		//output = new StreamWriter("Assets/Resources/jsonData/user_info.json");
   		//output.WriteLine(JsonUtility.ToJson(lvs));
   		//output.Close();
   		StartCoroutine(ChangeScene());
      //UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectionScene");
   	}
   }

   int findFirstBlank(){
        for(int i=0;i<blankSprites.Length;i++){
            if (blankSprites[i].name=="Blank"){
                return i;
            }
        }
        return -1;
    }
   
   string readAnswer(){
        string ans="";
        for(int i=0;i<blankSprites.Length;i++){
            ans+=blankSprites[i].name;
        }
        return ans;
   }
   
    // Update is called once per frame
    void Update()
    {

        if(findFirstBlank()==-1){
            SubmitAnswerAction(readAnswer());
        }
        if(StaticClass.KeyBoardInput!=null){
            int pos = findFirstBlank();
            if (pos!=-1){
                blankSprites[pos].name=StaticClass.KeyBoardInput;
                blankSprites[pos].sprite = Resources.Load<Sprite>(StaticClass.KeyBoardInput);
            }
            StaticClass.KeyBoardInput=null;
        }

    	timeDisplay.text = ":"+levelTimer.ToString();
    	timeDisplay.color = Color.black;
    	levelTimer -=Time.deltaTime;
    	if (levelTimer<0){
    		image1.sprite=null;
    		image2.sprite=null;
    		image3.sprite=null;
    		image4.sprite=null;
    		timeDisplay.text="";
    		
    		infoMessage.text = "GAME\nOVER!!!!";
    		
    		
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
