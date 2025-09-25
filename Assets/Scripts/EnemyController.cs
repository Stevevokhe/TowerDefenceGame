using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform target;
    public float speed=5f, health;
    private EnemyPath enemyPath;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyPath = FindAnyObjectByType<EnemyPath>();
        target = enemyPath.NextWaypoint();
    }

    // Update is called once per frame
    void Update()
    {
        // Where from, where to, how fast
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed*Time.deltaTime);

        if(Vector3.Distance(transform.position, target.position) > 0.5f)
        {
            target = enemyPath.NextWaypoint();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            health-=collision.gameObject.GetComponent<Projectile>().GetDamage();
            if (health >= 0) {
                Destroy(collision.gameObject);
                Destroy(this.gameObject);
            }                        
        }   
    }
}
