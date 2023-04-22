using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButtonController : MonoBehaviour
{

    BoxCollider2D boxCollider2D;

    //on click, reload the scene
    void OnMouseDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
