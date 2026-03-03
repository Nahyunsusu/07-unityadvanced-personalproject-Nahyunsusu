using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Enemy Info")]
    [SerializeField] protected int _monsterLevel;
    public int MonsterLevel => _monsterLevel;
    [SerializeField] private GameObject _monsterPrefab;
    public GameObject MonsterPrefab => _monsterPrefab;

    [SerializeField] protected float _hp;
    [SerializeField] protected float _speed;
    [SerializeField] protected int   _score;

    public System.Action<Enemy> OnReturnPool;

    public virtual void Init(float hp, float speed)
    {
        _hp    = hp;
        _speed = speed;
    }

    protected virtual void Update()
    {
        if (transform.position.y < -6f)
        {
            ReturnToPool();
        }
    }

    protected void ReturnToPool()
    {
        gameObject.SetActive(false);
        OnReturnPool?.Invoke(this);
    }
}