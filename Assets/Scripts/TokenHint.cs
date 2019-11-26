using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TokenHint : MonoBehaviour
{
    // Start is called before the first frame update
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
    private User userobj;
    private bool enable;
    private string userPath;


    void Start()
    {
    	StaticClass.tokenHint=false;
    	userPath = Application.persistentDataPath+"/user_info.json";
        
    }

    // Update is called once per frame
    void Update()
    {
    	userobj = readUserJson();
    	if(userobj.currency>=StaticClass.tokenHintCost && StaticClass.tokenHint==false && StaticClass.tokenHintUsed<StaticClass.tokenHintLimit){
            GetComponent<SpriteRenderer>().sprite=Resources.Load<Sprite>("addLetter");
    		enable=true;
    	}
    	else{
    		GetComponent<SpriteRenderer>().sprite=Resources.Load<Sprite>("addLetterDisable");
            enable=false;
    	}
        
    }
    User readUserJson(){

        StreamReader file = new StreamReader(userPath);
        string line;
        string usercontents = "";
        while ((line = file.ReadLine()) != null)
        {
            usercontents += line.Replace("\t", "");
        }
        file.Close();

         return JsonUtility.FromJson<User>(usercontents);

    }
    void writeUserinfo(User userobj){
        
        StreamWriter writer = new StreamWriter(userPath);
        writer.WriteLine(JsonUtility.ToJson(userobj));
        writer.Close();
    }
    void OnMouseDown(){
    	if(enable){
    		StaticClass.tokenHint=true;
    		enable=false;
    		userobj.currency-=StaticClass.tokenHintCost;
    		writeUserinfo(userobj);
    	}

    }
}
