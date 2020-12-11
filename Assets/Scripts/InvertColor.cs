using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertColor : MonoBehaviour
{
    public Sprite spr;
    public SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        sp.sprite = spr;
        sp.color = ColorInvert(sp.color);
    }
    public Color ColorInvert(Color startColor)
    {
        float h, s, v;

        Color.RGBToHSV(startColor, out h, out s, out v);

        return Color.HSVToRGB((h + 0.5f) % 1, s, v);
    }
    //将图片颜色反转
    public Texture2D TransparentColor(Texture2D img)
    {
        Texture2D copyImge = new Texture2D(img.width, img.height);
        copyImge.SetPixels(img.GetPixels());
        copyImge.Apply();
        for (int h = 0; h < copyImge.height; h++)
        {
            for (int w = 0; w < copyImge.width; w++)
            {
                float r = 1 - copyImge.GetPixel(w, h).r;
                float g = 1 - copyImge.GetPixel(w, h).g;
                float b = 1 - copyImge.GetPixel(w, h).b;
                copyImge.SetPixel(w, h, new Color(r, g, b));
            }
            copyImge.Apply();
        }
        return copyImge;
    }
}
