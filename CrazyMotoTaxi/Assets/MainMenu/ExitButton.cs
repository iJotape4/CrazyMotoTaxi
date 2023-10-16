using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Application.Quit());
    }
}