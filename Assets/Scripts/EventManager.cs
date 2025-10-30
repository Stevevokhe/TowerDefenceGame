using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Action<EnemyAgent> OnEnemyDied; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void EnemyDied(EnemyAgent agent) => OnEnemyDied?.Invoke(agent);
}
