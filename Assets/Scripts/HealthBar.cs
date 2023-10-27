using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform fill;
    
    // Start is called before the first frame update
    void Awake()
    {
        fill = transform.GetChild(0).GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // If health exceeds 100, change the health bar border to the broken sprite
    }

    public void SetHealth(int health, float startingHealth)
    {
        Debug.Log("Current health is " + health);
        float barLength = health / startingHealth;
        Debug.Log("Bar length is " + barLength);
        fill.localScale = new Vector3(barLength, 1f, 1f);
    }
}
