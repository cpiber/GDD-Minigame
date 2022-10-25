using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    private int obstacleLayer;
    private bool IsOnGround => ground != null;
    private GameObject ground = null;
    private bool IsJumping = false;
    private ContactPoint2D[] contacts = new ContactPoint2D[5];
    private new Rigidbody2D rigidbody;
    private new Collider2D collider;

    enum MoveType {
        No = 0,
        Left = 0x1,
        Right = 0x2,
    }

    void Start() {
        rigidbody = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<Collider2D>();
        obstacleLayer = LayerMask.NameToLayer("Obstacles");
    }

    void Update() {
        if (!IsOnGround) IsJumping = false;
    }

    public void Move(float moveInput, float speed) {
        var CanMove = ComputeMove();
        if ((CanMove & MoveType.Left) == 0 && moveInput < 0) moveInput = 0;
        else if ((CanMove & MoveType.Right) == 0 && moveInput > 0) moveInput = 0;
        rigidbody.velocity = new Vector2(moveInput * speed, rigidbody.velocity.y);
        // Debug.Log($"Ground? {IsOnGround}  Jump? {IsJumping}  CanMove? {CanMove.ToString("X")}  move={moveInput}");
    }

    public void Jump(float force) {
        if (IsOnGround && !IsJumping) {
            rigidbody.velocity += Vector2.up * force;
            IsJumping = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        for (int i = 0; i < collision.contactCount; ++i) {
            var o = collision.GetContact(i).collider.gameObject;
            var n = collision.GetContact(i).normal;
            // Debug.Log($"enter layer={o.layer} (obstacle={obstacleLayer}) with y={n.y} ({o.name})");
            if (o.layer == obstacleLayer && n.y >= 0.5) ground = o;
        }
    }

    void OnCollisionExit2D(Collision2D collision) {
        var o = collision.gameObject;
        // Debug.Log($"leave layer={o.layer} (obstacle={obstacleLayer}) ({o.name})");
        if (o == ground) ground = null;
    }

    private MoveType ComputeMove() {
        var move = MoveType.Left | MoveType.Right;
        var n = collider.GetContacts(contacts);
        if (n == contacts.Length) Debug.LogWarning($"ComputeMove: filled up collision buffer (length={n})");
        for (int i = 0; i < n; ++i) {
            if (contacts[i].normal.x <= -0.5) move &= ~(MoveType.Right); // remove possibility for right move
            else if (contacts[i].normal.x >= 0.5) move &= ~(MoveType.Left); // remove possibility for left move
        }
        return move;
    }
}
