using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehavior : MonoBehaviour
{
    public void PlayGame() {
        var loader = SceneManager.LoadSceneAsync("GameScene");
        loader.completed += (op) => {
            var playerB = GameObject.Find("Player").GetComponent<PlayerBehavior>();
            // TODO: set parameters
        };
    }
}
