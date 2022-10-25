using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 5.5f;
    [SerializeField] private float enemyJumpForce = 6.0f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float jumpDistance = 5.0f;
    private MovementBehavior movementB;
    private new Rigidbody2D rigidbody;
    private GameObject player;

    void Start() {
        movementB = this.GetComponent<MovementBehavior>();
        rigidbody = this.GetComponent<Rigidbody2D>();
        player = LevelBehavior.GetPlayer();
        this.GetComponent<HealthBehavior>().onDeath.AddListener(() => Destroy(gameObject));
    }

    void Update() {
        movementB.Move(Mathf.Sign(player.transform.position.x - transform.position.x), enemySpeed);
        var dist = (player.transform.position - transform.position).sqrMagnitude;
        if (dist > jumpDistance) movementB.Jump(enemyJumpForce);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            collision.gameObject.GetComponent<HealthBehavior>().Damage(damage);
            rigidbody.AddForce(collision.GetContact(0).normal * 2);
        }
    }
}
