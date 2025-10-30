using System.Collections;
using UnityEngine;

public class LaserController : CannonController
{
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private Transform laserStart, laserEnd;
    private SphereCollider sphereCollider;
    [SerializeField] private float laserDuration;
    

    [Range(0f, 1f)]
    public float indicatorAalpha = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.radius = range;
    }

    

    public override void Fire()
    {
        laserLine.enabled = true;
        target.GetComponent<EnemyController>().TakeDamage(damage);
        laserStart = firingPoint;
        laserEnd = target.transform;
        laserLine.SetPosition(0, laserStart.position);
        laserLine.SetPosition(1, laserEnd.transform.position);
        canFire = false;
        StartCoroutine(DisableLaser());
    }

    private IEnumerator DisableLaser()
    {
        yield return new WaitForSeconds(laserDuration);
        laserLine.enabled = false;
    }   

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
