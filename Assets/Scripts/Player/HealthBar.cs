using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform fill;
    private TextMeshProUGUI healthText;

    public Sprite healthBarBroken;
    
    // Start is called before the first frame update
    void Awake()
    {
        fill = transform.GetChild(0).GetComponent<RectTransform>();
        healthText = transform.GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    public void SetHealth(int health, float startingHealth)
    {
        healthText.text = health.ToString("#,#");
        Debug.Log("Current health is " + health);
        float barLength = health / startingHealth;
        Debug.Log("Bar length is " + barLength);
        fill.localScale = new Vector3(barLength, 1f, 1f);

        // If health exceeds 100, change the health bar border to the broken sprite
        if (barLength > 1)
        {
            BreakHealthBar();
        }
    }

    private void BreakHealthBar()
    {
        Image borderSprite = transform.GetChild(1).GetComponent<Image>();
        borderSprite.sprite = healthBarBroken;
    }
}
