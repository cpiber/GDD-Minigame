using System.Collections;
using UnityEngine;

public class ShrinkItemBehavior : MonoBehaviour
{
    [SerializeField] private float shrinkTime = 0.5f;
    private GameObject player;

    void Start() {
        player = LevelBehavior.GetPlayer();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject != player) return;
        StartCoroutine(Shrink());
        // disable rendering and further collisions
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }

    IEnumerator Shrink() {
        var shrinkStart = Time.time;
        var startSize = player.transform.localScale;
        while (shrinkStart + shrinkTime >= Time.time) {
            player.transform.localScale = startSize * Mathf.Lerp(1f, 0.5f, (Time.time - shrinkStart) / shrinkTime);
            yield return null;
        }
        player.transform.localScale = startSize / 2;
        Destroy(gameObject);
    }
}
