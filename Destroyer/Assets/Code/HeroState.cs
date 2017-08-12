using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunType
{
    Howitzer, Laser, MachineGun
}

public class HeroState : MonoBehaviour
{
    [HideInInspector]
    public ShotType shotType;
    [HideInInspector]
    public GunType gunType;


    private void Start()
    {
        shotType = ShotType.Vert;
        gunType = GunType.MachineGun;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (shotType == ShotType.Hor)
            { shotType = ShotType.Vert; }
            else { shotType = ShotType.Hor; }
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100, 100, 50, 50), shotType.ToString());
        GUI.Label(new Rect(300, 100, 50, 50), gunType.ToString());
    }
}
