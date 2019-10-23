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
        userPath = "Assets/Resources/jsonData/user_info.json";
        var theme1 = Instantiate(themelabels, new Vector3(-18.67f,3.5f,-3.0f),Quaternion.identity);
        var theme2 = Instantiate(themelabels, new Vector3(-12.94f,3.5f,-3.0f),Quaternion.identity);
        var theme3 = Instantiate(themelabels, new Vector3(-8.51f,3.5f,-3.0f),Quaternion.identity);
    	
    	theme1.GetComponent<TextMesh>().text = "Theme 1";
    	theme2.GetComponent<TextMesh>().text = "Theme 2";
    	theme3.GetComponent<TextMesh>().text = "Theme 3";

        var coinsAmount = Instantiate(currencyDisplay, new Vector3(-11.94f, 5.5f, -3.0f), Quaternion.identity);
        //coinsAmount.GetComponent<Transform>().Rotate(new Vector3(0, 180, 0));
        var coinsText1 = GameObject.FindWithTag("currencyValue").GetComponent<TextMesh>();

        User currencyObj = readUserinfo();
        Debug.Log("CoinVal:" + currencyObj.currency.ToString());

        coinsText1.text = currencyObj.currency.ToString();

        coinsText1.text = currencyObj.currency.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        var coinsText1 = GameObject.FindWithTag("currencyValue").GetComponent<TextMesh>();
        User currencyObj = readUserinfo();
        Debug.Log("CoinVal:" + currencyObj.currency.ToString());
        
        coinsText1.text = currencyObj.currency.ToString();
    }
}
