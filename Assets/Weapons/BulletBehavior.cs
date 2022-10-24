using UnityEngine;
using UnityEngine.Events;

public class BulletBehavior : MonoBehaviour
{
    internal float speed;
    internal int damage;
    public UnityEvent<int> OnBulletCollision;

    void Start() {
        
    }

    void FixedUpdate() {
        transform.position += transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D collider) {
        OnBulletCollision.Invoke(damage);
        Destroy(gameObject.transform.parent.gameObject); // destroy entire prefab
    }
}
