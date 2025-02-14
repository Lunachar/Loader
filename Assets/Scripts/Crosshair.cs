using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    private void Start()
    {
        GameObject crosshair = new GameObject("Crosshair");
        crosshair.transform.SetParent(GameObject.Find("UI_Canvas_Crosshair").transform);
        crosshair.AddComponent<RectTransform>().anchoredPosition = Vector2.zero;
        
        Image img = crosshair.AddComponent<Image>();
        img.sprite = Resources.Load<Sprite>("Sprites/crosshair");
    }
}
