using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject MinBlock;
    private float minSize;

    void Start()
    {
        Bounds bounds = MinBlock.GetComponent<SpriteRenderer>().bounds;
        minSize = bounds.max.x - bounds.min.x;
    }

    //void Update()
    //{
    //    //transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime); COOL
    //    //transform.RotateAround(gameObject.transform.position, Vector3.forward, transform.rotation.eulerAngles.z);
    //}

    private void ChangeBlocks(GameObject cl1, GameObject cl2, ShotType type, SpriteRenderer box, float angle)
    {
        if (type == ShotType.Hor)
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

        if (type == ShotType.Hor)
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
    }

    private void ChangeBlocksMachineGun(GameObject cl1, GameObject cl2, ShotType type, SpriteRenderer box, float angle)
    {
        if (type == ShotType.Hor)
        {
            cl1.transform.localScale = new Vector3(cl1.transform.localScale.x,
                   cl1.transform.localScale.y / 2.4f, cl1.transform.localScale.z);
            cl1.transform.position = new Vector3(transform.position.x,
                transform.position.y +
                (box.bounds.max.y - box.bounds.min.y) / 4,
                transform.position.z);
        }
        else
        {
            cl1.transform.localScale = new Vector3(cl1.transform.localScale.x / 2.4f,
                cl1.transform.localScale.y, cl1.transform.localScale.z);
            cl1.transform.position = new Vector3(transform.position.x -
                (box.bounds.max.x - box.bounds.min.x) / 4,
                transform.position.y, transform.position.z);
        }
        cl1.transform.RotateAround(transform.position, Vector3.forward, angle);

        if (type == ShotType.Hor)
        {
            cl2.transform.localScale = new Vector3(cl2.transform.localScale.x,
                   cl2.transform.localScale.y / 2.4f, cl2.transform.localScale.z);
            cl2.transform.position = new Vector3(transform.position.x,
                transform.position.y -
                (box.bounds.max.y - box.bounds.min.y) / 4,
                transform.position.z);
        }
        else
        {
            cl2.transform.localScale = new Vector3(cl2.transform.localScale.x / 2.4f,
                cl2.transform.localScale.y, cl2.transform.localScale.z);
            cl2.transform.position = new Vector3(transform.position.x +
                (box.bounds.max.x - box.bounds.min.x) / 4,
                transform.position.y, transform.position.z);
        }
        cl2.transform.RotateAround(transform.position, Vector3.forward, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            SpriteRenderer box = GetComponent<SpriteRenderer>();
            float angle = transform.rotation.eulerAngles.z;
            transform.RotateAround(transform.position, Vector3.forward, -angle);
            ShotType bulletType = collision.GetComponent<BulletType>().shotType;
            GunType gunType = collision.GetComponent<BulletType>().gunType;

            if ((bulletType == ShotType.Vert && (box.bounds.max.x - box.bounds.min.x) / 2 < minSize) ||
                (bulletType == ShotType.Hor && (box.bounds.max.y - box.bounds.min.y) / 2 < minSize))
            {
                if ((box.bounds.max.x - box.bounds.min.x) / 3 < minSize &&
                   (box.bounds.max.y - box.bounds.min.y) / 3 < minSize)
                { Destroy(gameObject); return; }

                if (gunType != GunType.MachineGun)
                {
                    transform.RotateAround(transform.position, Vector3.forward, angle);
                    return;
                }
                else
                {
                    if (bulletType == ShotType.Hor) bulletType = ShotType.Vert;
                    else bulletType = ShotType.Hor;
                }
            }

            GameObject cl1 = Instantiate(gameObject, transform.position, Quaternion.identity);
            GameObject cl2 = Instantiate(gameObject, transform.position, Quaternion.identity);
            if (gunType == GunType.MachineGun)
            { ChangeBlocksMachineGun(cl1, cl2, bulletType, box, angle); }
            else { ChangeBlocks(cl1, cl2, bulletType, box, angle); }

            if (cl1.GetComponent<Rigidbody2D>() == null)
            { cl1.AddComponent<Rigidbody2D>(); }
            if (cl2.GetComponent<Rigidbody2D>() == null)
            { cl2.AddComponent<Rigidbody2D>(); }

            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
