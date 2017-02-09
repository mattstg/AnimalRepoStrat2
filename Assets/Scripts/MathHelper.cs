using System.Collections;
using System.Collections.Generic;
using UnityEngine; using LoLSDK;

public class MathHelper  {

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }

    public static Vector2 V3toV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }

    public static float AngleBetweenPoints(Vector2 p1, Vector2 p2)
    {
        return Mathf.Atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Mathf.PI;
    }

	public static bool Fiftyfifty()
	{
		return (Random.Range (0f, 1f) > .5f);
	}
}
