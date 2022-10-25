using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float playerJumpForce = 5.0f;
    private MovementBehavior movementB;

    [SerializeField] private GameObject gun;
    private GunBehavior gunB;

    void Start() {
        movementB = this.GetComponent<MovementBehavior>();
        gunB = gun.GetComponent<GunBehavior>();
    }

    void Update() {
        movementB.Move(Input.GetAxis("Horizontal"), playerSpeed);
        if (Input.GetButton("Jump")) movementB.Jump(playerJumpForce);

        var mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(mouseWorld.x - transform.position.x), transform.localScale.y, transform.localScale.z);

        if (Input.GetMouseButtonDown(0)) gunB.Shoot();
    }
}
