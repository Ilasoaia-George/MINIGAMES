using UnityEngine;
using TMPro;

public class Stats : MonoBehaviour
{
    private string SceneName;
    [SerializeField] private GameObject StatsList;
    [SerializeField]private  TextMeshProUGUI[] StatsData;

    private void Awake()
    {
        //SceneName = GameObject.Find("Main Camera").GetComponent<DataBase>().SceneName;
    }


    public void ShowStatus()
    {
        StatsData = StatsList.GetComponentsInChildren<TextMeshProUGUI>();
        SceneName = GameObject.Find("Main Camera").GetComponent<DataBase>().SceneName;
        int i;
        for (i = 0; i < StatsData.Length; i++)
        {
            StatsData[i].text = StatsData[i].gameObject.name + " : " + PlayerPrefs.GetInt(SceneName + '_' + StatsData[i].gameObject.name);
            Debug.Log(SceneName + '_' + StatsData[i].gameObject.name + " : " + PlayerPrefs.GetInt(SceneName + '_' + StatsData[i].gameObject.name));
        }
    }
    public void ResetStatus()
    {
        SceneName = GameObject.Find("Main Camera").GetComponent<DataBase>().SceneName;
        StatsData = StatsList.GetComponentsInChildren<TextMeshProUGUI>();
        int i;
        for(i=0; i<StatsData.Length; i++)
            PlayerPrefs.SetInt(SceneName + '_' + StatsData[i].gameObject.name, 0);
        ShowStatus();
    }

    

}
