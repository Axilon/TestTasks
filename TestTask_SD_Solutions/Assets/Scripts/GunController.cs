using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    [SerializeField]
    private float shellSpeed;
    [SerializeField]
    private int shellDamage;
    [SerializeField]
    private Tower player;
    [SerializeField]
    private GameObject shellPrefab;
    private Queue<GameObject> shellPool;
    [SerializeField]
    private Transform shellHolder;
    [SerializeField]
    private LayerMask layer;

    public delegate void GameEnded();
    public static event GameEnded OnGameEnded;
    public static GunController getGunControll { get; private set; }
    // Use this for initialization
    void Start () {
        InitializeGun();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.isAlive && GameController.instance.GameStarted && !GameController.instance.GameFinished)
        {
            if (Input.GetMouseButtonDown(0) )
            {
                Attack();
            }
        }
    }
    public void Attack()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layer))
        {

            Vector3 targetPosition = hit.point;
            float distance = MathCalculations.CalculateDistance3D(transform.position, targetPosition);
            transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);
            Vector3 rot = transform.eulerAngles;
            rot.x = MathCalculations.CalculateAngleToHitTarget(shellSpeed,distance, targetPosition);
            transform.eulerAngles = rot;
            RemoveShellFromPool();
        }

    }
    public void AddAllShellsToPool()
    {
        if (OnGameEnded != null)
        {
            OnGameEnded();
        }
    }
    public void AddShellToPool(GameObject shell)
    {
        shell.SetActive(false);
        shellPool.Enqueue(shell);
    }
    public void RemoveShellFromPool()
    {
        if (shellPool.Count == 0)
        {
            CreateAndAddShellToPoll();
        }
        GameObject shell = shellPool.Dequeue();
        shell.transform.eulerAngles = transform.eulerAngles;
        shell.SetActive(true);
    }
    private void InitializeGun()
    {
        if (getGunControll == null)
        {
            getGunControll = this;
        }
        shellPrefab.SetActive(false);
        shellPool = new Queue<GameObject>();
        for(int i = 0; i <10; i++)
        {
            CreateAndAddShellToPoll();
        }
    }

    private void CreateAndAddShellToPoll()
    {
        GameObject shell = Instantiate(shellPrefab,shellHolder);
        shell.transform.position = transform.position;
        shell.GetComponentInChildren<ShellController>().SetShellSpeedAndDamage(shellSpeed, shellDamage);
        AddShellToPool(shell);
    }
    


}
