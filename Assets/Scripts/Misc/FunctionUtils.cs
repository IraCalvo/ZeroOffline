using UnityEngine;

public class FunctionUtils
{
    public static Vector2 GetRandomPositionInCircle(Vector2 center, float radius)
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float distance = Random.Range(0f, radius);

        float x = center.x + Mathf.Cos(angle) * distance;
        float y = center.y + Mathf.Sin(angle) * distance;

        return new Vector2(x, y);
    }

    public static int RandomChance(int floor, int ceiling)
    {
        int randInt = Random.Range(floor, ceiling);
        return randInt;
    }
}
