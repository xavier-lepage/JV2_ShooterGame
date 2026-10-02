using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private InputAction quitAction;

    void Start()
    {
        quitAction = InputSystem.actions.FindAction("Quit");
    }

    void Update()
    {
        if (quitAction.WasPerformedThisFrame()) QuitGame();
    }

    private void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
