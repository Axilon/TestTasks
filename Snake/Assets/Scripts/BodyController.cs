using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour {
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "SnakeHead")
        {
            GameController.instance.RestartGame();
        }
    }
}
