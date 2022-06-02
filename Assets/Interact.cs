using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Collider2D interactCollider;
    public string[] lines;
    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "NPC") {
            // Get dialogue lines from NPC
            NPC npc = other.GetComponent<NPC>();
            if (npc != null) {
                dialogue.lines = new string[npc.lines.Length];
                dialogue.lines = npc.lines; 
                dialogue.StartDialogue();
            }
        }
    }
}
