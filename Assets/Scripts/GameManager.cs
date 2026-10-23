using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private const string GAME_OVER_MESSAGE = "Vous êtes mort!\nPartie terminé!";
    private const string VICTORY_MESSAGE = "Victoire!\nPartie terminé!";
    [SerializeField] private PlayerHealthManager player;
    [SerializeField] private UnityEvent<string> onLevelComplete;
    private SpawnManager spawnManager;
    private InputAction quitAction;

    void Awake()
    {
        quitAction = InputSystem.actions.FindAction("Quit");
        spawnManager = GetComponent<SpawnManager>();
    }

    void Update()
    {
        if (quitAction.WasPerformedThisFrame()) QuitGame();

        if (!player.isAlive())
        {
            onLevelComplete.Invoke(GAME_OVER_MESSAGE);
        }
        else if (spawnManager.IsLevelComplete())
        {
            onLevelComplete.Invoke(VICTORY_MESSAGE);
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
