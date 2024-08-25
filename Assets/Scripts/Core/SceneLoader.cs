using UnityEngine.SceneManagement;

public class SceneLoader
{
    public void LoadNewLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
