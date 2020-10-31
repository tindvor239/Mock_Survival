using UnityEngine;

public class Compare
{
}
public class Vector
{
    public static bool Epsilon(Vector3 v1, Vector3 v2)
    {
        return NearlyEqual(v1.x, v2.x) && NearlyEqual(v1.y, v2.y) && NearlyEqual(v1.z, v2.z);
    }

    private static bool NearlyEqual(float f1, float f2)
    {
        return Mathf.Abs(f1 - f2) <= 0.1f;
    }
}
