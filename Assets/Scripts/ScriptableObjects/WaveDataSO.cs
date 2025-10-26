using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Wave/Wave Data")]
public class WaveDataSO : ScriptableObject
{
    public EnemyType enemyType;
    public float spawnInterval;
    public int enemiesPerWave;

}
