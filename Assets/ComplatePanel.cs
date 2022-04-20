
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComplatePanel : SceneDependentSingleton<ComplatePanel>
{
    // Start is called before the first frame update

    public void loadNewLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
