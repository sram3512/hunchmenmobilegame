using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ThemeRender : MonoBehaviour
{
	public GameObject themelabels;
    public GameObject currencyDisplay;

    private string userPath;

    
    [System.Serializable]
    private class User
    {
        public string name;
        public string email_id;
        public int currency;
        public List<Themeprogress> progress;

    }
    [System.Serializable]
    private class Themeprogress
    {
        public string theme;
        public int unlocked;
        public List<Questionpool> previousQuestion;
    }
    [System.Serializable]
    private class Questionpool
    {
        public int level;
        public int question;
    }

    private User currencyObj;
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

    void newUserCreation(){

        //Ensure to add new theme values here
        string jsonOutput="{\"name\":\"tommy\",\"email_id\":\"tommy@usc.edu\",\"currency\":0,\"progress\":";
        jsonOutput += "[{\"theme\":\"harrypotter\",\"unlocked\":3,\"previousQuestion\":[]},";
        jsonOutput += "{\"theme\":\"halloween\",\"unlocked\":3,\"previousQuestion\":[]},";
        jsonOutput += "{\"theme\":\"movies\",\"unlocked\":3,\"previousQuestion\":[]}]}";
        
        StreamWriter writer = new StreamWriter(userPath);
        writer.WriteLine(jsonOutput);
        writer.Close();

    }
    // Start is called before the first frame update
    void Start()
    {
        userPath = Application.persistentDataPath+"/user_info.json";
        Debug.Log(userPath);

        var theme1 = Instantiate(themelabels, new Vector3(-18.67f,3.5f,-3.0f),Quaternion.identity);
        var theme2 = Instantiate(themelabels, new Vector3(-13.94f,3.5f,-3.0f),Quaternion.identity);
        var theme3 = Instantiate(themelabels, new Vector3(-9.51f,3.5f,-3.0f),Quaternion.identity);
    	
    	theme1.GetComponent<TextMesh>().text = "Harry Potter";
    	theme2.GetComponent<TextMesh>().text = "Halloween";
    	theme3.GetComponent<TextMesh>().text = "Movies";

        var coinsAmount = Instantiate(currencyDisplay, new Vector3(-11.94f, 5.5f, -3.0f), Quaternion.identity);
       
       
        
        if(!File.Exists(userPath)){
            //Call the below func incase we add new Theme
            newUserCreation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var coinsText1 = GameObject.FindWithTag("currencyValue").GetComponent<TextMesh>();
        User currencyObj = readUserinfo();        
        coinsText1.text = currencyObj.currency.ToString();
    }
}
