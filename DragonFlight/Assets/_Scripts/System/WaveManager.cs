using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyPool _pool;
    [SerializeField] private _baseCharacter _player;

    [Header("Spawn Settinig")]
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private float _laneWidth     = 9f;
    [SerializeField] private float _spawnY        = 10f;
    [SerializeField] private float _lifeTime      = 5f;

    [Header("Stage Config")]
    public int currentStage = 0;
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        Vector3 targetPos = transform.position;
        targetPos.z = transform.position.z + _player.CameraDelta.z;

        transform.position = targetPos;
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
                enemy.transform.position = new Vector3(xPos, _spawnY, transform.position.z + 20);

                enemy.Init(enemy.HP, 20f, _lifeTime);
                enemy.gameObject.SetActive(true);
            }
        }
    }   

    private int GetRandomLevelByStage()
    {
        return Random.Range(0, 4);
    }
    
    public void ResetWaveManager()
    {
        transform.position = _startPosition;

        StopAllCoroutines();

        if (_pool != null)
        {
            _pool.ReturnAllEnemies();
        }

        StartCoroutine(SpawnRoutine());
    }
}