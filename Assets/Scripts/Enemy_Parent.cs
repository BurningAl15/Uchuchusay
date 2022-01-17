using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Parent : MonoBehaviour
{
    [SerializeField] Bar healthBar;

    protected virtual void Awake()
    {
        healthBar.InitBar();
    }
    void Start()
    {

    }

    void Update()
    {

    }

    public void DamageLife(float _damage)
    {
        healthBar.UpdateBar(-_damage);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            BarsManager._instance.DamageLife(10);
        }
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);

            DamageLife(10);

            if (!healthBar.isActive)
            {
                Destroy(gameObject);
                EnemiesManager._instance.EnemyDied();
                //Stop Game
            }
        }
    }
}
