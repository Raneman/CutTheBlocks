using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    void Start()
    {

    }
    
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            if (collision.gameObject.layer != LayerMask.NameToLayer("Bullet"))
            {
                int foo = 5;
            }
            Destroy(collision.gameObject); 
        }
    }
}
