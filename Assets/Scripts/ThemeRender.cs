using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ThemeRender : MonoBehaviour
{
	public GameObject themelabels;
    public GameObject currencyDisplay;
    public GameObject money;

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
        public int passed;
        public List<int> question;
    }

    [System.Serializable]
    public class Analytics
    {
        public string name;
        public string email_id;
        public int success_tries;
        public int failure_tries;
        public int all_tries;
        public int freeze_time_count;
        public int reveal_character_count;
        public int delete_character_count;
        public int sink_and_success_count;
        public int sink_and_fail_count;
        public int first_time_tries;
        public int game_opened_count;
        public List<ThemeWiseAnalytics> themeWise;
        public List<LevelWiseAnalytics> levelWise;
    }

    [System.Serializable]
    public class ThemeWiseAnalytics
    {
        public string name;
        public int count;
    }

    [System.Serializable]
    public class LevelWiseAnalytics
    {
        public string name;
        public int count;
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
        jsonOutput += "[{\"theme\":\"movies\",\"unlocked\":0,\"passed\":0,\"previousQuestion\":[]},";
        jsonOutput += "{\"theme\":\"sports\",\"unlocked\":0,\"passed\":0,\"previousQuestion\":[]},";
        jsonOutput += "{\"theme\":\"harrypotter\",\"unlocked\":0,\"passed\":0,\"previousQuestion\":[]},";
        jsonOutput += "{\"theme\":\"appgenres\",\"unlocked\":0,\"passed\":0,\"previousQuestion\":[]},";
        jsonOutput += "{\"theme\":\"nature\",\"unlocked\":0,\"passed\":0,\"previousQuestion\":[]},";
        jsonOutput += "{\"theme\":\"christmas\",\"unlocked\":0,\"passed\":0,\"previousQuestion\":[]}]}";
        
        StreamWriter writer = new StreamWriter(userPath);
        writer.WriteLine(jsonOutput);
        writer.Close();

    }

    void createAnalyticsFile()
    {

        string jsonOutput = "{\"name\":\"tommy\",\"email_id\":\"tommy@usc.edu\",\"success_tries\":0,\"failure_tries\":0,\"all_tries\":0,\"freeze_time_count\":0,\"game_opened_count\":1,\"reveal_character_count\":0,\"delete_character_count\":0,\"themewise\":[]}";
        
        StreamWriter writer = new StreamWriter(StaticClass.analyticsFilePath);
        writer.WriteLine(jsonOutput);
        writer.Close();

    }


    // Start is called before the first frame update
    void Start()
    {
        userPath = Application.persistentDataPath+"/user_info.json";
        Debug.Log(userPath);
        //applications ,nature and christmas

        var theme1 = Instantiate(themelabels, new Vector3(-17.67f,3.5f,-3.0f),Quaternion.identity);
        var theme2 = Instantiate(themelabels, new Vector3(-13.94f,3.5f,-3.0f),Quaternion.identity);
        var theme3 = Instantiate(themelabels, new Vector3(-9.51f,3.5f,-3.0f),Quaternion.identity);
        var theme4 = Instantiate(themelabels, new Vector3(-13.94f,1.5f,-3.0f),Quaternion.identity);
    	var theme5 = Instantiate(themelabels, new Vector3(-17.67f,1.5f,-3.0f),Quaternion.identity);
        var theme6 = Instantiate(themelabels, new Vector3(-9.51f,1.5f,-3.0f),Quaternion.identity);




        Instantiate(money, new Vector3(-12.94f,2.55f,-3.0f), Quaternion.identity);
        Instantiate(money, new Vector3(-12.44f,2.55f,-3.0f), Quaternion.identity);
        Instantiate(money, new Vector3(-11.94f,2.55f,-3.0f), Quaternion.identity);

    	theme1.GetComponent<TextMesh>().text = "Movies";
    	theme2.GetComponent<TextMesh>().text = "Harry Potter";
    	theme3.GetComponent<TextMesh>().text = "App Genres";
        theme4.GetComponent<TextMesh>().text = "Sports";
        theme5.GetComponent<TextMesh>().text="Nature";
        theme6.GetComponent<TextMesh>().text="Christmas";

        var coinsAmount = Instantiate(currencyDisplay, new Vector3(-11.94f, 5.5f, -3.0f), Quaternion.identity);
    
       //newUserCreation();
        if(!File.Exists(userPath)){
            //Call the below func incase we add new Theme
            newUserCreation();
        }

        if (!File.Exists(StaticClass.analyticsFilePath))
        {
            //Call the below func incase we add new Theme
            StaticClass.firstTimePlayer = true;
            createAnalyticsFile();
            StaticClass.gameStarted = true;
        }
        else if (!StaticClass.gameStarted)
        {
            Analytics analytics = readAnalyticsFile();
            analytics.game_opened_count += 1;
            writeAnalyticsFile(analytics);
            StaticClass.gameStarted = true;
        }


    }

    // Update is called once per frame
    void Update()
    {
        var coinsText1 = GameObject.FindWithTag("currencyValue").GetComponent<TextMesh>();
        User currencyObj = readUserinfo();        
        coinsText1.text = currencyObj.currency.ToString();
        coinsText1.color=Color.red;
    }

    Analytics readAnalyticsFile()
    {

        StreamReader file = new StreamReader(StaticClass.analyticsFilePath);
        string line;
        string contents = "";
        while ((line = file.ReadLine()) != null)
        {
            contents += line.Replace("\t", "");
        }
        file.Close();

        return JsonUtility.FromJson<Analytics>(contents);

    }

    void writeAnalyticsFile(Analytics analytics)
    {

        StreamWriter writer = new StreamWriter(StaticClass.analyticsFilePath);
        writer.WriteLine(JsonUtility.ToJson(analytics));
        writer.Close();
    }
}
