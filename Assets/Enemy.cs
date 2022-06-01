using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public Collider2D enemyCollider;
    public int damage = 1;
    public float moveSpeed = 1f;
    public float detectDistance = 0.48f;

    Animator animator;
    Vector2 direction;
    GameObject target;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (target != null) {
            direction = target.transform.position - transform.position;
            if (Mathf.Abs(direction.x) < detectDistance && Mathf.Abs(direction.y) < detectDistance) {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            }
        }
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
