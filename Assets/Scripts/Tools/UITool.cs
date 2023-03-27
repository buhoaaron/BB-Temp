using UnityEngine;
using UnityEngine.UI;

public static class UITool
{
    public static void SetAlpha(Graphic graphic, float alpha)
    {
        var color = graphic.color;
        color.a = alpha;

        graphic.color = color;
    }

    public static void SetPositionX(Graphic graphic, int x)
    {
        var nowPos = graphic.transform.localPosition;
        graphic.transform.localPosition = new Vector2(x, nowPos.y);
    }

    public static void SetPositionY(Graphic graphic, int y)
    {
        var nowPos = graphic.transform.localPosition;
        graphic.transform.localPosition = new Vector2(nowPos.x, y);
    }
}

