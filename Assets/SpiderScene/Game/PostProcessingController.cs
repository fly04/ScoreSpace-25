using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{

    PostProcessVolume postProcessVolume;
    [SerializeField] bool isStopped = false;
    [SerializeField] GameController gameController;

    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        handleInputs();

        if (gameController.isPlaying)
        {
            if (isStopped)
            {
                //slowly increase the intensity of the vignette
                postProcessVolume.weight += 0.01f;
                //limit to 1
                if (postProcessVolume.weight > 1)
                {
                    postProcessVolume.weight = 1;
                }
            }
            else
            {
                postProcessVolume.weight = Mathf.Lerp(postProcessVolume.weight, 0, 0.1f);
            }
        }
        else
        {
            postProcessVolume.weight = Mathf.Lerp(postProcessVolume.weight, 0, 0.1f);
        }
    }

    void handleInputs()
    {
        if (Input.GetMouseButton(0))
        {
            isStopped = true;
        }
        else
        {
            isStopped = false;
        }
    }
}
