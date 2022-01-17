using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController _instance;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] private Animator _anim;

    [SerializeField] float horizontalSpeed;
    [SerializeField] float verticalSpeed;

    [SerializeField] int direction = 0;

    public Transform tempTransform;

    [SerializeField] Transform limitLeft, limitRight, limitTop, limitBottom;

    [SerializeField] Transform shootPoint;
    [SerializeField] float shootForce;
    [SerializeField] GameObject bulletPrefab;
    bool isShooting = false;

    bool isDead = false;

    public void SetLimits(Transform _limitLeft, Transform _limitRight, Transform _limitTop, Transform _limitBottom)
    {
        limitLeft = _limitLeft;
        limitRight = _limitRight;
        limitTop = _limitTop;
        limitBottom = _limitBottom;
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
        tempTransform = transform;
        direction = 1;
    }

    void Movement()
    {
        float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float vertical = CrossPlatformInputManager.GetAxis("Vertical");

        if (!isShooting)
        {
            if (horizontal != 0)
            {
                direction = (int)Mathf.Sign(horizontal);
                _anim.SetInteger("State", 1);
            }
            else if (vertical != 0)
                _anim.SetInteger("State", 1);
            else
                _anim.SetInteger("State", 0);
        }

        FlipSprite();

        Vector2 pos = tempTransform.position;
        pos.x = Mathf.Clamp(pos.x + horizontal * horizontalSpeed * Time.deltaTime, limitLeft.position.x, limitRight.position.x);
        pos.y = Mathf.Clamp(pos.y + vertical * verticalSpeed * Time.deltaTime, limitBottom.position.y, limitTop.position.y);
        tempTransform.position = pos;
    }

    void Shoot()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            _anim.SetInteger("State", 2);
            isShooting = true;
        }
    }

    public void ShootProjectile()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        // bullet.GetComponent<Rigidbody2D>();
        // Vector2.right * shootForce * direction * Time.deltaTime
        bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction * shootForce, 0));
    }

    public void ShootingFinished()
    {
        isShooting = false;
        _anim.SetInteger("State", 0);
    }

    void Update()
    {
        if (GameState._instance.IsState(Game_State.Gameplay))
        {
            if (BarsManager._instance.IsAlive())
            {
                Movement();
                Shoot();
            }
            else
            {
                if (!isDead)
                {
                    GameplayMenuManager._instance.LoadEndScreen(false);
                    isDead = true;
                }
            }
        }
    }

    void FlipSprite()
    {
        //By rotations
        tempTransform.eulerAngles = new Vector3(0, direction == 1 ? 0 : 180, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            BarsManager._instance.DamageLife(10);
            Destroy(other.gameObject);
        }
    }
}
