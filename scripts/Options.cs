using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] private GameObject GameOverMenu;
    public GameObject RootMenu;    
    public List<GameObject> menu = new List<GameObject>();   

    void Awake()
    {
        gameObject.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Volume");
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GetComponent<DataBase>().GameOver)
        {
            if (!(menu.Count == 1 && menu[0].name == "GameOverMenu"))
                if (menu.Count == 0) // Deschide meniul
                {
                    menu.Add(RootMenu);
                    RootMenu.SetActive(true);
                    GetComponent<AudioSource>().pitch = 0;

                    GameObject.Find("Volume").GetComponent<Slider>().value = PlayerPrefs.GetFloat("Volume");

                    if (PlayerPrefs.GetFloat("Volume") > 0)               
                            GameObject.Find("Sound").gameObject.GetComponent<Image>().sprite = GameObject.Find("Volume").GetComponent<Audio>().SoundON;
                        else GameObject.Find("Sound").gameObject.GetComponent<Image>().sprite = GameObject.Find("Volume").GetComponent<Audio>().SoundOFF;
                    Time.timeScale = 0f;
                }
                else
                {
                    menu[menu.Count - 1].SetActive(false);
                    if (menu.Count > 1)
                        menu[menu.Count - 2].SetActive(true);
                    menu.Remove(menu[menu.Count - 1]);
                    if (menu.Count == 0) // Intra-n joc
                    {
                        GetComponent<AudioSource>().pitch = 1.1f;
                        if(!GetComponent<DataBase>().GameOver)
                        Time.timeScale = 1f;
                    }
                }
        }
    }
}
