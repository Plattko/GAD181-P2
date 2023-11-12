using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform fill;
    private TextMeshProUGUI healthText;
    
    // Start is called before the first frame update
    void Awake()
    {
        fill = transform.GetChild(0).GetComponent<RectTransform>();
        healthText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // If health exceeds 100, change the health bar border to the broken sprite
    }

    public void SetHealth(int health, float startingHealth)
    {
        healthText.text = health.ToString("#,#");
        Debug.Log("Current health is " + health);
        float barLength = health / startingHealth;
        Debug.Log("Bar length is " + barLength);
        fill.localScale = new Vector3(barLength, 1f, 1f);
    }
}
