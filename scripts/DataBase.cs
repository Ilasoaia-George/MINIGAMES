using UnityEngine;

public class DataBase : MonoBehaviour
{
    public string SceneName;
    [SerializeField] private GameObject RGB;
    public GameObject cover;
    [SerializeField] private GameObject CustomMenuGO;
    [SerializeField] private TextMesh ScoreTXT;
    [SerializeField] private GameObject GameOverMenu;
    //private GameObject[] CustomMenuList;
    //public GameObject StatsMenu, OptionsMenu, CustomMenu, AllowMenu;
    public int score;
    public bool GameOver = false, Restart = false;
    private int i, j;
    private GameObject[] GoList, CustomMenuList;
    private bool GameOverMenuAdd=false;


    private void Start()
    {
        GameOver = false;
        Time.timeScale = 1f;
        InitializingColors();
        cover.SetActive(true);
        Invoke("CoverOff", cover.GetComponent<Animation>().GetClip("StartScene").length);
        PlayerPrefs.SetInt(SceneName + "_ACCESARI", PlayerPrefs.GetInt(SceneName + "_ACCESARI") + 1);
    }

    private void ColPart()
    {
        for (int j = 0; j < GoList.Length; j++)
        {
            Color color = new Color(PlayerPrefs.GetFloat(SceneName + '_' + CustomMenuList[i].name + "_r"),
                            PlayerPrefs.GetFloat(SceneName + '_' + CustomMenuList[i].name + "_g"),
                            PlayerPrefs.GetFloat(SceneName + '_' + CustomMenuList[i].name + "_b"));

            if (GoList[j].GetComponent<SpriteRenderer>())
            { color.a = GoList[j].GetComponent<SpriteRenderer>().color.a; GoList[j].GetComponent<SpriteRenderer>().color = color; }
            else if (GoList[j].GetComponent<TextMesh>())
            { color.a = GoList[j].GetComponent<TextMesh>().color.a; GoList[j].GetComponent<TextMesh>().color = color; }
            else if (GoList[j].GetComponent<ParticleSystem>())
                GoList[j].GetComponent<ParticleSystem>().startColor = color;
        }
    }

   private void InitializingColors()
    {
        Transform[] trans = CustomMenuGO.GetComponentsInChildren<Transform>(true);
        CustomMenuList = new GameObject[trans.Length - 1];
        for (i = 1; i < trans.Length; i++)
            CustomMenuList[i - 1] = trans[i].gameObject;

        
        for (i=0; i<CustomMenuList.Length; i++)
        {
            bool single = false, CamBG = false;
            if (CustomMenuList[i].name[CustomMenuList[i].name.Length - 1] == '!')
                single = true;

            if (single)
                if (CustomMenuList[i].name == "Background!") CamBG = true;
                else
                {
                    GoList = new GameObject[1];                  
                    GoList[0] = GameObject.Find(CustomMenuList[i].name.Substring(0, CustomMenuList[i].name.Length - 1));
                    ColPart();
                }
            else
            {
                GoList = GameObject.FindGameObjectsWithTag(CustomMenuList[i].name);
                ColPart();
            }
            if(CamBG)
            {
                Color color = new Color(PlayerPrefs.GetFloat(SceneName + "_Background!_r"),
                                    PlayerPrefs.GetFloat(SceneName + "_Background!_g"),
                                    PlayerPrefs.GetFloat(SceneName + "_Background!_b"));

                Camera.main.backgroundColor = color;
            }

        }
    }

    private void CoverOff()
    {
        cover.SetActive(false);
    }

    private void Update()
    {
        if (GameOver && !Restart)
            SetGameOver();
    }

    public void RestartGame()
    {
        cover.GetComponent<Animation>().Play("");
    }


    private void SetGameOver()
    {
        //GameOver = true;
        ScoreTXT.text = "GAME OVER";
        GameOverMenu.SetActive(true);
        GetComponent<AudioSource>().pitch = Mathf.Lerp(GetComponent<AudioSource>().pitch, 0, .00095f);
        Time.timeScale = Mathf.Lerp(Time.timeScale, 0, .0009f);
    }

}






