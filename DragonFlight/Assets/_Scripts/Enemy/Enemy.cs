using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Info
    [Header("Enemy Info")]
    [SerializeField] protected int _monsterLevel;
    public int MonsterLevel => _monsterLevel;

    // Stat
    [SerializeField] protected float _hp;
    public float HP => _hp;
    [SerializeField] protected float _speed;
    [SerializeField] protected int   _score;

    // Collider
    private BoxCollider _boxCol;
    public BoxCollider BoxCol => _boxCol;

    public System.Action<Enemy> OnReturnPool;

    private void Start()
    {
        _boxCol = GetComponent<BoxCollider>();
    }   

    protected virtual void Update()
    {
    }

    protected virtual void LateUpdate()
    {
        if (transform.position.y < -6f)
        {
            ReturnToPool();
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

    public virtual void Init(float hp, float speed)
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);

        _hp = hp;
        _speed = speed;
    }

    protected void ReturnToPool()
    {
        gameObject.SetActive(false);
        OnReturnPool?.Invoke(this);
    }
}