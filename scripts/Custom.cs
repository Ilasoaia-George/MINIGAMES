using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Custom : MonoBehaviour
{
    public string SceneName;
    [SerializeField] private GameObject CustomMenu;
    [SerializeField] private GameObject RGBMenu;
    public GameObject[] Color_Go;
    [HideInInspector]public GameObject CursorTarget, RayTarget;
    private GameObject[] GO;
    private Color color;

    private RaycastHit2D CursorHit;

    public void MeniuStabilized()
    {
            CustomMenu.SetActive(true);            
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CursorHit = Physics2D.Raycast(Input.mousePosition, Vector2.zero);
            if (CursorHit.collider) 
            {
                CursorTarget = CursorHit.collider.gameObject;
                CursorTarget.GetComponent<TextMeshProUGUI>().color = Color.yellow;
                //RGBMenu.SetActive(true);
            }
        }
    }
}



