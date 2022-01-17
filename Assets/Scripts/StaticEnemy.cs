using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemy : Enemy_Parent
{
    public int direction;
    public float distance;

    public float maxDelay;
    public float delay;
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootForce;
    [SerializeField] GameObject bulletPrefab;
    Transform tempTransform;

    protected override void Awake()
    {
        base.Awake();
        tempTransform = transform;
        direction = -1;
    }

    void Update()
    {
        if (GameState._instance.IsState(Game_State.Gameplay))
        {
            if (PlayerController._instance.tempTransform.position.x - tempTransform.position.x > distance)
            {
                delay -= Time.deltaTime;
                if (delay <= 0)
                {
                    Shoot();
                    delay = maxDelay;
                }
            }
        }

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * shootForce, 0));
    }
}
