using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy[] _monsterOriginals;

    private Dictionary<int, Stack<Enemy>> _pools = new Dictionary<int, Stack<Enemy>>();

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            _pools[i] = new Stack<Enemy>(20);
        }
    }

    private Enemy CreateEnemy(int level)
    {
        if (level < 0 || level >= _monsterOriginals.Length) return null;

        Enemy newEnemy = Instantiate(_monsterOriginals[level]);

        newEnemy.OnReturnPool = PushToPool;

        newEnemy.gameObject.SetActive(false);

        return newEnemy;
    }

    public void PushToPool(Enemy enemy)
    {
        int level = enemy.MonsterLevel;
        if (_pools.ContainsKey(level))
        {
            _pools[level].Push(enemy);
        }
    }

    public Enemy PopFromPool(int level)
    {
        if (_pools.ContainsKey(level) && _pools[level].Count > 0)
        {
            return _pools[level].Pop();
        }

        return CreateEnemy(level);
    }
} 