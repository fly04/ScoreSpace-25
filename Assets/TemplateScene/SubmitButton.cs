using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitButton : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    private void OnMouseDown()
    {
        Debug.Log("Submit button clicked");
        StartCoroutine(gameManager.SubmitScoreRoutine(gameManager.score));
    }
}
