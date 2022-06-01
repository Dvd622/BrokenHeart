using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            // Give player health
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null) {
                player.health += 2;
                Destroy(gameObject);
            }
        }
    }
}
