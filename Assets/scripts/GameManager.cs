using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Paneles de UI")]
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject hudPanel;

    private bool isGameOver = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        Time.timeScale = 1f;
        if (victoryPanel) victoryPanel.SetActive(false);
        if (defeatPanel) defeatPanel.SetActive(false);
        if (hudPanel) hudPanel.SetActive(true);
    }

    public void TriggerVictory()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (hudPanel) hudPanel.SetActive(false);
        if (victoryPanel) victoryPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; // Pausa el juego
    }

    public void TriggerDefeat()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (hudPanel) hudPanel.SetActive(false);
        if (defeatPanel) defeatPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; // Pausa el juego
    }

    // Métodos para los botones del menú
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}