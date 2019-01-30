using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRayCaster : MonoBehaviour {
    [SerializeField]
    private LayerMask layer;
    
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 200, layer))
            {
                if (hit.collider.gameObject.tag == "Ground")
                {
                    Debug.Log("Hit Ground");
                    GameController.instance.CreateRandomMesh(hit.point);
                }
                if (hit.collider.gameObject.tag == "ActiveGO")
                {
                    hit.collider.gameObject.GetComponent<GeometryObjectModel>().IncreaseClickCount();
                    Debug.Log("Hit GO");
                }
            }
        }
	}
}
