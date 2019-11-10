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
    public GameObject freezeTime;
    public GameObject currencyDisplay;
    private GameObject freezeTimeObject;
    public GameObject removeKey;
    public GameObject tokenHint;
    public GameObject dTimer;



    private SpriteRenderer image1;
    private SpriteRenderer image2;
    private SpriteRenderer image3;
    private SpriteRenderer image4;


    private StreamReader file;
    private StreamWriter output;
    private Level lvs;
    private float levelTimer;
    private float fixedTimer;

    private TextMesh timeDisplay;
    private TextMesh numChars;
    private TextMesh outputMessage;
    private TextMesh infoMessage;
    private TextMesh coinVal;
    private TextMesh coinsText;

    private SpriteRenderer[] blankSprites;
    private List<GameObject> dynamicTimer;
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
        public string description;
        public List<QAPoll> questions;
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
        public int passed;
        public List<int> question;
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
    private List<Object> ansPos;

    private bool remove;
    private Dictionary<string,int> levelMap;

    private float xs;
    private float ys;
    private float zs;
    private bool revt;
    private float diff;
    private Dictionary<SpriteRenderer,bool> expandTrack;

    void Start()
    {

        //Dynamic Timer 
        dynamicTimer = new List<GameObject>();


        //Image Expansion parameters
        expandTrack = new Dictionary<SpriteRenderer,bool>();
        xs=0.5f;
        ys=0.5f;
        zs=0.5f;
        revt=false;
        diff=0.001f;

        levelMap = new Dictionary<string,int>();
        levelMap.Add("EASY",0);
        levelMap.Add("MEDIUM",1);
        levelMap.Add("HARD",2);

    	ansPos = new List<Object>();
        userPath = Application.persistentDataPath+"/user_info.json";
        
  
        currentLevel = levelMap[StaticClass.LevelSelection];
      

        var coinsAmount = Instantiate(currencyDisplay, new Vector3(-3.50f, 1.77f, -5.0f), Quaternion.identity);
        coinsAmount.GetComponent<Transform>().Rotate(new Vector3(0, 180, 0));
   

        var coinsText1 = GameObject.FindWithTag("currencyValue").GetComponent<TextMesh>();
        //coinVal = Instantiate(coins, new Vector3(0.55f, 2.06f, -5.0f), Quaternion.identity).GetComponent<TextMesh>();

        Instantiate(freezeTime, new Vector3(2.2f, 0.91f, -5.0f), Quaternion.identity);
        freezeTimeObject = freezeTime.gameObject;

        var rmObj = Instantiate(removeKey, new Vector3(2.20f,-0.11f,-5.0f),Quaternion.identity);

        rmObj.GetComponent<Transform>().Rotate(new Vector3(0,180,0));
        rmObj.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);

        var tokObj = Instantiate(tokenHint, new Vector3(2.20f,-1.14f,-5.0f),Quaternion.identity);
        tokObj.GetComponent<Transform>().Rotate(new Vector3(0,180,0));
        tokObj.GetComponent<Transform>().localScale = new Vector3(0.5f,0.5f,0.5f);


        User currencyObj = readUserinfo();
        
        //coinVal.text = currencyObj.currency.ToString();
        coinsText1.text = currencyObj.currency.ToString();

        var levelIndicator = Instantiate(hud,new Vector3(0.7f,2.06f,-5.0f),Quaternion.identity);
        var textmeshLevel = levelIndicator.GetComponent<TextMesh>();

        timeDisplay = Instantiate(hud, new Vector3(-1.34f,2.06f,-5.0f),Quaternion.identity).GetComponent<TextMesh>();
        timeDisplay.GetComponent<Transform>().Rotate(new Vector3(0,180,0));

        infoMessage = Instantiate(hud, new Vector3(-0.82f,0.3f,-5.0f),Quaternion.identity).GetComponent<TextMesh>();
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

        
        textmeshLevel.text = StaticClass.LevelSelection;
        textmeshLevel.color = Color.red;

        levelTimer = 25;
        fixedTimer = 25;
        currencyTrigger = false;

        remove = false;
        Debug.Log(StaticClass.LevelSelection);
        Debug.Log(StaticClass.ThemeSelection);

        themeInstance = readTheme(readUserinfo());

        readJson("jsonData/"+StaticClass.ThemeSelection);
        

        keyBoardCreation();
        answerCreation();

       StartCoroutine(fetchImages());

        createTimer();


    }
    void createTimer(){
        //dynamicTimer = new GameObject[40];
        var dx=-1.6f;
        var dy=-1.65f;
        var dz=-5.0f;
        for(int i=0;i<40;i++){
            var tmpDyno = Instantiate(dTimer, new Vector3(dx,dy,dz),Quaternion.identity);
            tmpDyno.GetComponent<SpriteRenderer>().color=Color.green;
            dynamicTimer.Add(tmpDyno);
            if(i<4){
                dx-=0.3f;
                tmpDyno.GetComponent<SpriteRenderer>().color=Color.green;
            }
            if(i==4){
                dy+=0.4f;
                tmpDyno.GetComponent<SpriteRenderer>().color=Color.green;
            }
            if(i>4 && i<14){
                tmpDyno.GetComponent<Transform>().Rotate(new Vector3(0,0,90));
                tmpDyno.GetComponent<SpriteRenderer>().color=Color.green;
                dy+=0.3f;
            }
            if(i>=14 && i<27){
                dx+=0.3f;
                tmpDyno.GetComponent<SpriteRenderer>().color=Color.yellow;
            }
            if(i==27){
                dy-=0.32f;
                tmpDyno.GetComponent<SpriteRenderer>().color=Color.yellow;
            }
            if(i>27 && i<37){
                tmpDyno.GetComponent<Transform>().Rotate(new Vector3(0,0,90));
                tmpDyno.GetComponent<SpriteRenderer>().color=Color.red;
                dy-=0.3f;
            }
            if(i==37){
                tmpDyno.GetComponent<SpriteRenderer>().color=Color.red;
            }
            if(i>37){
                dx-=0.3f;
                tmpDyno.GetComponent<SpriteRenderer>().color=Color.red;
            }
            
        }
        //-2.9
        
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
                int tp = data[k];
                int pos = Random.Range(0,data.Length-1);

                data[k]=data[pos];
                data[pos]=tp;
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
        //float x=2.75f;
        //float y=0.0f;
        float x =-3.48f;
        float y =0.96f;
        for (int i=0;i<12;i++){
            
            var temp = Instantiate(picCharacter, new Vector3(x,y,-5.0f), Quaternion.identity);
            temp.name=KeyBoard[i].ToString();
            temp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(KeyBoard[i].ToString());
            temp.GetComponent<Transform>().Rotate(new Vector3(0,180,0));
            if (i==2 || i==5 || i==8 || i==12){
                //x=2.75f;
                //y-=0.5f;
                x =-3.48f;
                y-=0.5f;
            }
            else{
                x-=0.5f;
            }
         	//Track AnswerLocation

         	if(!Answer.ToUpper().Contains(temp.name)){
         		ansPos.Add(temp);
         	}   
        }
    }
    void answerCreation(){

        float bx=-0.5f;
        float by=-1.92f;
        blankSprites = new SpriteRenderer[Answer.Length];
      
        for(int i=0;i<Answer.Length;i++){
            var blk = Instantiate(blankCharacter, new Vector3(bx,by,-5.0f), Quaternion.identity);
            blk.name="Blank";
            blankSprites[i] = blk.GetComponent<SpriteRenderer>();
            blankSprites[i].GetComponent<Transform>().Rotate(new Vector3(0,180,0));
            bx-=0.5f;
        }
    }
    void SubmitAnswerAction(string answer)
   {
   	if (string.Compare(answer.ToUpper(),Answer.ToUpper())==0){
     	
   		
      	infoMessage.text = "Correct";
      	infoMessage.color = Color.green;
      	timeDisplay.text="";
        if (currencyTrigger==false){
            computeReward();
        }
        resetSinks();
        if(themeInstance.previousQuestion[currentLevel].passed<themeInstance.previousQuestion[currentLevel].question.Count){
             StartCoroutine(NextQuestionScene());
        }
        else{
            StartCoroutine(ChangeScene());
        }
       
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
        int maxCurrency = 200;
        if(levelTimer<15){
            maxCurrency=50;
        }
        if(levelTimer<10){
            maxCurrency=20;
        }
        if(levelTimer<5){
            maxCurrency=5;
        }
        User currencyObj = readUserinfo();
        currencyObj.currency += maxCurrency;
        //Unlock next level
        Themeprogress thm = readTheme(currencyObj);
        thm.previousQuestion[currentLevel].passed+=1;

        if(thm.previousQuestion[currentLevel].passed>=5 && thm.unlocked==currentLevel && thm.unlocked+1<StaticClass.MaxLevels){
            thm.unlocked += 1;
            
        }
        writeUserinfo(writeTheme(currencyObj,thm));
   }
   
   void readJson(string filePath){

        var jsonTextFile = Resources.Load<TextAsset>(filePath);
        themeObject = JsonUtility.FromJson<Theme>(jsonTextFile.ToString());
      
        imageserverURL = themeObject.imageServer;
        
        int gameLevel =-1; 
        for(int i=0;i<themeObject.levels.Count;i++){
            if(themeObject.levels[i].name=="level"+currentLevel.ToString()){
                gameLevel=i;
            }
        }
        

        try{

           
            int poolSelection = randomQuestionWithTracking(themeObject.levels[gameLevel].questions.Count); 
        
            //int poolSelection=0;
            Answer = themeObject.levels[gameLevel].questions[poolSelection].answer;
            Questions = themeObject.levels[gameLevel].questions[poolSelection].imageList;
        }
        catch (System.ArgumentOutOfRangeException e){
             infoMessage.text = "Missing\nQuestions.";
             infoMessage.color = Color.red;
             StartCoroutine(ChangeScene());
        }
       
           
    }
    int randomQuestionWithTracking(int poolSize){


           

            int pos = -1;
            bool found = false;
            

            List<int> tmp = new List<int>();
            for(int i=0;i<poolSize;i++){
                tmp.Add(i);
            }

          
        
            for(int i=0;i<themeInstance.previousQuestion.Count;i++)
            {
                
                if(themeInstance.previousQuestion[i].level==currentLevel)
                {
                   
                    if(themeInstance.previousQuestion[i].question.Count!=poolSize){
                        found = true;
                        for(int k=0;k<themeInstance.previousQuestion[i].question.Count;k++){
                            tmp.Remove(themeInstance.previousQuestion[i].question[k]);
                        }
                       
                        if(tmp.Count>1){
                            pos=shuffleInt(tmp.ToArray())[0];
                        }
                        else{
                            pos=tmp[0];
                        }
                        themeInstance.previousQuestion[i].question.Add(pos);
                        
                    }
                }
                   
            }
            if(!found)
            {
                
                pos = Random.Range(0,poolSize-1);
                Questionpool qp = new Questionpool();
                qp.level=currentLevel;
                qp.question = new List<int>();
                qp.question.Add(pos);

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
        string contents="";
        while((line = file.ReadLine())!=null){
            contents+=line.Replace("\t","");
        }
        file.Close();
        
        return JsonUtility.FromJson<User>(contents);

    }
    void resetSinks(){
        StaticClass.freezeTimeEnabled=false;

    }
    IEnumerator fetchImages(){
        Debug.Log("Fetching images");

        questionSprites = new Sprite[Questions.Count];
        for(int i=0;i<Questions.Count;i++){
             
            WWW spriteURL  = new WWW(imageserverURL+"imageName="+Questions[i]);
            yield return spriteURL;

            try{
                //
                questionSprites[i] = Sprite.Create(spriteURL.texture,new Rect(0, 0, spriteURL.texture.width,spriteURL.texture.height), new Vector2(0, 0));
            }
            catch(System.NullReferenceException e){
                infoMessage.text = "Is the\nserver up?";
                infoMessage.color = Color.red;
                StartCoroutine(ChangeScene());
            }
        }

    }
    void expandImageFrame(SpriteRenderer IMAGE){

        if(expandTrack.ContainsKey(IMAGE)){

            if (expandTrack[IMAGE]){
              
                IMAGE.GetComponent<Transform>().localScale = new Vector3(xs,ys,zs);
                if(xs<0.75f && !revt){
                    xs+=diff;
                    ys+=diff;
                    zs+=diff;
                }
                else{
                    revt=true;
                }
                   
                if(revt && xs>0.49f){
                    xs-=diff;
                    ys-=diff;
                    zs-=diff;
                }

                if(revt && xs<=0.49){
                    revt=false;
                    expandTrack[IMAGE]=false;
                }

            }
        }
        else{
            expandTrack.Add(IMAGE,true);
        }
        
    }
    void revelCharacter(){
        int randPos = Random.Range(0,Answer.Length);
        blankSprites[randPos].GetComponent<BoxCollider>().enabled=false;
        blankSprites[randPos].name=Answer[randPos].ToString();
        blankSprites[randPos].sprite = Resources.Load<Sprite>(Answer[randPos].ToString());
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

        timeDisplay.text = ":" + System.String.Format("{0:0.0}", levelTimer);

        if ((Time.time >= StaticClass.currentFreezeTimeEnd) && StaticClass.freezeTimeEnabled == true)
        {
            StaticClass.freezeTimeEnabled = false;

        }

        if (StaticClass.freezeTimeEnabled)
        {
            levelTimer -= Time.deltaTime / StaticClass.freezeTimeFactor;
            timeDisplay.color = Color.cyan;
        }
        else
        {

            levelTimer -= Time.deltaTime;
            timeDisplay.color = Color.red;
        }

        if(fixedTimer-levelTimer>0.6 && levelTimer>=0){
            Destroy(dynamicTimer[0]);
            dynamicTimer.RemoveAt(0);
            fixedTimer=levelTimer;
        }

        if(StaticClass.removeKey){
            Debug.Log("Destroyed Object");
            StaticClass.removeKey=false;
            Destroy(ansPos[0]);
            ansPos.RemoveAt(0);

        }
        if(StaticClass.tokenHint){
            revelCharacter();
            StaticClass.tokenHint=false;

        }

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
               expandImageFrame(image1);  

        	   if(levelTimer<15){
        	       image2.sprite = questionSprites[1];
                   diff=0.002f;
                   expandImageFrame(image2);
        		}
        		if(levelTimer<10){
        		   image3.sprite = questionSprites[2]; 
                   diff=0.002f;
                   expandImageFrame(image3);
        		}
        		if(levelTimer<5){
        			image4.sprite = questionSprites[3];
                    diff=0.002f;
                    expandImageFrame(image4);
        		}
                
                //Resources.Load<Sprite>("<file_name>");
    	}
        //Update the coinValue
        var coinsText1 = GameObject.FindWithTag("currencyValue").GetComponent<TextMesh>();
        User currencyObj = readUserinfo();
        //coinVal.text = currencyObj.currency.ToString();
        coinsText1.text = currencyObj.currency.ToString();


    }
     IEnumerator ChangeScene(){
     	yield return new WaitForSeconds(1);
     	UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectionScene");
     }
     IEnumerator NextQuestionScene(){
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
     }

}
