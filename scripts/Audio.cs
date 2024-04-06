using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public Sprite SoundON;
    public Sprite SoundOFF;
    public Slider bar;
    [SerializeField] private AudioSource sound;

    public void SoundButton()
    {
        if (PlayerPrefs.GetFloat("Volume") > 0) // Setam Audio OFF
        {
            GetComponent<Image>().sprite = SoundOFF;
            PlayerPrefs.SetFloat("Volume", 0);
        }

        else  //Setam Audio ON

        {
            GetComponent<Image>().sprite = SoundON;
            PlayerPrefs.SetFloat("Volume", bar.maxValue/2);
        }

        bar.value = PlayerPrefs.GetFloat("Volume");
    }

    public void SoundSlider()
    {
        PlayerPrefs.SetFloat("Volume", bar.value);

        if(bar.value==0)
            GameObject.Find("Sound").GetComponent<Image>().sprite = SoundOFF;
        else
            GameObject.Find("Sound").GetComponent<Image>().sprite = SoundON;

        sound.volume = bar.value;
    }
}
