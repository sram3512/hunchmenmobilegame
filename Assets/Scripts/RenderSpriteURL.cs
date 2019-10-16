using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderSpriteURL : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer img;
    private string url="http://spriteserver.sanupindi.com:5000/hello";
   	IEnumerator Start()
    {
        WWW serverUrl = new WWW(url);
        yield return serverUrl;
        img.sprite = Sprite.Create(serverUrl.texture,new Rect(0, 0, serverUrl.texture.width, serverUrl.texture.height), new Vector2(0, 0));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
