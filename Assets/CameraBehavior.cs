using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    [SerializeField] private float minY = 10f;

    private new Camera camera;
    private GameObject player;
    private float z;

    void Start()
    {
        camera = this.GetComponent<Camera>();
        player = GameObject.Find("Player");
        z = camera.transform.position.z;
    }

    void Update()
    {
        camera.transform.position = calculateCamPosition();
        // camera.transform.LookAt(player.transform);
    }

    private Vector3 calculateCamPosition() {
        Vector3 focus = player.transform.position + Vector3.forward * z;
        focus.y = Mathf.Max(focus.y, minY);
        return focus;
    }
}
