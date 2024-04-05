using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonsActions : MonoBehaviour
{
    private string SceneName;
    [SerializeField] private GameObject NextMenu;
    [SerializeField] private GameObject CurrentMenu;
    [SerializeField] private GameObject CustomMenuColor;
    [SerializeField] private Options OptionsScript;
    [SerializeField] private GameObject RGB;
    [SerializeField] private string NextScene;
    private GameObject go, main;
    private string anim_name;

    private void Awake()
    {
        SceneName = SceneManager.GetActiveScene().name;
        main = GameObject.Find("Main Camera");
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(NextScene);
    }

    private void PlayAnim()
    {
        go.SetActive(true);  
        if(go == NextMenu) GameObject.Find("HighScoreTXT").GetComponent<TextMeshProUGUI>().text = GameObject.Find("Main Camera").GetComponent<DataBase>().score + "";
        go.GetComponent<Animation>().Play(anim_name);
    }

    public void Home()
    {
        
        SceneName = "MainMeniu";
        MenuEfects();    
    }

    public void Snake()
    {
        SceneName = "Snake";
        Invoke("LoadScene", 0.5f);
    }

    public void Exit()
    {
        OptionsScript.menu.Clear();
        CurrentMenu.SetActive(false);
        if (!GameObject.Find("Main Camera").GetComponent<DataBase>().GameOver)
        {
            GameObject.Find("Main Camera").GetComponent<AudioSource>().pitch = 1.1f;
            Time.timeScale = 1f;
        }
    }

    public void GoToMenu()
    {
        NextMenu.SetActive(true);
        if (NextMenu.name == "CustomMenu")
        {
            CustomMenuColor.GetComponent<ColorRGB>().TargetColor = GameObject.FindGameObjectsWithTag("CML");
            CustomMenuColor.SetActive(false);
        }            
            OptionsScript.menu.Add(NextMenu);
        if(CurrentMenu)
        CurrentMenu.SetActive(false);
    }
    public void Back()
    {
        NextMenu.SetActive(true);
        OptionsScript.menu.Remove(CurrentMenu);
        CurrentMenu.SetActive(false);
    }

    public void Restart()
    {
        SceneName = SceneManager.GetActiveScene().name;
        MenuEfects();
    }

    public void RGB_GroupTarget()
    {
        bool single = false;
        RGB.GetComponent<ColorRGB>().GroupTarget = gameObject;
        if (gameObject.name[gameObject.name.Length - 1] == '!')      
            single = true;

        int i;
        for (i = 0; i < RGB.GetComponent<ColorRGB>().TargetColor.Length; i++)
            if (gameObject == RGB.GetComponent<ColorRGB>().TargetColor[i])
            {
                RGB.GetComponent<ColorRGB>().CamBG = false;
                if (!single)                    
                        RGB.GetComponent<ColorRGB>().GOlist = GameObject.FindGameObjectsWithTag(gameObject.name);
                else
                {                    
                    RGB.GetComponent<ColorRGB>().GOlist = new GameObject[1];
                    if (GameObject.Find(gameObject.name.Substring(0, gameObject.name.Length - 1)))
                        RGB.GetComponent<ColorRGB>().GOlist[0] = GameObject.Find(gameObject.name.Substring(0, gameObject.name.Length - 1));                    
                    else if (gameObject.name == "Background!") RGB.GetComponent<ColorRGB>().CamBG = true;
                }
                break;
            }

        Color color = new Color(PlayerPrefs.GetFloat(SceneName + '_' + RGB.GetComponent<ColorRGB>().GroupTarget.name + "_r"),
                                PlayerPrefs.GetFloat(SceneName + '_' + RGB.GetComponent<ColorRGB>().GroupTarget.name + "_g"),
                                PlayerPrefs.GetFloat(SceneName + '_' + RGB.GetComponent<ColorRGB>().GroupTarget.name + "_b"));

        RGB.GetComponent<ColorRGB>().RGB_cursor.GetComponent<Image>().color = color;
        RGB.GetComponent<ColorRGB>().RGB_selected.GetComponent<Image>().color = color;
        RGB.GetComponent<ColorRGB>().RGBcursor_dir.transform.rotation = Quaternion.Euler(0, 0, PlayerPrefs.GetFloat(SceneName + '_' + RGB.GetComponent<ColorRGB>().GroupTarget.name + "_rot"));
        RGB.GetComponent<ColorRGB>().RGB_cursor.transform.localPosition = new Vector2(0, PlayerPrefs.GetFloat(SceneName + '_' + RGB.GetComponent<ColorRGB>().GroupTarget.name + "_pos_y"));

        if (!RGB.active)
        RGB.SetActive(true);
    }

    private void MenuEfects()
    {
        Time.timeScale = 1f;
        GameObject.Find("Main Camera").GetComponent<DataBase>().Restart = true;
        // CurrentMenu = Cover , NextMenu = HighScoreEvent
        Color col = CurrentMenu.GetComponent<Image>().color;
        col.a = 1;
        CurrentMenu.GetComponent<Image>().color = col;


        go = CurrentMenu; anim_name = "EndScene"; // Play la animatia de incheiere a scenei
        PlayAnim();
        

        if (GameObject.Find("Main Camera").GetComponent<DataBase>().score > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_HIGHSCORE"))
        {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_HIGHSCORE", GameObject.Find("Main Camera").GetComponent<DataBase>().score);
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_HIGHSCORE", GameObject.Find("Main Camera").GetComponent<DataBase>().score);
            go = NextMenu; anim_name = "HighScore";
            Invoke("PlayAnim", CurrentMenu.GetComponent<Animation>().GetClip("EndScene").length); // Play la animatia de HighScore
        }
        Invoke("LoadScene", go.GetComponent<Animation>().GetClip(anim_name).length + 1);
    }

    public void Quit()
    {
        GameObject.Find("Background").SetActive(false);
        Application.Quit();       
    }
}
