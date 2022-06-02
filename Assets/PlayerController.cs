using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffest = 0.05f;
    public ContactFilter2D movementFilter;
    public BasicAttack basicAttack;
    public int maxHealth = 6;
    public int health = 6;
    public int numOfHearts = 3;
    public int ammo = 3;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Dialogue dialogue;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Image ammoUI;
    public Sprite ammo3;
    public Sprite ammo2;
    public Sprite ammo1;
    public Sprite ammo0;
    int heartsToShow;

    private bool Facing_Right = true;

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
        for (int i = 0; i < maxHealth/2; i++) {
            if (i < heartsToShow) {
                hearts[i].sprite = fullHeart;
                hearts[i].enabled = true;
            } else if (i == heartsToShow && health % 2 == 1) {
                hearts[i].sprite = halfHeart;
                hearts[i].enabled = true;
            } else {
                hearts[i].enabled = false;
            }
        }

        if (ammo>3) {
            ammo = 3;
        } else if (ammo<0) {
            ammo = 0;
        }

        if (ammo==3) {
            ammoUI.sprite = ammo3;
        } else if (ammo==2) {
            ammoUI.sprite = ammo2;
        } else if (ammo==1) {
            ammoUI.sprite = ammo1;
        } else if (ammo<=0) {
            ammoUI.sprite = ammo0;
        }

        if (health <= 0) {
            animator.SetTrigger("dead");
        } else if (health > maxHealth) {
            health = maxHealth;
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
            if (movementInput.x < 0 && Facing_Right) {
                Flip();
            } else if (movementInput.x > 0 && !Facing_Right) {
                Flip();
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

    private void Flip() {
        Facing_Right = !Facing_Right;
        transform.Rotate(0f, 180f, 0f);
    }

    void OnMove(InputValue movementValue) {
        movementInput = movementValue.Get<Vector2>(); 
    }

    void OnFire() {
        // if there is dialogue
        if (dialogue.gameObject.activeSelf) {
            dialogue.Continue();
        } else {
            animator.SetTrigger("basicAttack");
        }
    }

    void OnFire2() {
        if (ammo>0) {
            ammo -= 1;
            animator.SetTrigger("shoot");
            Shoot();
        }
    }

    void OnInteract() {
        print("Interact");
    }

    public void BasicAttack() {
        basicAttack.Attack();
        print("attack");
        /*
        if (spriteRenderer.flipX == true) {
            basicAttack.AttackLeft();
        } else {
            basicAttack.AttackRight();
        }
        */
    }

    public void EndBasicAttack() {
        basicAttack.StopAttack();
    }

    public void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
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
