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

    private class Level
    {
        public string Stage;
        public int level;
    }
    private StreamReader file;
    private StreamWriter output;
    private Level lvs;


    void Start()
    {

    	Debug.Log("Rendering 4 pics Enter the required word");
        file = new StreamReader("Assets/Resources/level.json");
        string contents = file.ReadToEnd();
        lvs = JsonUtility.FromJson<Level>(contents);
        Debug.Log(lvs.level);
        //var levelDisplay = gameObject.GetComponent<TextMesh>();

        levelDisplay.text = "Level "+lvs.level.ToString();
        levelDisplay.color = Color.red;
        file.Close();


    	image1.sprite = Resources.Load<Sprite>("soccer");
    	image2.sprite = Resources.Load<Sprite>("basketball");
    	image3.sprite = Resources.Load<Sprite>("football");
    	image4.sprite = Resources.Load<Sprite>("baseball");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
