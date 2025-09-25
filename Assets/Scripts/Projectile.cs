using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed,damage;

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public float GetDamage()
    {
        return damage;
    }
    public void SetTarget(Transform t,float s, float d)
    {
        speed = s;
        damage = d;
        target = t;
    }
}
