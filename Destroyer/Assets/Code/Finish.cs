using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Hero"))
        {
            if (SceneManager.GetActiveScene().buildIndex + 1 != SceneManager.sceneCountInBuildSettings)
            { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }
            else
            { SceneManager.LoadScene(0); }
        }
    }
}
