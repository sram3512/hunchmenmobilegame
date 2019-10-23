using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FreezeTime : MonoBehaviour
{
    // Start is called before the first frame update

    private static readonly string USER_PATH = "Assets/Resources/jsonData/user_info.json";

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


    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("timefreeze");
        GetComponent<Transform>().Rotate(new Vector3(0, 180, 0));
        GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);

    }

    // Update is called once per frame
    void Update()
    {

        if (!StaticClass.freezeTimeEnabled){
            User userJson = readUserinfo();
            int currentCurrency = userJson.currency;
            if (canAffordFreezeTime(currentCurrency)){
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("timefreeze");
            }
            else{
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("timefreezedisable");
            }
            GetComponent<Transform>().localScale = new Vector3(0.7f, 0.7f, 1);
        }

    }

    User readUserinfo()
    {
        StreamReader file = new StreamReader(USER_PATH);
        string line;
        string usercontents = "";
        while ((line = file.ReadLine()) != null)
        {
            usercontents += line.Replace("\t", "");
        }
        file.Close();

        return JsonUtility.FromJson<User>(usercontents);
    }

    void writeUserinfo(User userobj)
    {
        StreamWriter writer = new StreamWriter(USER_PATH);
        writer.WriteLine(JsonUtility.ToJson(userobj));
        writer.Close();
    }

    bool canAffordFreezeTime(int currentCurrency) {
        if (currentCurrency - StaticClass.freezeTimeCost >= 0){
            return true;
        }
        else{
            return false;
        }
    }

    void OnMouseDown()
    {
        if (!StaticClass.freezeTimeEnabled)
        {
            User userJson = readUserinfo();
            int currentCurrency = userJson.currency;
            if (canAffordFreezeTime(currentCurrency))
            {
                userJson.currency = currentCurrency - StaticClass.freezeTimeCost;
                writeUserinfo(userJson);
                GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("frozen");
                GetComponent<Transform>().localScale = new Vector3(0.15f, 0.12f, 1);
                StaticClass.freezeTimeEnabled = true;
                StaticClass.currentFreezeTimeEnd = Time.time + (StaticClass.freezeTimeDuration * StaticClass.freezeTimeFactor);
            }
        }
    }
}
