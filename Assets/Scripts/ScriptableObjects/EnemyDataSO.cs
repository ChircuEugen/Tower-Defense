using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy Data", menuName ="Enemy/Enemy Data")]
public class EnemyDataSO : ScriptableObject
{
    public float lives;
    public int damage;
    public float speed;
}
