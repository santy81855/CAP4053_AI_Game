using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public enum EnemyType { REGULAR, SPEED, TANK };
    private EnemyType enemyType;

    public int hp;
    public void start()
    {
        if (enemyType == EnemyType.TANK)
        {
            hp = 2;
        }
        else
        {
            hp = 1;
        }
    }
}
