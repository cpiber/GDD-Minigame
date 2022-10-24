using UnityEngine;

public class LevelBehavior : MonoBehaviour
{
    private new GameObject camera;
    private GameObject player;

    void Start() {
        camera = GameObject.Find("Main Camera");
        player = GameObject.Find("Player");
    }

    void Update() {
        
    }
}
