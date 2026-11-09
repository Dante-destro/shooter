using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Paneles de UI")]
    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public GameObject hudPanel;

    // --- NUEVO: Paneles para Menú Principal y Pausa ---
    [Header("Menús Nuevos")]
    public GameObject mainMenuPanel;
    //public GameObject pauseMenuPanel;

    private bool isGameOver = false;
    private bool isPaused = false; // --- NUEVO: Estado de pausa ---

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // --- MODIFICADO/AGREGADO: Si existe un Menú Principal, arranca pausado en el menú ---
        if (mainMenuPanel != null && mainMenuPanel.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (hudPanel) hudPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            if (victoryPanel) victoryPanel.SetActive(false);
            if (defeatPanel) defeatPanel.SetActive(false);
          /*  if (pauseMenuPanel) pauseMenuPanel.SetActive(false);*/ // --- NUEVO ---
            if (hudPanel) hudPanel.SetActive(true);
        }
    }

    // --- NUEVO: Detectar tecla Escape o P para pausar el juego ---
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
    //    {
    //        if (!isGameOver && (mainMenuPanel == null || !mainMenuPanel.activeSelf))
    //        {
    //            if (isPaused)
    //            {
    //                ResumeGame();
    //            }
    //            else
    //            {
    //                PauseGame();
    //            }
    //        }
    //    }
    //}

    // --- NUEVO: Método para iniciar el juego desde el Menú Principal ---
    public void StartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        isPaused = false;

        if (mainMenuPanel) mainMenuPanel.SetActive(false);
        //if (pauseMenuPanel) pauseMenuPanel.SetActive(false);
        if (victoryPanel) victoryPanel.SetActive(false);
        if (defeatPanel) defeatPanel.SetActive(false);
        if (hudPanel) hudPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // --- NUEVO: Método para Pausar ---
    //public void PauseGame()
    //{
    //    isPaused = true;
    //    Time.timeScale = 0f;
    //    if (pauseMenuPanel) pauseMenuPanel.SetActive(true);
    //    if (hudPanel) hudPanel.SetActive(false);

    //    Cursor.lockState = CursorLockMode.None;
    //    Cursor.visible = true;
    //}

    // --- NUEVO: Método para Reanudar desde la Pausa ---
    //public void ResumeGame()
    //{
    //    isPaused = false;
    //    Time.timeScale = 1f;
    //    if (pauseMenuPanel) pauseMenuPanel.SetActive(false);
    //    if (hudPanel) hudPanel.SetActive(true);

    //    Cursor.lockState = CursorLockMode.Locked;
    //    Cursor.visible = false;
    //}

    public void TriggerVictory()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (hudPanel) hudPanel.SetActive(false);
        if (victoryPanel) victoryPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }

    public void TriggerDefeat()
    {
        if (isGameOver) return;
        isGameOver = true;

        if (hudPanel) hudPanel.SetActive(false);
        if (defeatPanel) defeatPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
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