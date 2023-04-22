using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenaceController : MonoBehaviour
{
    [SerializeField] public int type = 0;
    [SerializeField] public bool isActive = false;

    private void FixedUpdate()
    {
        if (transform.position.x < 2f)
        {
            isActive = true;
        }
    }
}
