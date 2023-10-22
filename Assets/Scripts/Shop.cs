using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject shopNotification;
    public GameObject shopUI;
    public PlayerController playerController;

    private bool inFrontOfShop = false;

    private int dmgCurrentCost;
    private int moveCurrentCost;

    private int dmgStartingCost = 10;
    private int moveStartingCost = 10;

    private float dmgIncrease;
    private float moveSpeedIncrease;

    public TextMeshProUGUI dmgCostText;
    public TextMeshProUGUI moveCostText;
    
    // Start is called before the first frame update
    void Start()
    {
        dmgCurrentCost = dmgStartingCost;
        dmgCostText.text = dmgStartingCost + " HP";
        dmgIncrease = playerController.startingAttackDamage * 0.20f;

        moveCurrentCost = moveStartingCost;
        moveCostText.text = moveStartingCost + " HP";
        moveSpeedIncrease = playerController.startingMoveSpeed * 0.05f;
    }

    // Update is called once per frame
    void Update()
    {
        if (inFrontOfShop)
        {
            if (Input.GetKeyDown(KeyCode.F) && !shopUI.activeSelf)
            {
                shopUI.SetActive(true);
                playerController.isInShop = true;

            }
            else if (Input.GetKeyDown(KeyCode.F) && shopUI.activeSelf)
            {
                shopUI.SetActive(false);
                playerController.isInShop = false;
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

            Debug.Log("Attack damage is " + playerController.attackDamage);
        }
    }

    public void UpgradeMoveSpeed()
    {
        if (playerController.currentHealth > dmgCurrentCost)
        {
            //float moveSpeed = playerController.moveSpeed;
            playerController.moveSpeed += moveSpeedIncrease;
            playerController.UpdateHealth(-dmgCurrentCost);

            Debug.Log("Move speed is " + playerController.moveSpeed);
        }
    }
}
