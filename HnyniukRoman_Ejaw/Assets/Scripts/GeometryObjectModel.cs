using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryObjectModel : MonoBehaviour {
    private int clickCount;
    private MeshRenderer objectMesh;
    [SerializeField]
    private Color startingColor;
    [SerializeField]
    private GeometryObjectData gameObjectSettings;
    [SerializeField]
    private TextMesh textMesh;


    private void Start()
    {
        InitializeObject();
    }

    private void InitializeObject()
    {
        clickCount = 0;
        textMesh.text = clickCount.ToString();
        objectMesh = GetComponent<MeshRenderer>();
        objectMesh.material.color = startingColor; 
        StartCoroutine(ColorRandomCoroutine());
    }

    public void IncreaseClickCount()
    {
        IncreaseCount();
    }
    private void IncreaseCount()
    {
        clickCount++;
        textMesh.text = clickCount.ToString();
        ChangeColor();
    }
    private void ChangeColor()
    {
        foreach (ClickColorData clickData in gameObjectSettings.ClicksData)
        {
            if (clickCount >= clickData.MinClicksCount && clickCount <= clickData.MaxClicksCount)
            {
                objectMesh.material.color = clickData.Color;
                break;
            }
        }
       
    }
    IEnumerator ColorRandomCoroutine()
    {
        for (; gameObject != null;)
        {
            yield return new WaitForSeconds(GameData.GetGameData.ObservableTime);
            objectMesh.material.color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));           
        }
        yield return null;
    }
}
