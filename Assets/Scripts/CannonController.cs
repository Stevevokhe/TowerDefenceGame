using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] public Transform target;
    [SerializeField] public Transform firingPoint;
    [SerializeField] private float turnSpeed = 2f, aimTime = 0.5f;
    [SerializeField] public float range, firingSpeed, damage, projectileSpeed;
    private float firingSpeedCounter;
    public bool canAim, canFire, isPreparingToShoot;
    public List<EnemyAgent> enemies;
    private EventManager eventManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        eventManager = GameObject.FindAnyObjectByType<EventManager>();
        eventManager.OnEnemyDied += RemoveEnemy;
    }

    private void OnDisable()
    {
        eventManager.OnEnemyDied -= RemoveEnemy;
    }

    void Start()
    {
        enemies = new List<EnemyAgent>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (canAim)
        {
            Vector3 relativePos = target.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, turnSpeed * Time.deltaTime);
            //transform.LookAt(target.position);
            if (isPreparingToShoot)
            {
                StartCoroutine(PrefireDelay());
                isPreparingToShoot = false;
            }
            else
            {
                canFire = false;
            }
        }


        if (firingSpeed < firingSpeedCounter && canAim)
        {
            Fire();
            firingSpeedCounter = 0;
        }
        firingSpeedCounter += Time.deltaTime;
    }

    public virtual void Fire()
    {
        Projectile shotbullet = Instantiate(bullet, firingPoint.position, Quaternion.identity).GetComponent<Projectile>();
        shotbullet.SetTarget(target, projectileSpeed, damage);
    }

    IEnumerator PrefireDelay()
    {
        Debug.Log("Preparing to fire");
        yield return new WaitForSeconds(aimTime);
        canFire = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            enemies.Add(other.GetComponent<EnemyAgent>());
            if (target == null)
            {
                target = enemies[0].gameObject.transform;
            }
            isPreparingToShoot = true;
            canAim = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            RemoveEnemy(other.GetComponent<EnemyAgent>());
        }
    }

    public void RemoveEnemy(EnemyAgent enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);    
            
        }
        Retarget();
    }

    public void Retarget() {
        //if enemy count is not 0 but the target is gone, do shit
        if (enemies.Count == 0 || target == null)
        {
            canAim = false;
        }
        else
        {
            target = enemies[0].gameObject.transform; canAim = true;
        }

        
    }

    
}
