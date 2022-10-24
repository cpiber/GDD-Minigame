using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = .5f;
    [SerializeField] private int bulletDamage = 10;

    void Start() {
        
    }

    void FixedUpdate() {
        var mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Mathf.Atan2(mouseWorld.y - transform.position.y, mouseWorld.x - transform.position.x);
        if (transform.parent.localScale.x < 0) angle += Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
    }

    public void Shoot() {
        var rot = transform.rotation;
        if (transform.parent.localScale.x < 0) rot *= Quaternion.Euler(0, 0, 180); // add 180deg to offset scale=-x
        var forward = rot * Vector3.right; // "forward" in direction of rotation
        var bullet = Instantiate(bulletPrefab, transform.position + forward, rot);
        var b = bullet.GetComponentInChildren<BulletBehavior>();
        b.speed = bulletSpeed;
        b.damage = bulletDamage;
    }
}
