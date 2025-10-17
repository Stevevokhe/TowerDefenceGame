using System.Collections;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private Transform target;
    [SerializeField] private Transform firingPoint;
    private SphereCollider sphereCollider;
    [SerializeField] private float range, firingSpeed, damage, laserDuration;
    private float firingSpeedCounter;
    private bool canFire;

    [Range(0f, 1f)]
    public float indicatorAalpha = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = range;
    }

    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target.position);
            canFire = true;
        }
        else
        {
            canFire = false;
        }

        if (firingSpeed < firingSpeedCounter && canFire)
        {
            Fire();
            firingSpeedCounter = 0;
        }


        firingSpeedCounter += Time.deltaTime;
    }

    private void Fire()
    {
        laserLine.enabled = true;
        target.GetComponent<EnemyController>().TakeDamage(damage);
        laserLine.SetPosition(0, firingPoint.position);
        laserLine.SetPosition(1, target.transform.position);
        canFire = false;
        StartCoroutine(DisableLaser());
    }

    private IEnumerator DisableLaser()
    {
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            target = other.transform;
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
