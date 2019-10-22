using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LevelSelectionScript : MonoBehaviour
{

	public GameObject levelSign;
    public GameObject currencyDisplay;

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

    
    // Start is called before the first frame update
    void Start()
    {


        var displayObj = Instantiate(currencyDisplay, new Vector3(-12.1f,8.06f,-5.23f),Quaternion.identity);
        var currencyText = GameObject.FindWithTag("currencyValue").GetComponent<TextMesh>();

        var filePath = "Assets/Resources/jsonData/user_info.json";
        StreamReader file = new StreamReader(filePath);
        string line;
        string contents="";
        while((line = file.ReadLine())!=null){
            contents+=line.Replace("\t","");
        }

        User userobj = JsonUtility.FromJson<User>(contents);

        currencyText.text = userobj.currency.ToString();
        //displayObj.Currencyvalue.GetComponent<TextMesh>().text="5";

    	float x = -14.93f;
    	float y = 7.15f;
    	float z = -5.23f;
        StaticClass.MaxLevels = 9;
        for (int i=0;i<StaticClass.MaxLevels;i++){
            var lvs = Instantiate(levelSign, new Vector3(x,y,z),Quaternion.identity);
            lvs.GetComponent<TextMesh>().text = "Level "+(i+1).ToString();
            x+=2.0f;
            if(i ==2 || i==5 || i==8){
                y-=1.0f;
                x=-14.93f;
            }
           
        }
       
        
    }

}
