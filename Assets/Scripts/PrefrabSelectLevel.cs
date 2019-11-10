using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class PrefrabSelectLevel : MonoBehaviour
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

    private int unlock;
    private string userPath;
    public Dictionary<string,int> levelMap;

    void Start()
    {
        userPath = Application.persistentDataPath+"/user_info.json";
        //GetComponent<Renderer>().material.color = Color.yellow;
        readUserJson();
        levelMap = new Dictionary<string,int>();
        levelMap.Add("EASY",0);
        levelMap.Add("MEDIUM",1);
        levelMap.Add("HARD",2);
    }

    // Update is called once per frame
    void Update()
    {
        var levelValue = levelMap[GetComponent<TextMesh>().text];
        if(levelValue<=unlock){
            GetComponent<Renderer>().material.color = Color.red;
        }
        else{
            GetComponent<Renderer>().material.color = Color.black;
        }
    }
    void readUserJson(){

        StreamReader file = new StreamReader(userPath);
        string line;
        string usercontents = "";
        while ((line = file.ReadLine()) != null)
        {
            usercontents += line.Replace("\t", "");
        }
        file.Close();

        User userobj = JsonUtility.FromJson<User>(usercontents);

        foreach (Themeprogress thm in userobj.progress){
            if(thm.theme==StaticClass.ThemeSelection){ 
                unlock=thm.unlocked;
            }
        }
    }
    void OnMouseDown(){
        var levelValue = levelMap[GetComponent<TextMesh>().text];
        if(levelValue<=unlock){
        StaticClass.LevelSelection = GetComponent<TextMesh>().text;
    	UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }
}
