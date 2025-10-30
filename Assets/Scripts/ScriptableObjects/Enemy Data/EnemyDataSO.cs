using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData", menuName ="Data Objects/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    public float lives;
    public int damage;
    public float speed;
    public int rewardResource;
}
