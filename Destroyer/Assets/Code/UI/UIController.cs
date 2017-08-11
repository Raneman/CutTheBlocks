using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private MoveHero hero;

    public void OnLeftDown()
    { hero.H = -1; }
    public void OnRightDown()
    { hero.H = 1; }
    public void OnLeftRightUp()
    { hero.H = 0; }
    public void OnJumpDown()
    { hero.TryJump(); }
}