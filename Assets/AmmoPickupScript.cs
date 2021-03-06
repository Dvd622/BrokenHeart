using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupScript : MonoBehaviour
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
            // Give player ammo
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null) {
                player.ammo += 3;
                player.audioSource.clip = player.pickupClip;
                player.audioSource.Play();
                Destroy(gameObject);
            }
        }
    }
}
