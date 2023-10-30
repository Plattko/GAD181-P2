using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringSpotlight : MonoBehaviour
{
    [Header("Visual Variation Variables")]
    public float minIntensity = 1f; 
    public float maxIntensity = 2f; 
    public float flickerSpeed = 1f; 

    private Light2D spotlight;
    private float originalIntensity;

    void Start()
    {
        spotlight = GetComponent<Light2D>();

        //stores the original intensity of the spotlight.
        originalIntensity = spotlight.intensity;

        //start the light flickering.
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            //creates a random intesnity of light withing a specified range.
            float randomIntensity = Random.Range(minIntensity, maxIntensity);
            spotlight.intensity = randomIntensity;

            //wait for a short time before flickering again.
            yield return new WaitForSeconds(1f / flickerSpeed);
        }
    }

    void OnDestroy()
    {
        //resets the spotlight intensity to its original value when the object is destroyed
        spotlight.intensity = originalIntensity;
    }
}
