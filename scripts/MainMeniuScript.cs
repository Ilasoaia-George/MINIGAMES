using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMeniuScript : MonoBehaviour
{
    public Image Background;
    private string SceneName;
    public void GoToColorBox()
    {
        Background.GetComponent<Animation>().Play("MainMeniu_End");
        Invoke("ColorBoxScene", 1);
    }

    public void ColorBoxScene()
    {
        SceneManager.LoadScene("ColorBox");
    }
}
