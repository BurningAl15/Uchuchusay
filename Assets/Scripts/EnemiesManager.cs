using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager _instance;
    public int currentEnemies = 2;
    public int rounds = 0;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }

    public void RefillEnemiesRemaining()
    {
        currentEnemies = 2;
    }

    public void EnemyDied()
    {
        currentEnemies--;
        if (currentEnemies <= 0)
        {
            if (rounds < 3)
            {
                rounds++;
                StageManager._instance.BeatStage();
                RefillEnemiesRemaining();
            }
            else
            {
                GameplayMenuManager._instance.LoadEndScreen(true);
            }
        }
    }
}
