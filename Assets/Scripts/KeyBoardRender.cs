using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardRender : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject picCharacter;
    public GameObject blankCharacter;
    private SpriteRenderer[] blankSprites;
    void Start()
    {
    	string [] KeyBoard = new string[] {"A","B","C","D","E","F","G","H","I","J"};
    	
    	float x=0.0f;
    	float y=5.5f;
    	for (int i=0;i<10;i++){
    		Debug.Log(KeyBoard[i]);
    		var temp = Instantiate(picCharacter, new Vector3(x,y,-10.0f), Quaternion.identity);
    		temp.name=KeyBoard[i];
    		temp.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(KeyBoard[i]);

    		if (i==4){
    			x=0.0f;
    			y=2.5f;
    		}
    		else{
    			x+=1.0f;
    		}
    	}
        
        float bx=0.0f;
        float by=7.5f;
        blankSprites = new SpriteRenderer[4];
      
        for(int i=0;i<4;i++){
        	var blk = Instantiate(blankCharacter, new Vector3(bx,by,-10.0f), Quaternion.identity);
        	blk.name = "Blank";
        	blankSprites[i] = blk.GetComponent<SpriteRenderer>();
        	bx+=1.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {

    	if(StaticClass.KeyBoardInput!=null){
    		int pos = findFirstBlank();
    		if (pos!=-1){
    			blankSprites[pos].name=StaticClass.KeyBoardInput;
    			blankSprites[pos].sprite = Resources.Load<Sprite>(StaticClass.KeyBoardInput);
    		}
    		StaticClass.KeyBoardInput=null;
    	}
        
    }
    int findFirstBlank(){
    	for(int i=0;i<blankSprites.Length;i++){
    		if (blankSprites[i].name=="Blank"){
    			return i;
    		}
    	}
    	return -1;
    }
}
