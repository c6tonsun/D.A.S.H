using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeSingle : MonoBehaviour {

    public GameObject player;
    public GameObject jojo;
    
    public float jojoSpeed;
    public float jojoLifeTime;

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
        GameObject newJojo = Instantiate(jojo, transform.position, Quaternion.identity);

        // aim
        Vector3 target = player.transform.position - transform.position;
        target.Normalize();

        // shoot
        newJojo.GetComponent<Rigidbody2D>().AddForce(target * jojoSpeed, ForceMode2D.Impulse);
        newJojo.transform.right = -newJojo.GetComponent<Rigidbody2D>().velocity;

        Destroy(newJojo, jojoLifeTime);
    }
}
