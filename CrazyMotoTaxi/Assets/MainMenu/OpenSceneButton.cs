using MyBox;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenSceneButton : MonoBehaviour
{
    [SerializeField,Scene] string sceneName;
    Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenScene);
    }

    private void OpenScene()
    {
       SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
