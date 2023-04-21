using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreClicker : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.AddScore(1);
    }
}
