using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroState : MonoBehaviour
{
    [HideInInspector]
    public ShotType shotType;

    private void Start()
    {
        shotType = ShotType.Vert;
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
    }
}
