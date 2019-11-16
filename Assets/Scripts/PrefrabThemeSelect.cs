using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PrefrabThemeSelect : MonoBehaviour
{
    // Start is called before the first frame update
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

    void Start()
    {
        GetComponent<Renderer>().material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter(){
    	GetComponent<Renderer>().material.color = Color.red;

    }
    void OnMouseExit(){
    	GetComponent<Renderer>().material.color = Color.black;
    }
    void OnMouseDown(){

        StaticClass.backposition=0;
        StaticClass.ThemeSelection = GetComponent<TextMesh>().text.ToLower().Replace(" ","");
        incrementThemeSelection();
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectionScene");
    }

    void incrementThemeSelection()
    {
        Analytics analytics = readAnalyticsFile();
        bool found = false;
        foreach (ThemeWiseAnalytics themeWiseAnalytics in analytics.themeWise)
        {
            if (themeWiseAnalytics.name.Equals(StaticClass.ThemeSelection))
            {
                themeWiseAnalytics.count += 1;
                found = true;
            }
        }
        if (!found)
        {
            ThemeWiseAnalytics themeWiseAnalytics = new ThemeWiseAnalytics();
            themeWiseAnalytics.name = StaticClass.ThemeSelection;
            themeWiseAnalytics.count = 1;
            analytics.themeWise.Add(themeWiseAnalytics);
        }
        writeAnalyticsFile(analytics);
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
