using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Info
    [Header("Enemy Info")]
    [SerializeField] protected int _monsterLevel;
    public int MonsterLevel => _monsterLevel;

    // Stat
    [SerializeField] protected float _hp;
    private float _maxHp;
    public float HP => _hp;
    [SerializeField] protected float _speed;
    [SerializeField] protected int   _score;

    // 사망관리
    private float _currentLifeTime = 0f;
    private float _maxLifeTime     = 0f;
    private bool  _isDead          = false;

    // Collider
    private BoxCollider _boxCol;
    public BoxCollider BoxCol => _boxCol;

    public System.Action<Enemy> OnReturnPool;

    protected virtual void Start()
    {
        _boxCol = GetComponent<BoxCollider>();
    }   

    protected virtual void Update()
    {
        _currentLifeTime += Time.deltaTime;

        if (_currentLifeTime >= _maxLifeTime)
        {
            ReturnToPool();
        }
    }

    protected virtual void LateUpdate()
    {
        if (transform.position.y < -6f)
        {
            Die();
        }
    }

    private void OnDrawGizmos()
    {
        if (_boxCol == null) _boxCol = GetComponent<BoxCollider>();
        if (_boxCol == null) return;

        Gizmos.color = Color.green;

        Matrix4x4 rotationMatrix = transform.localToWorldMatrix;
        Gizmos.matrix = rotationMatrix;

        Gizmos.DrawWireCube(_boxCol.center, _boxCol.size);
    }

    /////////////////////////////////////////////

    public virtual void Init(float hp, float speed, float lifeTime)
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);

        _hp              = hp;
        _maxHp           = _hp;
        _speed           = speed;

        _maxLifeTime     = lifeTime;
        _currentLifeTime = 0f;
        _isDead          = false;
    }

    protected void ReturnToPool()
    {
        if (_isDead) return;

        _isDead = true;

        _hp = _maxHp;

        gameObject.SetActive(false);
        OnReturnPool?.Invoke(this);
    }

    public virtual void TakeDamage(float damage)
    {
        _hp -= damage;

        if(_hp <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        ReturnToPool();
    }
}