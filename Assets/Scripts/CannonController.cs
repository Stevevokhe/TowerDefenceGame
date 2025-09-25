using UnityEngine;
[RequireComponent (typeof(SphereCollider))]
public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private Transform target;
    [SerializeField] private Transform firingPoint;
    private SphereCollider sphereCollider;
    [SerializeField] private float range,firingSpeed,projectileDagame,projectileSpeed;
    private float firingSpeedCounter;

    
    [Range(0f, 1f)]
    public float indicatorAalpha = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider> ();
        sphereCollider.radius = range;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        transform.LookAt(target.position);
        if(firingSpeed<firingSpeedCounter)
        {
            Fire();
            firingSpeedCounter = 0;
        }
        firingSpeedCounter += Time.deltaTime;
    }

    private void Fire()
    {
        bullet = Instantiate(bullet, firingPoint);
        bullet.gameObject.GetComponent<Projectile>().SetTarget(target,projectileSpeed,projectileDagame);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(target != null && collision.transform.CompareTag("Enemy"))
        {
            target = collision.transform;
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
