using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class RenderImages : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer image1;
    public SpriteRenderer image2;
    public SpriteRenderer image3;
    public SpriteRenderer image4;
    public TextMesh levelDisplay;
    public TextMesh timeDisplay;

    private class Level
    {
        public string Stage;
        public int level;
    }
    private StreamReader file;
    private StreamWriter output;
    private Level lvs;
    private float levelTimer;


    void Start()
    {

    	Debug.Log("Rendering 4 pics Enter the required word");
        file = new StreamReader("Assets/Resources/jsonData/user_info.json");
        string contents = file.ReadToEnd();
        lvs = JsonUtility.FromJson<Level>(contents);
        Debug.Log(lvs.level);
        //var levelDisplay = gameObject.GetComponent<TextMesh>();

        levelDisplay.text = StaticClass.LevelSelection;
        levelDisplay.color = Color.red;
        file.Close();

        levelTimer = 20;
    	
    	
    }

    // Update is called once per frame
    void Update()
    {
    	timeDisplay.text = ":"+levelTimer.ToString();
    	timeDisplay.color = Color.black;
    	levelTimer -=Time.deltaTime;
    	if (levelTimer<0){
    		image1.sprite=null;
    		image2.sprite=null;
    		image3.sprite=null;
    		image4.sprite=null;
    		levelDisplay.text = "GAME OVER!!!";
    	}
    	else{
    		image1.sprite = Resources.Load<Sprite>("soccer");
    		if(levelTimer<15){
    			image2.sprite = Resources.Load<Sprite>("baseball");
    		}
    		if(levelTimer<10){
    			image3.sprite = Resources.Load<Sprite>("football");
    		}
    		if(levelTimer<5){
    			image4.sprite = Resources.Load<Sprite>("basketball");
    		}
    	}
    }
}
