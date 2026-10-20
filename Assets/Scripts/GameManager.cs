using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UnityEvent onLevelComplete;
    private SpawnManager spawnManager;
    private InputAction quitAction;

    void Start()
    {
        quitAction = InputSystem.actions.FindAction("Quit");
        spawnManager = GetComponent<SpawnManager>();
    }

    void Update()
    {
        if (quitAction.WasPerformedThisFrame()) QuitGame();
        if (spawnManager.IsLevelComplete())
        {
            onLevelComplete.Invoke();
        }
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
