using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnakeEffectBooster : MonoBehaviour {
    [SerializeField]
    private BoosterData boosterData;
    private void Start()
    {
        GetComponent<MeshRenderer>().material.color = boosterData.color;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "SnakeHead")
        {
            SnakeController snakeController = collider.GetComponent<SnakeController>();
            if (boosterData.boosterType == BoosterType.INCREASE)
            {
                snakeController.ExtendSnake();
            }
            else if(boosterData.boosterType == BoosterType.DECREASE)
            {
                snakeController.ShortenSnake();
            }
            else if (boosterData.boosterType == BoosterType.REVERSE)
            {
                snakeController.ReverseSnake();
            }
            BoosterController.instance.ResetBooster(gameObject);
        }
    }
}
