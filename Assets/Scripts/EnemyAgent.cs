using UnityEngine;
using UnityEngine.AI;

public class EnemyAgent : MonoBehaviour
{
    int currentWaypoint, pathLentgh;
    public float speed = 5f, health;
    private NavMeshAgent agent;
    private EnemyPath path;
    private EventManager eventManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eventManager = GameObject.FindAnyObjectByType<EventManager>();
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {   

        if (agent.remainingDistance <.5f && path != null)
        {
            if (currentWaypoint + 1 >= pathLentgh)
            {
                currentWaypoint = -1;                
            }
            currentWaypoint++;
            agent.SetDestination(path.waypoints[currentWaypoint].position);
        }
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            eventManager.EnemyDied(this);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<Projectile>().GetDamage());
            Destroy(collision.gameObject);
        }
    }


    public void SetPath(EnemyPath enemyPath)
    {
        path = enemyPath;
        currentWaypoint = 0;
        pathLentgh = path.waypoints.Count;
    }
}
