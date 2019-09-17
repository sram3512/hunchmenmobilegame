using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



public class PlayerControls : MonoBehaviour
{
    private class Level
    {
        public string Stage;
        public int level;
    }

	private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        //StreamReader file = new StreamReader("Assets/Resources/level.json");
        //string contents = file.ReadToEnd();
        //Level lvs = JsonUtility.FromJson<Level>(contents);
        //Debug.Log(lvs.Stage);
        //Level lvs = JsonConvert.DeserializeObject<Level>(file.ReadToEnd());
        //Debug.Log(lvs.level);
    }

    // Update is called once per frame
    void Update()
    {
     	transform.Translate(Vector3.forward * speed * Time.deltaTime *Input.GetAxis("Horizontal"));
    }

    void OnCollisionEnter(Collision otherObject){
    	Destroy(otherObject.gameObject);
    	UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }
}
