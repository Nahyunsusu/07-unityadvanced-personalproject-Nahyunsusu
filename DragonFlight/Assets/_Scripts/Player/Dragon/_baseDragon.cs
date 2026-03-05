using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

enum BulletType
{
    Straight,
    Wide,
    Guided
}

public class _baseDragon : MonoBehaviour
{
    // Status
    protected int LV;
    protected float _speed;
    protected int _damage = 5;
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; UpdateUI(); }
    }

    // Projectile
    protected Muzzle[] _muzzles;
    protected int      _muzzleCount;
    public int MuzzleCount 
    {
        get { return _muzzleCount; }
        set { _muzzleCount = value; UpdateUI(); }
    }

    private float  _fireRate = 0.2f;
    public float FireRate
    {
        get { return _fireRate; }
        set { _fireRate = value; UpdateUI(); }
    }
    private float _lastFireTime;

    private bool _isSpread = false;
    public bool IsSpread
    {
        get { return _isSpread; }
        set { _isSpread = value; UpdateUI(); }
    }

    private bool isHoming = false;
    public bool IsHoming
    {
        get { return isHoming; }
        set { isHoming = value; UpdateUI(); }
    }

    // Collider
    private BoxCollider _boxCol;
    public BoxCollider BoxCol => _boxCol;

    // UI
    [SerializeField] private TextMeshProUGUI _muzzleText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _fireRateText;
    [SerializeField] private TextMeshProUGUI _isSpreadText;
    [SerializeField] private TextMeshProUGUI _isHomingText;

    private void Awake()
    {
        _muzzles = GetComponentsInChildren<Muzzle>();
        _muzzleCount = 1;
    }

    private void Start()
    {
        _boxCol = GetComponent<BoxCollider>();
        UpdateUI();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        bool holdingJ = Keyboard.current.jKey.isPressed;

        if (holdingJ && Time.time >= _lastFireTime + _fireRate)
        {
            Shoot();
            _lastFireTime = Time.time; 
        }
    }

    private void Shoot()
    {
        int bulletIndex = 0;

        if      (IsSpread) { bulletIndex = 1; }
        else if (IsHoming) { bulletIndex = 2; }

        for (int i = 0; i < _muzzleCount; i++)
        {
            if (IsSpread && _muzzleCount > 1)
            {
                float angle = Mathf.Lerp(-20f, 20f, (float)i / (_muzzleCount - 1));
                _muzzles[i].transform.localRotation = Quaternion.Euler(0, angle, 0);
            }
            else
            {
                _muzzles[i].transform.localRotation = Quaternion.identity;
            }

            _muzzles[i].LoadBullet(_damage, bulletIndex);
        }
    }

    /////////// UI ///////////////
    private void UpdateUI ()
    {
        if (_muzzleText   != null) _muzzleText.text   = $"{MuzzleCount}";
        if (_damageText   != null) _damageText.text   = $"{Damage}";
        if (_fireRateText != null) _fireRateText.text = FireRate.ToString("F2");
        if (_fireRateText != null)
        { 
            if(IsSpread) _isSpreadText.text = "ON";
            else _isSpreadText.text = "OFF";
        }
        if (_isHomingText != null)
        {
            if (isHoming) _isHomingText.text = "ON";
            else _isHomingText.text = "OFF";
        }
    }
    public void PlusMuzzle()
    {
        if (MuzzleCount < _muzzles.Length)
        {
            MuzzleCount++;
            Debug.Log($"총구 추가! 현재 총구 개수: {MuzzleCount}");
        }
    }

    public void MinusMuzzle()
    {
        if (MuzzleCount > 1)
        {
            MuzzleCount--;
            Debug.Log($"총구 제거! 현재 총구 개수: {MuzzleCount}");
        }
    }

    public void PlusDamage()
    {
        Damage += 5;
        Debug.Log("PlusDamage 실행완료");
    }

    public void MinusDamage()
    {
        Damage -= 5;
        Debug.Log("MinusDamage 실행완료");
    }
    public void MinusFireRate()
    {
        FireRate -= 0.05f;
        FireRate = Mathf.Clamp(_fireRate, 0.05f, 2.0f);

        Debug.Log($"현재 발사 간격: {FireRate}");
    }

    public void PlusFireRate()
    {
        FireRate += 0.05f;
        FireRate = Mathf.Clamp(_fireRate, 0.05f, 2.0f);

        Debug.Log($"현재 발사 간격: {FireRate}");
    }

    public void OnSpreadButton()
    {
        IsSpread = true;
        Debug.Log("Spread 활성화");
    }
    public void OffSpreadButton()
    {
        IsSpread = false;
        Debug.Log("Spread 비활성화");
    }

    public void OnHomingButton()
    {
        IsHoming = true;
        Debug.Log("Homing 활성화");
    }
    public void OffHomingButton()
    {
        IsHoming = false;
        Debug.Log("Homing 비활성화");
    }
}