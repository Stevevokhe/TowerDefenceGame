using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform target;
    public float speed=5f, health;
    private EnemyPath enemyPath;
    private bool targetFound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyPath = FindAnyObjectByType<EnemyPath>();
        target = enemyPath.NextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetFound) {
            target = enemyPath.NextWaypoint();
            targetFound = true;
        }
        // Where from, where to, how fast
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);

        if(Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            target = enemyPath.NextWaypoint();
        }
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            Debug.Log("TargetHit");
            TakeDamage(collision.gameObject.GetComponent<Projectile>().GetDamage());
            Destroy(collision.gameObject);            
        }
    }

    
}
