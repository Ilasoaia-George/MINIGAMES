using UnityEngine;

public class ResetStats : MonoBehaviour
{
    [SerializeField] private string[] status_id;
    public void ResetStatus()
    {
        int i;
        for (i = 0; i < status_id.Length; i++)
            PlayerPrefs.SetInt(status_id[i], 0);
    }
}
