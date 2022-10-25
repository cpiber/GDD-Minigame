using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float playerJumpForce = 5.0f;

    private new Rigidbody2D rigidbody;
    private new BoxCollider2D collider;
    private int obstacleLayer;
    private bool IsOnGround => ground != null;
    private GameObject ground = null;
    private bool IsJumping = false;
    private ContactPoint2D[] contacts = new ContactPoint2D[5];

    [SerializeField] private GameObject gun;
    private GunBehavior gunB;

    enum MoveType {
        No = 0,
        Left = 0x1,
        Right = 0x2,
    }

    void Start() {
        rigidbody = this.GetComponent<Rigidbody2D>();
        collider = this.GetComponent<BoxCollider2D>();
        obstacleLayer = LayerMask.NameToLayer("Obstacles");
        gunB = gun.GetComponent<GunBehavior>();
    }

    void Update() {
        var moveInput = Input.GetAxis("Horizontal");
        var CanMove = ComputeMove();
        if ((CanMove & MoveType.Left) == 0 && moveInput < 0) moveInput = 0;
        else if ((CanMove & MoveType.Right) == 0 && moveInput > 0) moveInput = 0;
        rigidbody.velocity = new Vector2(moveInput * playerSpeed, rigidbody.velocity.y);
        // Debug.Log($"Ground? {IsOnGround}  Jump? {IsJumping}  CanMove? {CanMove.ToString("X")}  move={moveInput}");

        if (Input.GetButton("Jump") && IsOnGround && !IsJumping) {
            rigidbody.velocity += Vector2.up * playerJumpForce;
            IsJumping = true;
        }
        if (!IsOnGround) IsJumping = false;

        var mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(mouseWorld.x - transform.position.x), transform.localScale.y, transform.localScale.z);

        if (Input.GetMouseButtonDown(0)) gunB.Shoot();
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
            else if (contacts[i].normal.x >= -0.5) move &= ~(MoveType.Left); // remove possibility for left move
        }
        return move;
    }
}
