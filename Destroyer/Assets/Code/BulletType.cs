using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShotType
{ Hor, Vert }

public class BulletType : MonoBehaviour
{
    [HideInInspector]
    public ShotType shotType;
}
