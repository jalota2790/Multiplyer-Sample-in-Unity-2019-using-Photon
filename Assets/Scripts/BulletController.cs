using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Rigidbody rb;

    /// <summary>
    /// Speed of bullet
    /// </summary>
    public float speed;

    /// <summary>
    /// Amount of Damage
    /// </summary>
    public float damage;

    void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<PlayerController>().OnGetDamage(damage);
        }
        Destroy(gameObject);
    }

}
