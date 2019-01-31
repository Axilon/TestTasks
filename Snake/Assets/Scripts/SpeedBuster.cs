using UnityEngine;

public class SpeedBuster : MonoBehaviour {
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
            if (boosterData.boosterType == BoosterType.SPEED_UP)
            {
                snakeController.SetBoostSpeed(snakeController.ticTimeLimit = snakeController.defaultTicTime / boosterData.boostValue);
            }
            else if (boosterData.boosterType == BoosterType.SLOW_DOWN)
            {
                    snakeController.SetBoostSpeed(snakeController.ticTimeLimit = snakeController.defaultTicTime * boosterData.boostValue);
            }
            BoosterController.instance.ResetBooster(gameObject);
        }
    }
}
