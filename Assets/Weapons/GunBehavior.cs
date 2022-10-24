using UnityEngine;

public class GunBehavior : MonoBehaviour
{
    void Start() {
        
    }

    void Update() {
        var mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Mathf.Atan2(mouseWorld.y - transform.position.y, mouseWorld.x - transform.position.x);
        if (transform.parent.localScale.x < 0) angle += Mathf.PI;
        transform.rotation = Quaternion.Euler(0, 0, angle * 180 / Mathf.PI);
    }

    void Shoot() {

    }
}
