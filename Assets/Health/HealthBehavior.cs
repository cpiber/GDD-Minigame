using UnityEngine;
using UnityEngine.Events;

public class HealthBehavior : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] public GameObject objectToKill;
    [SerializeField] public GameObject healthPrefab;
    private GameObject healthBar;
    private float healthPercent;
    public UnityEvent onDeath;

    void Start() {
        if (!objectToKill) objectToKill = gameObject;
        var bg = Instantiate(healthPrefab);
        bg.transform.parent = transform;
        bg.transform.localPosition = bg.transform.position;
        bg.GetComponent<SpriteRenderer>().color = Color.white;
        healthBar = Instantiate(healthPrefab);
        healthBar.transform.parent = transform;
        healthBar.transform.localPosition = new Vector3(healthBar.transform.position.x, healthBar.transform.position.y, healthBar.transform.position.z - 0.1f);
        healthPercent = healthBar.transform.localScale.x / health;
    }

    public void Damage(int damage) {
        health -= damage;
        healthBar.transform.localScale = new Vector3(healthPercent * health, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        if (health > 0) return;
        onDeath.Invoke();
        Destroy(objectToKill);
    }
}
