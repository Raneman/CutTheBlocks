using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ButtonJump : MonoBehaviour
{
    [SerializeField]
    private MoveHero hero;

#if UNITY_ANDROID
    public void OnMouseDown()
    { hero.TryJump(); }
#endif
}
