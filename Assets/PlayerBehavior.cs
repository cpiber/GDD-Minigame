using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float playerJumpForce = 5.0f;

    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;
    private int groundMask;
    private bool IsOnGround => Physics2D.IsTouchingLayers(collider, groundMask);
    private bool IsJumping = false;

    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<BoxCollider2D>();
        groundMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        var moveInput = Input.GetAxis("Horizontal");
        if (!collider.IsTouchingLayers() || IsOnGround)
            rigidbody.velocity = new Vector2(moveInput * playerSpeed, rigidbody.velocity.y);
        // Debug.Log($"Ground? {IsOnGround}  Jump? {IsJumping}  IsTouching? {collider.IsTouchingLayers()}");

        if (Input.GetButton("Jump") && IsOnGround && !IsJumping) {
            rigidbody.velocity += Vector2.up * playerJumpForce;
            IsJumping = true;
        }
        if (!IsOnGround) IsJumping = false;
    }
}
