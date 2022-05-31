using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffest = 0.05f;
    public ContactFilter2D movementFilter;
    public BasicAttack basicAttack;
    public int health = 6;
    public int numOfHearts = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    int heartsToShow;

    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        heartsToShow = health / 2;
        for (int i = 0; i < hearts.Length; i++) {
            if (i < heartsToShow) {
                hearts[i].sprite = fullHeart;
            } else if (i == heartsToShow && health % 2 == 1) {
                hearts[i].sprite = halfHeart;
            } else {
                hearts[i].enabled = false;
            }
        }

        if (health <= 0) {
            Die();
        }

        if (canMove) {
            // If movement input is not 0, try to move
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success) {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success) {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                animator.SetBool("isMoving", success);
            } else {
                animator.SetBool("isMoving", false);
            }

            // Set direction of sprite to movement direction
            if (movementInput.x < 0) {
                spriteRenderer.flipX = true;
            } else if (movementInput.x > 0) {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction) {
        if (direction != Vector2.zero) {
            // Check for potential collisions
            int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffest);

            if (count == 0) {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            } else {
                return false;
            }
        } else {
            // Can't move if there's no direction to move in
            return false;
        }
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>(); 
    }

    void OnFire() {
        animator.SetTrigger("basicAttack");
    }

    public void BasicAttack() {
        if (spriteRenderer.flipX == true) {
            basicAttack.AttackLeft();
        } else {
            basicAttack.AttackRight();
        }
    }

    public void EndBasicAttack() {
        basicAttack.StopAttack();
    }

    public void LockMovement() {
        canMove = false;
    }

    public void UnlockMovement() {
        canMove = true;
    }

    public void Die() {
        Application.LoadLevel(Application.loadedLevel);
    }
}
