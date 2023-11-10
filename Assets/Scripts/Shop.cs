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
    private int dashCurrentCost;

    private int dmgStartingCost = 10;
    private int dashStartingCost = 100;

    private float dmgIncrease;
    private float dashIncrease;

    public TextMeshProUGUI dmgCostText;
    public TextMeshProUGUI dashCostText;
    
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        
        dmgCurrentCost = dmgStartingCost;
        dmgCostText.text = dmgStartingCost + " HP";
        dmgIncrease = playerController.startingAttackDamage * 0.20f;

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
            //float atkDMG = playerController.attackDamage;
            playerController.attackDamage += dmgIncrease;
            playerController.UpdateHealth(-dmgCurrentCost);

            float newCost = dmgCurrentCost * 1.5f;
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
            dashCostText.text = dashCurrentCost + " HP";

            Debug.Log("Dash unlocked");
        }
    }
}
