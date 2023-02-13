using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOutline : MonoBehaviour
{
    public Image item;
    public Image outline;
    public Color outlineColor;
    public int outlineSize;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) GenerateOutline();
    }

    private void GenerateOutline()
    {
        outline.sprite = Sprite.Create(item.sprite.texture, new Rect(Vector2.zero, item.sprite.texture.texelSize), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
