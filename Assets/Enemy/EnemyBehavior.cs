using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 5.5f;
    [SerializeField] private float enemyJumpForce = 6.0f;
    private MovementBehavior movementB;

    void Start() {
        movementB = this.GetComponent<MovementBehavior>();
    }

    void Update() {
        // TODO: AI
    }
}
