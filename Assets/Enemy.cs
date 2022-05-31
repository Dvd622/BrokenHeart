using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public Collider2D enemyCollider;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Health {
        set {
            health = value;
            if (health <= 0) {
                Defeated();
            }
        }
        get { return health; }
    }

    public float health = 10;

    public void Defeated() {
        animator.SetTrigger("Defeated");
    }

    public void RemoveEnemy() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            // Deal damage to enemy
            PlayerController player = other.GetComponent<PlayerController>();

            if (player != null) {
                player.health -= damage;
                print(player.health);
            }
        }
    }
}
