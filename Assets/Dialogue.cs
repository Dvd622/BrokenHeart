using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.3f;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        gameObject.SetActive(false);
        // StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewLines(string[] newLines) {
        lines = new string[newLines.Length];
        lines = newLines;
    }

    public void Continue() {
        if (textComponent.text == lines[index]) {
            NextLine();
        } else {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }

    public void StartDialogue() {
        StopAllCoroutines();
        textComponent.text = string.Empty;
        gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine() {
        // Type each character 1 by 1
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        } else {
            gameObject.SetActive(false);
        }
    }
}
