using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyPool _pool;

    [Header("Spawn Settinig")]
    [SerializeField] private float _spawnInterval = 3f;
    [SerializeField] private float _laneWidth     = 1.5f;
    [SerializeField] private float _spawnY        = 10f;

    [Header("Stage Config")]
    public int currentStage = 0;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while(true)
        {
            SpawnWave();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    private void SpawnWave()
    {
        for (int i = 0; i < 5; i++)
        {
            int level = GetRandomLevelByStage();

            Enemy enemy = _pool.PopFromPool(level);

            if(enemy != null)
            {
                float xPos = (i - 2) * _laneWidth;
                enemy.transform.position = new Vector3(xPos, _spawnY, 0);

                enemy.Init(10 * (level + 1), 5f);
                enemy.gameObject.SetActive(true);
            }
        }
    }

    private int GetRandomLevelByStage()
    {
        return Random.Range(0, 5);
    }
}