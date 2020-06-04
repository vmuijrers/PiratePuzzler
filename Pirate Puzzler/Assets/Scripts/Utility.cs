using UnityEngine;

public static class Utility
{
    public static T Choose<T>(params T[] input)
    {
        return input[Random.Range(0, input.Length)];
    }
}
