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

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    void Update()
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
                bullet.GetComponent<BulletType>().shotType = GetComponent<HeroState>().shotType;
            }
                //    }
            //}
        }
    }
}
