using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject shopNotification;
    public GameObject shopUI;
    public Transform player;
    private PlayerController playerController;

    private bool inFrontOfShop = false;

    private int dmgCurrentCost;
    private int healthReturnCurrentCost;
    private int potionPotencyCurrentCost;
    private int dashCurrentCost;

    private int dmgStartingCost = 10;
    private int healthReturnStartingCost = 25;
    private int potionPotencyStartingCost = 25;
    private int dashStartingCost = 100;

    private float dmgIncrease;
    private float healthReturnIncrease = 0.1f;
    private int potionPotencyIncrease = 5;
    private float dashIncrease;

    public TextMeshProUGUI dmgCostText;
    public TextMeshProUGUI healthReturnCostText;
    public TextMeshProUGUI potionPotencyCostText;
    public TextMeshProUGUI dashCostText;

    public static List<GameObject> enemies = new List<GameObject>();
    private int enemyHealthIncrease = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        
        dmgCurrentCost = dmgStartingCost;
        dmgCostText.text = dmgStartingCost + " HP";
        dmgIncrease = playerController.startingAttackDamage * 0.20f;
        
        healthReturnCurrentCost = healthReturnStartingCost;
        healthReturnCostText.text = healthReturnStartingCost + " HP";

        potionPotencyCurrentCost = potionPotencyStartingCost;
        potionPotencyCostText.text = potionPotencyStartingCost + " HP";

        dashCurrentCost = dashStartingCost;
        dashCostText.text = dashStartingCost + " HP";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && shopUI.activeSelf)
        {
            shopUI.SetActive(false);
            playerController.isInShop = false;
        }
        else if (inFrontOfShop)
        {
            //Debug.Log("Is in front of shop");
            if (Input.GetKeyDown(KeyCode.F) && !shopUI.activeSelf)
            {
                shopUI.SetActive(true);
                playerController.isInShop = true;
            }
        }

        if (playerController.healthReturn >= 1f)
        {
            healthReturnCostText.text = "MAX LEVEL";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopNotification.SetActive(true);
            inFrontOfShop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shopNotification.SetActive(false);
            inFrontOfShop = false;
        }
    }

    public void UpgradeDMG()
    {
        if (playerController.currentHealth > dmgCurrentCost)
        {
            playerController.attackDamage += dmgIncrease;
            playerController.UpdateHealth(-dmgCurrentCost);

            float newCost = dmgCurrentCost + (dmgCurrentCost * 0.1f);
            dmgCurrentCost = Mathf.RoundToInt(newCost);
            dmgCostText.text = dmgCurrentCost + " HP";

            Debug.Log("Attack damage is " + playerController.attackDamage);
        }
    }

    public void UpgradeDash()
    {
        int purchaseNumber = 0;
        
        if (playerController.currentHealth > dashCurrentCost && purchaseNumber == 0)
        {
            player.GetComponent<Dash>().enabled = true;
            playerController.UpdateHealth(-dashCurrentCost);

            float newCost = 250f;
            dashCurrentCost = Mathf.RoundToInt(newCost);
            dashCostText.text = "MAX LEVEL";

            Debug.Log("Dash unlocked");
        }
    }

    public void UpgradeHealthReturn()
    {
        if (playerController.currentHealth > healthReturnCurrentCost && playerController.healthReturn != 1f)
        {
            playerController.healthReturn += healthReturnIncrease;
            playerController.UpdateHealth(-healthReturnCurrentCost);

            float newCost = healthReturnCurrentCost + (healthReturnCurrentCost * 0.1f);
            healthReturnCurrentCost = Mathf.RoundToInt(newCost);
            healthReturnCostText.text = healthReturnCurrentCost + " HP";

            Debug.Log("Health return is: " + playerController.healthReturn);
        }
    }

    public void UpgradePotionPotency()
    {
        if (playerController.currentHealth > potionPotencyCurrentCost)
        {
            playerController.potionPotency += potionPotencyIncrease;
            playerController.UpdateHealth(-potionPotencyCurrentCost);

            float newCost = potionPotencyCurrentCost + potionPotencyStartingCost;
            potionPotencyCurrentCost = Mathf.RoundToInt(newCost);
            potionPotencyCostText.text = potionPotencyCurrentCost + " HP";
            
            Debug.Log("The number of enemies is: " + enemies.Count);

            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().startingHealth += enemyHealthIncrease;
                Debug.Log("Enemy starting health is: " + enemy.GetComponent<Enemy>().startingHealth);
            }

            Debug.Log("Potion potency is: " + playerController.potionPotency);
        }
    }
}
