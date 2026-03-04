using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField] protected bool  _isLaunched;
    [SerializeField] protected float _activeTime    = 0;
    [SerializeField] protected float _maxActiveTime = 3;
    [SerializeField] protected int   _speed         = 30;
    [SerializeField] protected int   _damage;

    [SerializeField] protected Vector3 _rotationAngle = new Vector3(0, 0, 500);

    private SphereCollider _col;

    public bool ActiveInHierarchy => gameObject.activeInHierarchy;

    private void Start()
    {
        _col = GetComponent<SphereCollider>();
    }

    protected virtual void Update()
    {
        if (!_isLaunched) return;

        Move();

        _activeTime += Time.deltaTime;

        if (_activeTime >= _maxActiveTime)
        {
            ReturnToPool();
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(_damage);
            }
            Debug.Log("충돌");
            ReturnToPool();
        }
    }

    private void OnDrawGizmos()
    {
        if (_col == null) _col = GetComponent<SphereCollider>();
        if (_col == null) return;

        Gizmos.color = Color.cyan;

        Vector3 center = transform.TransformPoint(_col.center);

        float lossyScale = Mathf.Max(transform.lossyScale.x, Mathf.Max(transform.lossyScale.y, transform.lossyScale.z));
        float radius = _col.radius * lossyScale;

        Gizmos.DrawWireSphere(center, radius);
    }

    public virtual void Launch()
    {
        _isLaunched = true;
        _activeTime = 0;

        if (_col == null) _col = GetComponent<SphereCollider>();
        if (_col != null) _col.isTrigger = true;
    }

    protected abstract void Move();

    protected void ReturnToPool()
    {
        _isLaunched = false;
        gameObject.SetActive(false);
    }

    public void SetActive(bool setActive)
    {
        gameObject.SetActive(setActive);

        if (!setActive)
        {
            _isLaunched = false;
            _activeTime = 0;
        }
    }
}
