using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] WaveEventSO enemyWave;
    [SerializeField] EnemyAgent enemy;
    [SerializeField] float spawnCooldown, wavesRemaining;
    [SerializeField] EnemyPath path;
    bool canSpawn = true;
   

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            canSpawn = false;
            if(wavesRemaining >= 0)
            {
                StartCoroutine(SpawnWave(enemyWave));
                wavesRemaining--;
            }
            
        }
    }

    IEnumerator SpawnWave(WaveEventSO enemyWaveToRelese)
    {
        yield return new WaitForSeconds(2);
        foreach (EnemyAgent enemyToSpawn in enemyWaveToRelese.enemies)
        {
            Instantiate(enemy, transform.position, Quaternion.identity).SetPath(path);           
            yield return new WaitForSeconds(2);
        }
        
        canSpawn = true;
    }

   
}
