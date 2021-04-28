using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiveScript : MonoBehaviour
{
    public float radius = 20f;
    public float force = 700f;

    public GameObject explosionEffect;

    bool exploded = false;

    void Start()
    {
    }

    private void Update()
    {
    }

    public void Explode()
    {
        if (exploded == false)
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);

            Collider[] cols = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in cols)
            {
                if (nearbyObject.TryGetComponent<enemyScript>(out enemyScript enemy))
                {
                    enemy.TakeCritDamage(35);
                }
                if (nearbyObject.TryGetComponent<enemy2Script>(out enemy2Script enemy2))
                {
                    enemy2.TakeCritDamage(25);
                }
                if (nearbyObject.TryGetComponent<playerController>(out playerController player))
                {
                    player.TakeDamage(30);
                }
            }
            exploded = true;
            Destroy(gameObject);
        }
    }
}
