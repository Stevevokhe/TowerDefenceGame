using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(SphereCollider))]
public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform target;
    [SerializeField] private Transform firingPoint;
    private SphereCollider sphereCollider;
    [SerializeField] private float range,firingSpeed,projectileDagame,projectileSpeed;
    private float firingSpeedCounter;
    private bool canFire;
    List<Transform> enemies;

    [UnityEngine.Range(0f, 1f)]
    public float indicatorAalpha = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemies = new List<Transform>();
        sphereCollider = GetComponent<SphereCollider> ();
        sphereCollider.radius = range;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0) {
            target = enemies[0];
        }
        if (target != null)
        {
            transform.LookAt(target.position);
            canFire = true;
        }
        else {
            canFire = false;
        }
        
        if(firingSpeed<firingSpeedCounter && canFire)
        {
            Fire();
            firingSpeedCounter = 0;
        }
        firingSpeedCounter += Time.deltaTime;
    }

    private void Fire()
    {       
        Projectile shotbullet = Instantiate(bullet, firingPoint).GetComponent<Projectile>();
        shotbullet.SetTarget(target, projectileSpeed, projectileDagame);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            enemies.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            enemies.Remove(other.transform);
        }
    }


    private void OnDrawGizmos()
    {
        // Set the color with custom alpha.
        Gizmos.color = new Color(1f, 0f, 0f, indicatorAalpha); // Red with custom alpha

        // Draw the sphere.
        Gizmos.DrawSphere(transform.position, range);

        // Draw wire sphere outline.
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
