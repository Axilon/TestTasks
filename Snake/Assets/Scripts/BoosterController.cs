using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterController : MonoBehaviour {
    public static BoosterController instance;
    public List<Transform> snakeTransform;
    [SerializeField]
    private int gameArea_Max_Height;        //z coordinates
    [SerializeField]
    private int gameArea_Min_Height;        //z coordinates
    [SerializeField]
    private int gameArea_Max_Width;         //x coordinates
    [SerializeField]
    private int gameArea_Min_Width;         //x coordinates
    [SerializeField]
    private GameObject booster_SpeedUp;
    [SerializeField]
    private GameObject booster_SlowDown;
    [SerializeField]
    private GameObject booster_Increse;
    [SerializeField]
    private GameObject booster_Decrease;
    [SerializeField]
    private GameObject booster_Reverse;
    // Use this for initialization
    void Awake () {
        if (instance == null)
        {
            instance = this;
        }
	}
    private void Start()
    {
        StartCoroutine(GenerateBoosters());
    }
    IEnumerator GenerateBoosters()
    {

        GameObject booster = Instantiate(booster_Increse, transform);
        AddActiveElementTransformToList(booster.transform);
        ResetBooster(booster);
        booster = Instantiate(booster_Decrease, transform);
        AddActiveElementTransformToList(booster.transform);
        ResetBooster(booster);
        yield return new WaitForSeconds(1);
        booster = Instantiate(booster_SpeedUp, transform);
        AddActiveElementTransformToList(booster.transform);
        ResetBooster(booster);
        yield return new WaitForSeconds(1);
        booster = Instantiate(booster_SlowDown, transform);
        AddActiveElementTransformToList(booster.transform);
        ResetBooster(booster);
        yield return new WaitForSeconds(2);
        booster = Instantiate(booster_Reverse, transform);
        AddActiveElementTransformToList(booster.transform);
        ResetBooster(booster);
        
    }

    public void ResetBooster(GameObject booster)
    {
        booster.transform.position = RandomizeBoosterPosition();
    }
    private Vector3 RandomizeBoosterPosition()
    {
        Vector3 newPosition = new Vector3(Random.Range(gameArea_Min_Width, gameArea_Max_Width), 0, Random.Range(gameArea_Min_Height, gameArea_Max_Height));
        foreach(Transform fullPositions in snakeTransform)
        {
            if(fullPositions.position == newPosition)
            {
                RandomizeBoosterPosition();
            }
        }
        return newPosition;
    }
    public void AddActiveElementTransformToList(Transform transform)
    {
        snakeTransform.Add(transform);
    }
    public void RemoveElementTransFormToList(Transform transform)
    {
        snakeTransform.Remove(transform);
    }
}
