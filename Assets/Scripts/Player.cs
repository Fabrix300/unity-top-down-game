using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;

    private Vector2 moveDelta;
    private RaycastHit2D hit;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (!boxCollider) Debug.LogWarning("There's no boxCollider2D");
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!spriteRenderer) Debug.LogWarning("There's no spriteRenderer");
    }

    private void FixedUpdate()
    {
        // Reset moveDelta
        moveDelta = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        // Swap sprite direction, wether you're going right or left
        if (moveDelta.x > 0)
            spriteRenderer.flipX = false;
        else if (moveDelta.x < 0)
            spriteRenderer.flipX = true;

        // Make this thing move
        // transform.Translate(moveDelta * Time.deltaTime * playerSpeed);

        // Make sure we can move in this direction, by casting a box there first. If the box returns null, we're free to move
        // Debug.DrawRay(transform.position, new Vector2(moveDelta.x, 0), Color.red);
        // hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Character", "Blocking"));
        hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(moveDelta.x, 0), .1f, LayerMask.GetMask("Character", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime * playerSpeed, 0f, 0f);
        }

        // Debug.DrawRay(transform.position, new Vector2(0, moveDelta.y), Color.blue);
        // hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Character", "Blocking"));
        hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, new Vector2(0, moveDelta.y), .1f, LayerMask.GetMask("Character", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0f, moveDelta.y * Time.deltaTime * playerSpeed, 0f);
        }
    }
}
