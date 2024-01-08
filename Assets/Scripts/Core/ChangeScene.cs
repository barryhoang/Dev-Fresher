using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void MoveToScence(int sceneId) => SceneManager.LoadScene(sceneId);

    public void QuitGame() => Application.Quit();
}
