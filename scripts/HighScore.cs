using UnityEngine;

public class HighScore : MonoBehaviour
{
    [SerializeField] private Animation anim;

    public void Home()
    {

        anim.Play("HighScore");
    }
}