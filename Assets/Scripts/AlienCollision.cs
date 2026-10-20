using UnityEngine;

public class AlienCollision : MonoBehaviour
{
    private bool playerOverHead;

    void OnEnable()
    {
        playerOverHead = false;    
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        playerOverHead = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        playerOverHead = false;
    }

    public bool CheckHeadStomp()
    {
        return playerOverHead;
    }
}
