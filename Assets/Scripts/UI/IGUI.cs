using UnityEngine;
using UnityEngine.UI;

public class IGUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Button pauseButton;
    [SerializeField] Button resumeButton;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject guiButtons;
    private void Awake()
    {
        pauseButton.onClick.AddListener(() => Pause());
        resumeButton.onClick.AddListener(() => Resume());
    }

    private void Pause()
    {
        pauseMenu.SetActive(true);
        guiButtons.SetActive(false);
        Time.timeScale = 0f;
    }

    private void Resume()
    {
        pauseMenu.SetActive(false);
        guiButtons.SetActive(true);
        Time.timeScale = 1f;
    }
}
