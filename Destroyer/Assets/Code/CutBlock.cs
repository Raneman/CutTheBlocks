using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutBlock : MonoBehaviour
{
    void Update()
    {
        //transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime); COOL
        //transform.RotateAround(gameObject.transform.position, Vector3.forward, transform.rotation.eulerAngles.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            SpriteRenderer box = GetComponent<SpriteRenderer>();
            float angle = transform.rotation.eulerAngles.z;
            transform.RotateAround(transform.position, Vector3.forward, -angle);

            GameObject cl1 = Instantiate(gameObject, transform.position, Quaternion.identity);
            if (collision.GetComponent<BulletType>().shotType == ShotType.Hor)
            {
                cl1.transform.localScale = new Vector3(cl1.transform.localScale.x,
                       cl1.transform.localScale.y / 2, cl1.transform.localScale.z);
                cl1.transform.position = new Vector3(transform.position.x,
                    transform.position.y +
                    (box.bounds.max.y - box.bounds.min.y) / 4,
                    transform.position.z);
            }
            else
            {
                cl1.transform.localScale = new Vector3(cl1.transform.localScale.x / 2,
                    cl1.transform.localScale.y, cl1.transform.localScale.z);
                cl1.transform.position = new Vector3(transform.position.x -
                    (box.bounds.max.x - box.bounds.min.x) / 4,
                    transform.position.y, transform.position.z);
            }
            cl1.transform.RotateAround(transform.position, Vector3.forward, angle);

            GameObject cl2 = Instantiate(gameObject, transform.position, Quaternion.identity);
            if (collision.GetComponent<BulletType>().shotType == ShotType.Hor)
            {
                cl2.transform.localScale = new Vector3(cl2.transform.localScale.x,
                       cl2.transform.localScale.y / 2, cl2.transform.localScale.z);
                cl2.transform.position = new Vector3(transform.position.x,
                    transform.position.y -
                    (box.bounds.max.y - box.bounds.min.y) / 4,
                    transform.position.z);
            }
            else
            {
                cl2.transform.localScale = new Vector3(cl2.transform.localScale.x / 2,
                    cl2.transform.localScale.y, cl2.transform.localScale.z);
                cl2.transform.position = new Vector3(transform.position.x +
                    (box.bounds.max.x - box.bounds.min.x) / 4,
                    transform.position.y, transform.position.z);
            }
            cl2.transform.RotateAround(transform.position, Vector3.forward, angle);

            if (cl1.GetComponent<Rigidbody2D>() == null)
            { cl1.AddComponent<Rigidbody2D>(); }
            if (cl2.GetComponent<Rigidbody2D>() == null)
            { cl2.AddComponent<Rigidbody2D>(); }

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
