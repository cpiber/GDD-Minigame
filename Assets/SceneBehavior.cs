using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehavior : MonoBehaviour
{
    public void PlayGame(int selected) {
        var loader = SceneManager.LoadSceneAsync("GameScene");
        loader.completed += (op) => {
            var player = GameObject.Find("Player");
            for (int i = 0; i < player.transform.childCount; ++i) {
                player.transform.GetChild(i).gameObject.SetActive(i == selected);
            }
        };
    }
}
