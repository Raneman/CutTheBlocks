using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float maxSpeed;
    private HeroState hero;
    private bool canFire;

    void Start()
    {
        hero = GetComponent<HeroState>();
        canFire = true;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    IEnumerator BulletWait()
    { canFire = false; yield return new WaitForSeconds(1); canFire = true; }
    IEnumerator BulletWaitMachineGun()
    { canFire = false; yield return new WaitForSeconds(0.1f); canFire = true; }

    void Update()
    {
        if (!canFire) return;
        if (hero.gunType != GunType.MachineGun)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                //if (hit.collider != null)
                //{
                //    GameObject hitObject = hit.transform.gameObject;
                //    if (hitObject.layer != LayerMask.NameToLayer("UI"))
                //    {
                //if (!EventSystem.current.IsPointerOverGameObject())
                if (!IsPointerOverUIObject())
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 dir = clickPos - (new Vector2(transform.position.x, transform.position.y));
                    dir.Normalize();
                    bullet.GetComponent<Rigidbody2D>().velocity = dir * maxSpeed;
                    bullet.GetComponent<BulletType>().shotType = hero.shotType;
                    bullet.GetComponent<BulletType>().gunType = hero.gunType;
                }
                //    }
                //}
                StartCoroutine(BulletWait());
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (!IsPointerOverUIObject())
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    Vector2 clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 dir = clickPos - (new Vector2(transform.position.x, transform.position.y));
                    dir.Normalize();
                    bullet.GetComponent<Rigidbody2D>().velocity = dir * maxSpeed;
                    bullet.GetComponent<BulletType>().shotType = hero.shotType;
                    bullet.GetComponent<BulletType>().gunType = hero.gunType;
                    StartCoroutine(BulletWaitMachineGun());
                    if (hero.shotType == ShotType.Hor) { hero.shotType = ShotType.Vert; }
                    else { hero.shotType = ShotType.Hor; }
                }
            }
        }
    }
}
