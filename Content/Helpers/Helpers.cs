using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class Helpers
{
    public static void Clear(this InputField input) => input.text = string.Empty;

    public static void Clear(this Text text) => text.text = string.Empty;

    public static void Clear(this TMP_Text text) => text.text = string.Empty;

    public static void SetAlpha(this SpriteRenderer rend, float alpha) =>
        rend.color = new Color(rend.color.r, rend.color.g, rend.color.b, alpha);

    public static void SetAlpha(this Image image, float alpha) =>
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);

    public static void SetAlpha(this TMP_Text text, float alpha) =>
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);

    public static void SetAlpha(this SpriteRenderer rend, byte alpha) =>
        rend.color = new Color32((byte) (rend.color.r * 255f), (byte) (rend.color.g), (byte) (rend.color.b), alpha);

    public static int GetPercent(this int original, int percent) => (original * percent) / 100;

    public static float GetPercent(this float original, int percent) => (original * percent) / 100;

    public static Vector2 GetPercent(this Vector2 vector, int percent)
    {
        float x = vector.x;
        float y = vector.y;

        x = x * percent / 100;
        y = y * percent / 100;

        return new Vector2(x, y);
    }

    public static Vector3 GetPercent(this Vector3 vector, int percent)
    {
        float x = vector.x;
        float y = vector.y;
        float z = vector.z;

        x = x * percent / 100;
        y = y * percent / 100;
        z = z * percent / 100;

        return new Vector3(x, y, z);
    }

    public static T GetRandomUniqueFromLists<T>(List<T> arrayPool, List<T> arrayExclude)
    {
        return arrayPool.FindAll(x => !arrayExclude.Contains(x)).GetRandomElement();
    }

    public static T GetRandomElement<T>(this IEnumerable<T> container) => container.ElementAt(Random.Range(0, container.Count()));

    public static T GetRandomElementAndRemove<T>(this List<T> container)
    {
        int ind = Random.Range(0, container.Count);
        T obj = container[ind];

        container.RemoveAt(ind);

        return obj;
    }

    public static Vector3 GetRandomVector3Between(Vector3 starting, Vector3 target)
    {
        float distance = Vector3.Distance(starting, target);
        float randDistance = Random.Range(0, distance);

        Vector3 direction = (target - starting).normalized;

        Vector3 result = target - (direction * randDistance);

        return result;
    }

    public static Vector3 GetRandomVector() => new Vector3(Random.value * 360, Random.value * 360, Random.value * 360);

    public static Vector3 GetRandomVector(float min, float max)
        => new Vector3(Random.Range(min, max), Random.Range(min, max), Random.Range(min, max));

    public static void Shuffle<T>(ref List<T> list)
    {
        System.Random _rng = new System.Random();

        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = _rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static void Shuffle<T>(ref T[] array)
    {
        System.Random _rng = new System.Random();

        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = _rng.Next(n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
    }

    public static List<T> DistinctSubtractArrays<T>
    (List<T> baseList, List<T> listToSubstract)
    {
        List<T> res = new List<T>();

        baseList.ForEach(elem =>
        {
            if (!listToSubstract.Contains(elem))
            {
                res.Add(elem);
            }
        });

        return res;
    }

    public static List<T> DistinctSubtractArrays<T>
        (IEnumerable<T> baseList, IEnumerable<T> listToSubstract)
    {
        List<T> res = new List<T>();

        baseList.MyForeach(elem =>
        {
            if (!listToSubstract.Contains(elem))
            {
                res.Add(elem);
            }
        });

        return res;
    }

    public static void Disable(this GameObject gameObject) => gameObject.SetActive(false);

    public static void Enable(this GameObject gameObject) => gameObject.SetActive(true);

    public static bool GetRandomBoolean() => Random.Range(0, 2) == 0;

    public static bool GetRandomBoolean(int percent) => Random.Range(0, 101) < percent;

    public static void Foreach(this Transform transform, System.Action<Transform> action)
    {
        foreach (Transform child in transform)
        {
            action(child);
        }
    }

    public static void LookAtY(this Transform origin, Transform target)
    {
        Vector3 targetPostition = new Vector3(target.position.x,
                                       origin.position.y,
                                       target.position.z);

        origin.LookAt(targetPostition);
    }

    public static Vector2 SetX(this Vector2 vector, float x) => new Vector2(x, vector.y);

    public static Vector2 SetY(this Vector2 vector, float y) => new Vector2(vector.x, y);

    public static Vector2 AddX(this Vector2 vector, float x) => new Vector2(vector.x + x, vector.y);

    public static Vector2 AddY(this Vector2 vector, float y) => new Vector2(vector.x, vector.y + y);

    public static Vector2 GetClosestVector2From(this Vector2 vector, Vector2[] otherVectors)
    {
        if (otherVectors.Length == 0)
            Debug.LogWarning("The list of other vectors is empty");
        float minDistance = Vector2.Distance(vector, otherVectors[0]);
        Vector2 minVector = otherVectors[0];
        for (int i = otherVectors.Length - 1; i > 0; i--)
        {
            float newDistance = Vector2.Distance(vector, otherVectors[i]);
            if (newDistance < minDistance)
            {
                minDistance = newDistance;
                minVector = otherVectors[i];
            }
        }
        return minVector;
    }

    public static Vector2 GetClosestVector2From(this Vector2 vector, List<Vector2> otherVectors)
    {
        if (otherVectors.Count == 0)
            Debug.LogWarning("The list of other vectors is empty");
        float minDistance = Vector2.Distance(vector, otherVectors[0]);
        Vector2 minVector = otherVectors[0];
        for (int i = otherVectors.Count - 1; i > 0; i--)
        {
            float newDistance = Vector2.Distance(vector, otherVectors[i]);
            if (newDistance < minDistance)
            {
                minDistance = newDistance;
                minVector = otherVectors[i];
            }
        }
        return minVector;
    }

    public static void DestroyChildren(this Transform transform)
    {
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }
    }

    public static void ResetTransformation(
        this Transform transform, bool resetPosition = true,
        bool resetRotation = true, bool resetScale = true)
    {
        if (resetPosition)
            transform.position = Vector3.zero;
        if (resetRotation)
            transform.localRotation = Quaternion.identity;
        if (resetScale)
            transform.localScale = Vector3.one;
    }

    public static void ResetChildTransformations(this Transform transform, bool isRecursive = false, bool resetPosition = true,
        bool resetRotation = true, bool resetScale = true)
    {
        foreach (Transform child in transform)
        {
            child.ResetTransformation(resetPosition, resetRotation, resetScale);

            if (isRecursive)
                child.ResetChildTransformations(isRecursive, resetPosition, resetRotation, resetScale);
        }
    }

    public static void MyForeach<T>(this IEnumerable<T> myContainer, System.Action<T> action)
    {
        foreach (T item in myContainer)
        {
            action(item);
        }
    }

    public static void MyForeach<T>(this IEnumerable<T> myContainer, System.Action<T> action, bool @break)
    {
        foreach (T item in myContainer)
        {
            action(item);
            if (@break)
                break;
        }
    }

    public static bool Contains(this LayerMask layerMask, int layer)
    {
        return (layerMask == (layerMask | (1 << layer)));
    }
}
