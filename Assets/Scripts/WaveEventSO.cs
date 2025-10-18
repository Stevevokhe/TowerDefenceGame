using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WaveEvent", menuName = ("Waves/Wave"))]
public class WaveEventSO : ScriptableObject

{
    public List<EnemyAgent> enemies;
}
