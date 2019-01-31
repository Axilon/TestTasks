using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BorderSide {TOP,DOWN,LEFT,RIGHT }
public class BorderTeleport : MonoBehaviour {

    [SerializeField]
    private BorderSide border_Side;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "SnakeHead")
        {
            Vector3 teleport = collider.transform.position;
            switch (border_Side)
            {
                case (BorderSide.TOP):
                    teleport.z = -(teleport.z) + 1;
                    break;
                case (BorderSide.DOWN):
                    teleport.z = -(teleport.z) - 1;
                    break;
                case (BorderSide.LEFT):
                    teleport.x = -(teleport.x) - 1;
                    break;
                case (BorderSide.RIGHT):
                    teleport.x = -(teleport.x) + 1;
                    break;
            }
            collider.GetComponent<SnakeController>().tempPos = teleport;
        }
    }
}
