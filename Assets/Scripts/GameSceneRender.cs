using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using UnityEngine.Networking;




public class GameSceneRender : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject hud;
    public GameObject imageHolder;
    public GameObject picCharacter;
    public GameObject blankCharacter;

    public GameObject currencyDisplay;

    

    private SpriteRenderer image1;
    private SpriteRenderer image2;
    private SpriteRenderer image3;
    private SpriteRenderer image4;


    private StreamReader file;
    private StreamWriter output;
    private Level lvs;
    private float levelTimer;

    private TextMesh timeDisplay;
    private TextMesh numChars;
    private TextMesh outputMessage;
    private TextMesh infoMessage;
    private TextMesh coinVal;
    private TextMesh coinsText;

    private SpriteRenderer[] blankSprites;
    private char[] alphabet = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O',
                                'P','Q','R','S','T','U','V','W','X','Y','Z'};


    [System.Serializable]
    private class Theme{
        public List<Level> levels;
        public string imageServer;     
        
    }

    [System.Serializable]
    private class Level{
        public string name;
        public int difficulty;
        public List<QAPoll> pool;
    }
    
    [System.Serializable]
    private class QAPoll
    {
        public string answer;
        public List<string> imageList;
    }


    [System.Serializable]
    private class User{
        public string name;
        public string email_id;
        public int currency;
        public List<Themeprogress> progress;
        
    }

    [System.Serializable]
    private class Themeprogress{
        public string theme;
        public int unlocked;
        public List<Questionpool> previousQuestion;
    }

    [System.Serializable]
    private class Questionpool{
        public int level;
        public int question;
    }


    private Theme themeObject;
    private string Answer;
    private List<string> Questions;
    private string imageserverURL;

    private Sprite[] questionSprites;
    private string userPath;
    private bool currencyTrigger;
    private int currentLevel;
    private Themeprogress themeInstance;
    public static int coinsTotal;


    void Start()
    {

        userPath = "Assets/Resources/jsonData/user_info.json";
        //coinsTotal = 0;
        currentLevel = int.Parse(StaticClass.LevelSelection);
        //coinVal = coins.GetComponent<TextMesh>();

        var coinsAmount = Instantiate(currencyDisplay, new Vector3(1.9f, 2.06f, -5f), Quaternion.identity);
        coinsAmount.GetComponent<Transform>().Rotate(new Vector3(0, 180, 0));
        coinsAmount.GetComponent<SpriteRenderer>().transform.position = new Vector3(3.63f, -0.44f, -4.86f);
        //coinsAmount.GetComponent<TextMesh>().transform.position = new Vector3(1.9f, 2.06f, -5f);


        var coinsText1 = GameObject.FindWithTag("currencyValue").GetComponent<TextMesh>();
        //coinVal = Instantiate(coins, new Vector3(0.55f, 2.06f, -5.0f), Quaternion.identity).GetComponent<TextMesh>();

        User currencyObj = readUserinfo();
        Debug.Log("CoinVal:" + currencyObj.currency.ToString());
        //coinVal.text = currencyObj.currency.ToString();
        coinsText1.text = currencyObj.currency.ToString();

        var levelIndicator = Instantiate(hud,new Vector3(0.88f,2.06f,-5.0f),Quaternion.identity);
        var textmeshLevel = levelIndicator.GetComponent<TextMesh>();

        timeDisplay = Instantiate(hud, new Vector3(-1.42f,2.06f,-5.0f),Quaternion.identity).GetComponent<TextMesh>();
        timeDisplay.GetComponent<Transform>().Rotate(new Vector3(0,180,0));

        infoMessage = Instantiate(hud, new Vector3(-2.82f,0.06f,-5.0f),Quaternion.identity).GetComponent<TextMesh>();
        infoMessage.GetComponent<Transform>().Rotate(new Vector3(0,180,0));



        levelIndicator.GetComponent<Transform>().Rotate(new Vector3(0,180,0));

        image1=Instantiate(imageHolder, new Vector3(-2.58f,0.15f,-5.0f),Quaternion.identity).GetComponent<SpriteRenderer>();
        image2=Instantiate(imageHolder, new Vector3(-0.9f,0.15f,-5.0f),Quaternion.identity).GetComponent<SpriteRenderer>();
        image3=Instantiate(imageHolder, new Vector3(-2.58f,-1.35f,-5.0f),Quaternion.identity).GetComponent<SpriteRenderer>();
        image4=Instantiate(imageHolder, new Vector3(-0.9f,-1.35f,-5.0f),Quaternion.identity).GetComponent<SpriteRenderer>();


        image1.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);
        image2.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);
        image3.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);
        image4.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);

        
        textmeshLevel.text = "Level "+StaticClass.LevelSelection;
        textmeshLevel.color = Color.red;

        levelTimer = 20;
        currencyTrigger = false;

        Debug.Log(StaticClass.LevelSelection);
        Debug.Log(StaticClass.ThemeSelection);

        themeInstance = readTheme(readUserinfo());

        readJson("Assets/Resources/jsonData/"+StaticClass.ThemeSelection+".json");

        keyBoardCreation();
        answerCreation();

       StartCoroutine(fetchImages());


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
    int[] shuffleInt(int[] data){
        for (int k=0;k<data.Length;k++){
                int tmp = data[k];
                int pos = Random.Range(0,data.Length-1);
                data[k]=data[pos];
                data[pos]=tmp;
        }
        return data;
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
        char[] KeyBoard = genCharSet(Answer);
        float x=2.75f;
        float y=0.0f;
        for (int i=0;i<12;i++){
            
            var temp = Instantiate(picCharacter, new Vector3(x,y,-5.0f), Quaternion.identity);
            temp.name=KeyBoard[i].ToString();
            temp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(KeyBoard[i].ToString());
            temp.GetComponent<Transform>().Rotate(new Vector3(0,180,0));
            if (i==2 || i==5 || i==8 || i==12){
                x=2.75f;
                y-=0.5f;
            }
            else{
                x-=0.5f;
            }
        }
    }
    void answerCreation(){

        float bx=0.0f;
        float by=-1.92f;
        blankSprites = new SpriteRenderer[Answer.Length];
      
        for(int i=0;i<Answer.Length;i++){
            var blk = Instantiate(blankCharacter, new Vector3(bx,by,-5.0f), Quaternion.identity);
            blk.name = "Blank";
            blankSprites[i] = blk.GetComponent<SpriteRenderer>();
            blankSprites[i].GetComponent<Transform>().Rotate(new Vector3(0,180,0));
            bx-=0.5f;
        }
    }
    void SubmitAnswerAction(string answer)
   {
   	if (string.Compare(answer,Answer.ToUpper())==0){
     	
   		
      	infoMessage.text = "Correct";
      	infoMessage.color = Color.green;
      	timeDisplay.text="";
        if (currencyTrigger==false){
            computeReward();
        }
        
        StartCoroutine(ChangeScene());
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
   Themeprogress readTheme(User userObj){

    Themeprogress Theme = null;
    for(int i=0;i<userObj.progress.Count;i++){
        if(userObj.progress[i].theme==StaticClass.ThemeSelection){
            Debug.Log("Fount at:"+i.ToString());
            Theme=(Themeprogress)userObj.progress[i];
        }
    }
    return Theme;
   }
   User writeTheme(User userObj, Themeprogress instance){
     
    for(int i=0;i<userObj.progress.Count;i++){
        if(userObj.progress[i].theme==instance.theme){
           userObj.progress[i]=instance;
        }
    }
    return userObj;
   }
   void computeReward(){

        currencyTrigger = true;
        int maxCurrency = 20;
        if(levelTimer<15){
            maxCurrency=15;
        }
        if(levelTimer<10){
            maxCurrency=10;
        }
        if(levelTimer<5){
            maxCurrency=5;
        }
        User currencyObj = readUserinfo();
        currencyObj.currency += maxCurrency;
        //Unlock next level
        
        Themeprogress thm = readTheme(currencyObj);
        if(thm.unlocked==currentLevel && thm.unlocked+1<StaticClass.MaxLevels){
            Debug.Log("valid");
            thm.unlocked += 1;
            
        }
        writeUserinfo(writeTheme(currencyObj,thm));
   }
   
   void readJson(string filePath){
        StreamReader file = new StreamReader(filePath);
        string line;
        string contents="";
        while((line = file.ReadLine())!=null){
            contents+=line.Replace("\t","");
        }
        
        themeObject= JsonUtility.FromJson<Theme>(contents);
      
        imageserverURL = themeObject.imageServer;
        
        int gameLevel = int.Parse(StaticClass.LevelSelection)-1;

        try{

            int poolSelection = randomQuestionWithTracking(themeObject.levels[gameLevel].pool.Count); 
        
            Answer = themeObject.levels[gameLevel].pool[poolSelection].answer;
            Questions = themeObject.levels[gameLevel].pool[poolSelection].imageList;
        }
        catch (System.ArgumentOutOfRangeException e){
             infoMessage.text = "Missing\nQuestions.";
             infoMessage.color = Color.red;
             StartCoroutine(ChangeScene());
        }
       
           
    }
    int randomQuestionWithTracking(int poolSize){


        Debug.Log(poolSize);     

            int pos = -1;
            bool found = false;
            Debug.Log("Pos:"+pos.ToString());

            List<int> tmp = new List<int>();
            for(int i=0;i<poolSize;i++){
                tmp.Add(i);
            }
        
            for(int i=0;i<themeInstance.previousQuestion.Count;i++){
                if(themeInstance.previousQuestion[i].level==currentLevel){
                    found = true;
                    int prevQ = themeInstance.previousQuestion[i].question;
                        
                    tmp.RemoveAt(prevQ);
                    pos = shuffleInt(tmp.ToArray())[0];

                    themeInstance.previousQuestion[i].question=pos;
                    }
                   
            }
            if(!found){
                pos = shuffleInt(tmp.ToArray())[0];
                Questionpool qp = new Questionpool();
                qp.level=currentLevel;
                qp.question=pos;

                themeInstance.previousQuestion.Add(qp);
            }
            

        writeUserinfo(writeTheme(readUserinfo(),themeInstance));
        return pos;
    }

    void writeUserinfo(User userobj){
        StreamWriter writer = new StreamWriter(userPath);
        writer.WriteLine(JsonUtility.ToJson(userobj));
        writer.Close();
    }
    User readUserinfo(){
        StreamReader file = new StreamReader(userPath);
        string line;
        string usercontents="";
        while((line = file.ReadLine())!=null){
            usercontents+=line.Replace("\t","");
        }
        file.Close();

        return JsonUtility.FromJson<User>(usercontents);

    }
    IEnumerator fetchImages(){
        Debug.Log("Fetching images");

        questionSprites = new Sprite[Questions.Count];
        for(int i=0;i<Questions.Count;i++){
             
            WWW spriteURL  = new WWW(imageserverURL+"imageName="+Questions[i]);
            yield return spriteURL;

            try{
                questionSprites[i] = Sprite.Create(spriteURL.texture,new Rect(0, 0, spriteURL.texture.width, spriteURL.texture.height), new Vector2(0, 0));
            }
            catch(System.NullReferenceException e){
                infoMessage.text = "Is the\nserver up?";
                infoMessage.color = Color.red;
                StartCoroutine(ChangeScene());
            }
        }

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
    		

    	}
    	else{

        	   image1.sprite = questionSprites[0]; 
        	   if(levelTimer<15){
        	       image2.sprite = questionSprites[1]; 
        		}
        		if(levelTimer<10){
        		   image3.sprite = questionSprites[2]; 
        		}
        		if(levelTimer<5){
        			image4.sprite = questionSprites[3]; 
        		}
                
                //Resources.Load<Sprite>("<file_name>");
    	}
        //Update the coinValue
        var coinsText1 = GameObject.FindWithTag("currencyValue").GetComponent<TextMesh>();
        User currencyObj = readUserinfo();
        Debug.Log("CoinVal:" + currencyObj.currency.ToString());
        //coinVal.text = currencyObj.currency.ToString();
        coinsText1.text = currencyObj.currency.ToString();


    }
     IEnumerator ChangeScene(){
     	yield return new WaitForSeconds(1);
     	UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectionScene");
     }

}
