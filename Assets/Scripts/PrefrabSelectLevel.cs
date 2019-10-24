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
    private string userPath;
    void Start()
    {
        userPath = Application.persistentDataPath+"/user_info.json";
        GetComponent<Renderer>().material.color = Color.yellow;
        readUserJson();
    }

    // Update is called once per frame
    void Update()
    {
        
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
    void OnMouseEnter(){
        var levelValue = int.Parse(GetComponent<TextMesh>().text.Split()[1]);
        if(levelValue<=unlock){
            GetComponent<Renderer>().material.color = Color.red;
        }
    	

    }
    void OnMouseExit(){
    	GetComponent<Renderer>().material.color = Color.yellow;
    }
    void OnMouseDown(){
        var levelValue = int.Parse(GetComponent<TextMesh>().text.Split()[1]);
        if(levelValue<=unlock){
        StaticClass.LevelSelection = GetComponent<TextMesh>().text.Split()[1];
    	UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        }
    }
}
