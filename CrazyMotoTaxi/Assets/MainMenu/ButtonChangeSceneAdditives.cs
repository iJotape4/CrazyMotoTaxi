using MyBox;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonChangeSceneAdditives : MonoBehaviour
{
    [SerializeField, Scene] string sceneCityBuilder, sceneMainCity, systemsPrefabs;
    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenScene);
    }

    private void OpenScene()
    {
       var currentScene = SceneManager.GetActiveScene();      
       SceneManager.LoadSceneAsync(sceneCityBuilder, LoadSceneMode.Additive);
       SceneManager.LoadSceneAsync(sceneMainCity, LoadSceneMode.Additive);
        // TODO assign systems scene
        //SceneManager.LoadSceneAsync(systemsPrefabs, LoadSceneMode.Additive);

        Destroy(transform.root.gameObject);                      
    }
}