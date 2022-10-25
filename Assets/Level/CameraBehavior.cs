using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private GameObject ground;
    [SerializeField] private GameObject wallLeft;
    [SerializeField] private GameObject wallRight;

    private new Camera camera;
    public GameObject player;
    private float z;
    public Rect cameraRect;
    public Vector2 cameraSize;

    void Start() {
        camera = this.GetComponent<Camera>();
        player = LevelBehavior.GetPlayer();
        z = camera.transform.position.z;

        var zz = ground.transform.position.z - z;
        var bl = camera.ViewportToWorldPoint(new Vector3(0, 0, zz));
        var c = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, zz));
        cameraSize = new Vector2(c.x - bl.x, c.y - bl.y);
        cameraRect = Rect.MinMaxRect(wallLeft.transform.position.x + cameraSize.x, ground.transform.position.y + cameraSize.y,
                                     wallRight.transform.position.x - cameraSize.x, float.MaxValue);
    }

    void FixedUpdate() {
        camera.transform.position = calculateCamPosition();
        // camera.transform.LookAt(player.transform);
    }

    private Vector3 calculateCamPosition() {
        Vector3 focus = player.transform.position + Vector3.forward * z;
        focus.x = Mathf.Clamp(focus.x, cameraRect.xMin, cameraRect.xMax);
        focus.y = Mathf.Max(focus.y, cameraRect.yMin);
        // Debug.Log($"{focus} {cameraRect}");
        return focus;
    }
}
