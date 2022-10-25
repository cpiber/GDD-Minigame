using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject wallLeft;
    [SerializeField] private GameObject wallRight;

    private new Camera camera;
    private GameObject player;
    private float z;
    private float minY;
    private float minX;
    private float maxX;

    void Start() {
        camera = this.GetComponent<Camera>();
        var p = GameObject.Find("Player");
        for (int i = 0; i < p.transform.childCount; ++i) {
            player = p.transform.GetChild(i).gameObject;
            if (player.activeSelf) break;
        }
        z = camera.transform.position.z;

        var zz = ground.transform.position.z - z;
        var bl = camera.ViewportToWorldPoint(new Vector3(0, 0, zz));
        var tr = camera.ViewportToWorldPoint(new Vector3(1, 1, zz));
        var c = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, zz));
        minY = ground.transform.position.y + c.y - bl.y;
        minX = wallLeft.transform.position.x + c.x - bl.x;
        maxX = wallRight.transform.position.x - (tr.x - c.x);
    }

    void FixedUpdate() {
        camera.transform.position = calculateCamPosition();
        // camera.transform.LookAt(player.transform);
    }

    private Vector3 calculateCamPosition() {
        Vector3 focus = player.transform.position + Vector3.forward * z;
        focus.x = Mathf.Clamp(focus.x, minX, maxX);
        focus.y = Mathf.Max(focus.y, minY);
        // Debug.Log($"{focus} [{minX},{maxX}] {minY}");
        return focus;
    }
}
