using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour {
    [SerializeField]
    private int damage;
    private float speed;
    private float time;
    private float angle;
    private float gravity;
    [SerializeField]
    private float blastArea;
    private bool groundHited;
    [SerializeField]
    private SphereCollider shellCollider; //using to collide with ground
    [SerializeField]
    private SphereCollider shellCollider_Trigger; // using to collide with enemies
    float baseY;
    public void SetShellSpeedAndDamage(float speed, int damage)
    {
        this.speed = speed;
        this.damage = damage;
    }
    private void Start()
    {
        gravity = Physics.gravity.magnitude;
    }

    private void Update()
    {
        if (!groundHited)
        {
            time += Time.deltaTime;
            transform.localPosition += MathCalculations.MiscalculationParabolicTrajectoryOfBody(time, speed, angle, gravity);
            if (time > 10)
            {
                ReturnToShellPool();
            }
        }
    }
    private void OnEnable()
    {

        ConfigureShell();
    }
    private void ConfigureShell()
    {
        groundHited = false;
        transform.localEulerAngles = new Vector3(transform.parent.eulerAngles.x, 0, transform.parent.eulerAngles.z);
        transform.parent.eulerAngles = new Vector3(0, transform.parent.eulerAngles.y, 0);

        angle = transform.localEulerAngles.x;
        angle = angle * Mathf.Deg2Rad;

        GunController.OnGameEnded += ReturnToShellPool;

        shellCollider.enabled = true;
        shellCollider_Trigger.enabled = false;
        GetComponent<MeshRenderer>().enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            float distToTarget = Vector3.Distance(transform.position, other.transform.position);
            Debug.LogError("distance to target = " + distToTarget);
            if (distToTarget <= blastArea)
            {
                other.GetComponent<EnemyController>().GetDamage(MathCalculations.CalculateDamageByDistance(blastArea, distToTarget, damage));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            groundHited = true;
            shellCollider.enabled = false;
            shellCollider_Trigger.enabled = true;
            GetComponent<MeshRenderer>().enabled = false;
            Invoke("ReturnToShellPool", 0.5f);
        }
    }

    private void ReturnToShellPool()
    {
        time = 0;
        GunController.OnGameEnded -= ReturnToShellPool;
        transform.localPosition = Vector3.zero;
        GunController.getGunControll.AddShellToPool(transform.parent.gameObject);
    }
    private void OnApplicationQuit()
    {
        GunController.OnGameEnded -= ReturnToShellPool;
    }
}
