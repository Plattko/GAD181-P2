using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public GameObject witchUI;
    public GameObject shopUI;

    private int dialogueInstance = 0;
    private int textIndex = 0;
    private bool dialogueFinished;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //StartCoroutine("Ah, a new traveller...", 5f);

        dialogueFinished = false;
        dialogueInstance += 1;
        Debug.Log("Dialogue instance is: " + dialogueInstance);
        textIndex = 0;
        dialogueBox.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !dialogueFinished)
        {
            textIndex += 1;
            Debug.Log("Text index is: " + textIndex);
        }
        
        if (dialogueInstance == 1)
        {
            if (textIndex == 0)
            {
                dialogueText.text = "Ah, a new traveller...";
            }
            else if (textIndex == 1)
            {
                dialogueText.text = "You've seen the dangers of the forest, I presume.";
            }
            else if (textIndex == 2)
            {
                dialogueText.text = "I can make you stronger, for a small price.";
            }
            else if (textIndex == 3)
            {
                dialogueText.text = "Allow me a small portion of your life force... and I shall weave new powers for you to control.";
            }
            else if(textIndex == 4)
            {
                dialogueFinished = true;
                dialogueBox.SetActive(false);
                shopUI.SetActive(true);
                dialogueText.text = "Welcome back, traveler...";
            }
        }
        else if (dialogueInstance >= 2)
        {
            //if (textIndex == 0)
            //{
            //    dialogueText.text = "Welcome back, traveler...";
            //}
            if (textIndex >= 1)
            {
                dialogueFinished = true;
                dialogueBox.SetActive(false);
                shopUI.SetActive(true);
            }
        }
    }

    //IEnumerator TypeWriter(string sentence, float time)
    //{
    //    dialogueFinished = false;

    //    string currentText = "";
    //    List<char> chars = new List<char>(sentence);

    //    foreach (char letter in chars)
    //    {
    //        currentText += letter;
    //        dialogueText.text = currentText;
    //        yield return new WaitForSeconds(time);
    //    }

    //    dialogueFinished = true;
    //}
}
