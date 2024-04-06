using UnityEngine.UI;
using UnityEngine;

public class ColorRGB : MonoBehaviour
{
    private string SceneName;
    private Texture2D RGBimage_texture;
    private Vector2 PosInsideImage, center;
    private float width, height, cursdir_rot;
    private bool ChangePermision = false;
    public bool CamBG=false;
    [SerializeField] private int nrtags;
    [SerializeField] int distance_max;
    [SerializeField] private GameObject RGB_circle;
    [HideInInspector] public GameObject RGB_cursor;
     public GameObject RGBcursor_dir;
     public GameObject RGB_selected;
    [HideInInspector] public Color SelectedColor;
    public GameObject[] GOlist, TargetColor;
    public GameObject GroupTarget;
    private Color color;
      



    private void Start()
    {
        SceneName = GameObject.Find("Main Camera").GetComponent<DataBase>().SceneName;
        RGBimage_texture = RGB_circle.GetComponent<Image>().mainTexture as Texture2D;
    }

    private void Update()
    {
        _update();

        if (Input.GetMouseButtonDown(0) && Vector2.Distance(RGBcursor_dir.transform.position, Input.mousePosition) * 2 <= distance_max) // Press
            ChangePermision = true;

        if (ChangePermision) // Hold
        {
            ColorPicking();
            UpdateData();
            ApplyData();
        }

        if (Input.GetMouseButtonUp(0)) // Release
            ChangePermision = false;
    }

    private void _update()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(RGB_circle.GetComponent<RectTransform>(), RGB_cursor.transform.position, null, out PosInsideImage); 
        width = RGB_circle.GetComponent<RectTransform>().rect.width;
        height = RGB_circle.GetComponent<RectTransform>().rect.height;

        center.x = Mathf.Clamp(PosInsideImage.x, -width / 2, width / 2);
        center.y = Mathf.Clamp(PosInsideImage.y, -height / 2, height / 2);

        PosInsideImage.x += width / 2; PosInsideImage.y += height / 2;
        PosInsideImage.x /= width; PosInsideImage.y /= height;
        PosInsideImage.x = Mathf.Clamp(PosInsideImage.x, 0, 1); PosInsideImage.y = Mathf.Clamp(PosInsideImage.y, 0, 1);
        PosInsideImage.x *= RGBimage_texture.width; PosInsideImage.y *= RGBimage_texture.height;
        PosInsideImage.x = Mathf.RoundToInt(PosInsideImage.x); PosInsideImage.y = Mathf.RoundToInt(PosInsideImage.y);
    }

    private void ColorPicking()
    {
        float angle, r;
        Vector2 lookdir = Input.mousePosition - RGBcursor_dir.transform.position;
        cursdir_rot = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        RGBcursor_dir.transform.rotation = Quaternion.Euler(0, 0, 0);
        RGBcursor_dir.transform.rotation = Quaternion.Euler(0, 0, cursdir_rot);
        RGB_cursor.transform.localPosition = new Vector2(0, Mathf.Clamp(Vector2.Distance(RGBcursor_dir.transform.position, Input.mousePosition) * 2, 0, distance_max));
        SelectedColor = RGBimage_texture.GetPixel(Mathf.RoundToInt(PosInsideImage.x), Mathf.RoundToInt(PosInsideImage.y));
        RGB_selected.GetComponent<Image>().color = SelectedColor;
        RGB_cursor.GetComponent<Image>().color = SelectedColor;
    }



    //DATABASE


    private void UpdateData()
    {
        PlayerPrefs.SetFloat(SceneName + '_' + GroupTarget.name + "_r", SelectedColor.r);
        PlayerPrefs.SetFloat(SceneName + '_' + GroupTarget.name + "_g", SelectedColor.g);
        PlayerPrefs.SetFloat(SceneName + '_' + GroupTarget.name + "_b", SelectedColor.b);
        Debug.Log(SceneName + '_' + GroupTarget.name + "_b");

        PlayerPrefs.SetFloat(SceneName + '_' + GroupTarget.name + "_rot", cursdir_rot);
        PlayerPrefs.SetFloat(SceneName + '_' + GroupTarget.name + "_pos_y", RGB_cursor.transform.localPosition.y);
    }

    private void ApplyData()
    {
        RGB_cursor.transform.localPosition = new Vector2(0, PlayerPrefs.GetFloat(SceneName + '_' + GroupTarget.name + "_pos_y"));

        Color color = new Color(PlayerPrefs.GetFloat(SceneName + '_' + GroupTarget.name + "_r"),
                                PlayerPrefs.GetFloat(SceneName + '_' + GroupTarget.name + "_g"),
                                PlayerPrefs.GetFloat(SceneName + '_' + GroupTarget.name + "_b"));

        int i;
        for (i = 0; i < GOlist.Length; i++)
            if (CamBG)
                Camera.main.backgroundColor = color;
            else if (GOlist[i].GetComponent<SpriteRenderer>())
            { color.a = GOlist[i].GetComponent<SpriteRenderer>().color.a; GOlist[i].GetComponent<SpriteRenderer>().color = color; }
            else if (GOlist[i].GetComponent<TextMesh>())
                GOlist[i].GetComponent<TextMesh>().color = color;
            else if (GOlist[i].GetComponent<ParticleSystem>())
                GOlist[i].GetComponent<ParticleSystem>().startColor = color;

        Debug.Log("Color : " + SceneName + '_' + GroupTarget.name + "_r");
    }

}