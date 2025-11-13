using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] Button playButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button backToMenuButton;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;

    [Header("Scenes")]
    [SerializeField] SceneAsset levelToLoad;

    private void Awake()
    {
        //Initiate menus
        EnableMainMenu();

        playButton.onClick.AddListener(() => SceneManager.LoadScene(levelToLoad.name));
        optionsButton.onClick.AddListener(() => EnableOptions());
        quitButton.onClick.AddListener(() => ExitGame());
        backToMenuButton.onClick.AddListener(() => EnableMainMenu());
    }

    private void EnableOptions()
    {
        optionsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    private void EnableMainMenu()
    {
        optionsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
