using UnityEngine;
using TMPro;

public class TextUpdate : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public void UpdateText(string newText)
    {
        text.text = newText;
    }
}
