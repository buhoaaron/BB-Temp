using UnityEngine;

public enum FingerGroup { Single, Two, All }

public class TouchManager
{
    public static Vector2 Position(FingerGroup fingerGroup = FingerGroup.Single)
    {
#if UNITY_EDITOR
            return Input.mousePosition;
#elif UNITY_ANDROID || UNITY_IOS
        switch (fingerGroup)
        {
            case FingerGroup.Single:
                if (Input.touchCount >= 1) return Input.touches[0].position;
                break;
            case FingerGroup.Two:
                if (Input.touchCount == 1) return Input.touches[0].position;
                else if (Input.touchCount >= 2) return (Input.touches[0].position + Input.touches[1].position) / 2f;
                break;
            case FingerGroup.All:
                return Input.mousePosition;
                break;
        }
        return new Vector2(0, 0);
#endif
    }

#if UNITY_EDITOR
    private static Vector2 pos;

    public static void SetPosition1()
    {
        pos = Input.mousePosition;
    }

    public static float Angle()
    {
        if (((Vector2)Input.mousePosition - pos).y < 0)
            return 360f - Vector2.Angle(((Vector2)Input.mousePosition - pos), new Vector2(1, 0));
        else
            return Vector2.Angle(((Vector2)Input.mousePosition - pos), new Vector2(1, 0));
    }

    public static float Distance()
    {
        return Vector2.Distance(pos, (Vector2)Input.mousePosition);
    }
#elif UNITY_ANDROID || UNITY_IOS
    public static float Angle()
    {
        if (Input.touchCount < 2) return 0;

        if ((Input.touches[1].position - Input.touches[0].position).y < 0)
            return 360f - Vector2.Angle(Input.touches[1].position - Input.touches[0].position, new Vector2(1, 0));
        else
            return Vector2.Angle(Input.touches[1].position - Input.touches[0].position, new Vector2(1, 0));
    }

    public static float Distance()
    {
        if (Input.touchCount < 2) return 0;

        return Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
    }
#endif
}
