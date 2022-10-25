using UnityEngine;

public class LevelBehavior : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float enemyCooldown = 6.0f;
    [SerializeField] private float enemyCooldownScale = 0.8f;
    [SerializeField] private float enemyCooldownMin = 0.5f;
    [SerializeField] private uint numEnemies = 5;
    [SerializeField] private GameObject shrinkItemPrefab;
    [SerializeField] private float shrinkItemSpawnChance = 0.05f;

    private float lastSpawn;
    private CameraBehavior camB;
    private SceneBehavior sceneB;

    void Start() {
        camB = this.GetComponent<CameraBehavior>();
        sceneB = Camera.main.GetComponent<SceneBehavior>();
        lastSpawn = enemyCooldown - 0.1f;

        InvokeRepeating("MaybeSpawnShrinkItem", 0, 1);
    }

    void FixedUpdate() {
        lastSpawn += Time.deltaTime;
        if (lastSpawn < enemyCooldown) return;
        lastSpawn -= enemyCooldown;
        enemyCooldown *= enemyCooldownScale;
        if (enemyCooldown < enemyCooldownMin) enemyCooldown = enemyCooldownMin;
        
        for (int i = 0; i < numEnemies; ++i) SpawnEnemy();
    }

    public static GameObject GetPlayer() {
        GameObject player = null;
        var p = GameObject.Find("Player");
        for (int i = 0; i < p.transform.childCount; ++i) {
            player = p.transform.GetChild(i).gameObject;
            if (player.activeSelf) break;
        }
        return player;
    }

    void MaybeSpawnShrinkItem() {
        if (Random.Range(0f, 1f) < shrinkItemSpawnChance) SpawnShrinkItem();
    }

    void SpawnEnemy() {
        var spawnLeft = camB.player.transform.position.x >= camB.cameraRect.xMax - 20;
        // spawn space is either entire left side (just outside screen) or right side
        var spawnRect = new Vector2(spawnLeft ? camB.cameraRect.xMin - camB.cameraHalfSize.x : Camera.main.transform.position.x + camB.cameraHalfSize.x,
                                    spawnLeft ? Camera.main.transform.position.x - camB.cameraHalfSize.x : camB.cameraRect.xMax + camB.cameraHalfSize.x);
        var spawnPoint = new Vector3(Random.Range(spawnRect.x, spawnRect.y), 0, 0);
        var enemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);
        enemy.GetComponent<HealthBehavior>().onDeath.AddListener(sceneB.EnemyKilled);
    }

    void SpawnShrinkItem() {
        // spawn space is entire camera
        var spawnPoint = new Vector3(Random.Range(-camB.cameraHalfSize.x, camB.cameraHalfSize.x) + GetPlayer().transform.position.x, 0, 0);
        var enemy = Instantiate(shrinkItemPrefab, spawnPoint, Quaternion.identity);
        CancelInvoke();
    }
}
