using UnityEngine;

public class RangeSingle : MonoBehaviour {

    public GameObject player;
    public GameObject projectiles;

    private Animator _anim;

    public bool isJojoDude;
    public float projectileSpeed;
    public float projectileLifeTime;

    public float firstShoot;
    public float shootRate;
    private float _shootTimer;

    public float startShoot;
    public float endShoot;
    public bool _shoot;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        if (isJojoDude)
        {
            startShoot = 0.8f;
            endShoot = 0.1f;
        }
        else
        {
            startShoot = 0.6f;
            endShoot = 0.2f;
        }
    }

    private void OnEnable()
    {
        if (isJojoDude)
        {
            _anim.runtimeAnimatorController =
               Resources.Load("90sKid") as RuntimeAnimatorController;
            _anim.Play("90s_kid_idle", -1, 0f);
        }
        else
        {
            _anim.runtimeAnimatorController =
               Resources.Load("Binary") as RuntimeAnimatorController;
            _anim.Play("binary_guy_idle", -1, 0f);
        }
        _shootTimer = firstShoot;
    }

    private void FixedUpdate()
    {
        _anim.SetBool("shoot", _shoot);

        _shootTimer -= Time.fixedDeltaTime;

        if (_shootTimer < startShoot && _shootTimer > endShoot)
        {
            _shoot = true;
        }
        else
        {
            _shoot = false;
        }

        if (_shootTimer <= 0)
        {
            Shoot();
            _shootTimer = shootRate;
        }
    }

    private void Shoot()
    {
        // make
        GameObject newProjectile;

        if (isJojoDude)
        {
            newProjectile = Instantiate(
                    projectiles.transform.GetChild(0).gameObject,
                    transform.position,
                    Quaternion.identity);
        }
        else
        {
            newProjectile = Instantiate(
                    projectiles.transform.GetChild(Random.Range(1, 3)).gameObject,
                    transform.position,
                    Quaternion.identity);
        }

        // aim
        Vector3 target = player.transform.position - transform.position;
        target.Normalize();

        // shoot
        newProjectile.GetComponent<Rigidbody2D>().AddForce(target * projectileSpeed, ForceMode2D.Impulse);
        newProjectile.transform.right = -newProjectile.GetComponent<Rigidbody2D>().velocity;

        Destroy(newProjectile, projectileLifeTime);
    }
}
