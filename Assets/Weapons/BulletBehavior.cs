using UnityEngine;
using UnityEngine.Events;

public class BulletBehavior : MonoBehaviour
{
    internal float speed;
    internal int damage;

    void Start() {
        
    }

    void FixedUpdate() {
        transform.position += transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        collider.gameObject.GetComponent<HealthBehavior>()?.Damage(damage);
        Destroy(gameObject);
    }
}
