using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Panels")]
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _statPanel;
    [SerializeField] private GameObject _inGamePanel;

    [Header("References")]
    [SerializeField] private WaveManager _waveManager;

    private void Awake()
    {
        ShowStartUI();

        _waveManager.enabled = false;
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
}
