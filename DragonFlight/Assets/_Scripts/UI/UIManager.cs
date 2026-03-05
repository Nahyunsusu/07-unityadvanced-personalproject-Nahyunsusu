using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _statPanel;
    [SerializeField] private GameObject _inGamePanel;

    [Header("References")]
    [SerializeField] private WaveManager      _waveManager;
    [SerializeField] private _baseCharacter   _player;
    [SerializeField] private CameraController _cameraController;

    private void Awake()
    {
        Instance = this;

        ShowStartUI();
        _waveManager.enabled = false;
        Time.timeScale = 0;
    }

    /////////////////////////////////

    public void OnClickStart() // 메인화면 -> 스탯화면
    {
        _startPanel.SetActive(false);
         _statPanel.SetActive(true);
    }

    public void OnClickEnterStage() // 스탯화면 -> 게임화면
    {
         _statPanel.SetActive(false);
        _inGamePanel.SetActive(true);

        StartGame();
    }

    public void ShowStartUI()
    {
         _startPanel.SetActive(true);
          _statPanel.SetActive(false);
        _inGamePanel.SetActive(false);
    }

    public void StartGame()
    {
        _waveManager.enabled = true;

        Time.timeScale = 1f;
    }

    public void OnPlayerDie()
    {
        Time.timeScale = 0;
        _waveManager.enabled = false;

        _player.ResetPlayer();
        _cameraController.ResetCamera();
        _waveManager.ResetWaveManager();

        _inGamePanel.SetActive(false);
        _statPanel.SetActive(true);
    }

    ///////////// Muzzle  /////////////////
    public void OnMuzzlePlus()
    {
        _player.Dragon.PlusMuzzle();
    }

    public void OnMuzzleMinus()
    {
        _player.Dragon.MinusMuzzle();
    }

    ///////////// Damage  /////////////////

}