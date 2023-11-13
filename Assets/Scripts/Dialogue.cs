using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    private int textIndex = 0;
    private bool dialogueFinished;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        textIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TypeWriter(string sentence, float time)
    {
        string currentText = "";
        dialogueFinished = false;
        List<char> charArray = sentence.ToCharArray();

        foreach (char letter in charArray)
        {
            currentText += letter;
            dialogueText.text = currentText;
            yield return new WaitForSeconds(time);
        }

        dialogueFinished = true;
    }
}
