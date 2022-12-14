using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = .5f;
    [SerializeField] private int bulletDamage = 10;
    [SerializeField] private float cooldown = 0.1f;
    private float activeCooldown = 0;

    static float halfAngle = Mathf.PI / 2 - 0.1f;

    void FixedUpdate() {
        var mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Mathf.Atan2(mouseWorld.y - transform.position.y, mouseWorld.x - transform.position.x);
        if (transform.parent.localScale.x < 0) angle += Mathf.PI;
        if (angle >= Mathf.PI) angle -= 2 * Mathf.PI; // make sure we are always in range [-pi,pi]
        angle = Mathf.Clamp(angle, -halfAngle, halfAngle);
        transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);

        activeCooldown = Mathf.Max(0, activeCooldown - Time.deltaTime);
    }

    public void Shoot() {
        if (activeCooldown > 0.0f) return;
        var rot = transform.rotation;
        if (transform.parent.localScale.x < 0) rot *= Quaternion.Euler(0, 0, 180); // add 180deg to offset scale=-x
        var bullet = Instantiate(bulletPrefab, transform.position, rot);
        var b = bullet.GetComponent<BulletBehavior>();
        b.speed = bulletSpeed;
        b.damage = bulletDamage;
        activeCooldown = cooldown;
    }
}
