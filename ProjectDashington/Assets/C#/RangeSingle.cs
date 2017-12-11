using UnityEngine;

public class RangeSingle : MonoBehaviour {

    public GameObject player;
    public GameObject projectiles;

    public bool isJojoDude;
    public float projectileSpeed;
    public float projectileLifeTime;

    public float firstShoot;
    public float shootRate;
    private float _shootTimer;

    private void OnEnable()
    {
        _shootTimer = firstShoot;
    }

    private void FixedUpdate()
    {
        _shootTimer -= Time.fixedDeltaTime;

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
