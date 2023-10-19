using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuScreen : MonoBehaviour
{
    private UIDocument _uiDocument;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        var root = _uiDocument.rootVisualElement;

        root.Q<Button>("btn-start").RegisterCallback<ClickEvent>(HandleGameStart);
        root.Q<Button>("btn-exit").RegisterCallback<ClickEvent>(HandleExitGame);
    }

    private void HandleGameStart(ClickEvent evt)
    {
        SceneManager.LoadScene(SceneList.Game);
    }

    private void HandleExitGame(ClickEvent evt)
    {
        Application.Quit();
    }

}