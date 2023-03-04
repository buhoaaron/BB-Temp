using UnityEngine.UI;

public static class UITool
{
    public static void SetAlpha(Graphic graphic, float alpha)
    {
        var color = graphic.color;
        color.a = alpha;

        graphic.color = color;
    }
}

