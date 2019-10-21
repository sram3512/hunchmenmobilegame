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
        public int question;
    }

    private int unlock;
    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
        readUserJson("Assets/Resources/jsonData/user_info.json");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void readUserJson(string filePath){
        StreamReader file = new StreamReader(filePath);
        string line;
        string contents="";
        while((line = file.ReadLine())!=null){
            contents+=line.Replace("\t","");
        }

        User userobj = JsonUtility.FromJson<User>(contents);
        foreach (Themeprogress thm in userobj.progress){
            if(thm.theme==StaticClass.ThemeSelection){ 
                unlock=thm.unlocked;
            }
        }
    }
    void OnMouseEnter(){
        var levelValue = int.Parse(GetComponent<TextMesh>().text.Split()[1]);
        if(levelValue<=unlock){
            GetComponent<Renderer>().material.color = Color.red;
        }
    	

    }
    void OnMouseExit(){
    	GetComponent<Renderer>().material.color = Color.black;
    }
    void OnMouseDown(){
        var levelValue = int.Parse(GetComponent<TextMesh>().text.Split()[1]);
        if(levelValue<=unlock){
        StaticClass.LevelSelection = GetComponent<TextMesh>().text.Split()[1];
    	UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }
}
