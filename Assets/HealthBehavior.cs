using UnityEngine;
using UnityEngine.Events;

public class HealthBehavior : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] public GameObject objectToKill;
    public UnityEvent onDeath;

    void Start() {
        if (!objectToKill) objectToKill = gameObject;
    }

    public void Damage(int damage) {
        health -= damage;
        if (health > 0) return;
        onDeath.Invoke();
        Destroy(objectToKill);
    }
}
