using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneBehavior : MonoBehaviour
{
    [SerializeField] private GameObject gameScore;
    private TMP_Text gameScoreText;
    private int score;

    void Start() {
        if (gameScore) {
            gameScoreText = gameScore.GetComponent<TMP_Text>();
            DisplayScore();
        }
    }

    public void PlayGame(int selected) {
        var loader = SceneManager.LoadSceneAsync("GameScene");
        loader.completed += (op) => {
            var player = GameObject.Find("Player");
            for (int i = 0; i < player.transform.childCount; ++i) {
                player.transform.GetChild(i).gameObject.SetActive(i == selected);
            }
        };
    }

    public void EndGame() {
        var loader = SceneManager.LoadSceneAsync("TitleScene");
        loader.completed += (op) => {
            GameObject.Find("IntroText").GetComponent<TMP_Text>().text = $"You lost!\nEnemies killed: {score}";
        };
    }

    public void EnemyKilled() {
        score += 1;
        DisplayScore();
    }

    void DisplayScore() {
        gameScoreText.text = $"Score: {score}";
    }
}
