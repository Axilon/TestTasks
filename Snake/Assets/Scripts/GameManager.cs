using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public int maxHeigt = 20;
    public int maxWidth = 20;

    public Color color1;
    public Color color2;

    public GameObject mapObject;
    SpriteRenderer mapRenderer;

    void Start () {
        CreateGroundMap();
	}
	
	private void CreateGroundMap()
    {
        mapRenderer = mapObject.GetComponent<SpriteRenderer>();
        Texture2D txt = new Texture2D(maxWidth, maxHeigt);
        for(int x= 0; x < maxHeigt; x++)
        {  
            for (int y = 0; y < maxWidth; y++)
            {
                #region Visual
                if (x % 2 != 0)
                {
                    if (y % 2 != 0)
                    {
                        txt.SetPixel(x, y, color1);
                    }
                    else
                    {
                        txt.SetPixel(x, y, color2);
                    }
                }
                else
                {
                    if (y % 2 != 0)
                    {
                        txt.SetPixel(x, y, color2);
                    }
                    else
                    {
                        txt.SetPixel(x, y, color1);
                    }
                }
                #endregion


            }
        }
        txt.filterMode = FilterMode.Point;
        txt.Apply();
        Rect rect = new Rect(0, 0, maxWidth, maxHeigt);
        Sprite sprite = Sprite.Create(txt, rect, Vector2.one /2, 1, 0, SpriteMeshType.FullRect);
        mapRenderer.sprite = sprite;
    }
    
}
