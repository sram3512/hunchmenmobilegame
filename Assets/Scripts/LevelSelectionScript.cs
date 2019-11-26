using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelSelectionScript : MonoBehaviour
{

	public GameObject levelSign;
    public GameObject currencyDisplay;
    public GameObject returnBack;
    public GameObject menu;
    private string userPath;

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

    User readUserinfo()
    {
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
    
    // Start is called before the first frame update
    void Start()
    {

        userPath = Application.persistentDataPath+"/user_info.json";

        var displayObj = Instantiate(currencyDisplay, new Vector3(-12.1f,8.06f,-5.23f),Quaternion.identity);
        Instantiate(returnBack, new Vector3(-12.3f,3.83f,-5.23f),Quaternion.identity);
        
        var menuObj = Instantiate(menu, new Vector3(-11.63f,6.49f,-5.23f),Quaternion.identity);
        menuObj.GetComponent<Transform>().localScale = new Vector3(0.25f,0.25f,0.25f);

        var currencyText = GameObject.FindWithTag("currencyValue").GetComponent<TextMesh>();

        User userobj = readUserinfo();

        currencyText.text = userobj.currency.ToString();
        currencyText.color = Color.red;
        string[] modes = new string[3]{"EASY","MEDIUM","HARD"};

    	float x = -14.93f;
    	float y = 5.15f;
    	float z = -5.23f;
        StaticClass.MaxLevels = 3;
        for (int i=0;i<StaticClass.MaxLevels;i++){
            var lvs = Instantiate(levelSign, new Vector3(x,y,z),Quaternion.identity);
            lvs.GetComponent<TextMesh>().text = modes[i];
            x+=2.0f;
            if(i ==2 || i==5 || i==8){
                y-=1.0f;
                x=-14.93f;
            }
           
        }
       
        
    }

}
