using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class DirtyObjectHelper : EditorWindow
{
    [MenuItem("Tools/Dity Helpers/Object Helper")]
    public static void ShowMyEditor()
    {
        EditorWindow wnd = GetWindow<DirtyObjectHelper>();

        wnd.titleContent = new GUIContent("Dirty Object Helper");
    }

    void OnGUI()
    {
        GUILayout.Space(50);

        if (Selection.activeTransform == null)
        {
            EditorGUILayout.LabelField("Please select one object.");
        }
        else
        {
            if (GUILayout.Button("Recenter pivot"))
            {
                Transform selectedTransform = Selection.activeTransform;

                RecenterParent(selectedTransform);
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Parent Objects"))
            {
                ParentObjects();
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Parent Objects + Recenter"))
            {
                RecenterParent(ParentObjects().transform);
            }

            GUILayout.Space(20);

            if (GUILayout.Button("Clear all colliders"))
            {
                List<GameObject> selectedObjects = Selection.gameObjects.ToList();

                foreach (GameObject selectedObject in selectedObjects)
                {
                    List<Transform> allChildren = selectedObject.GetComponentsInChildren<Transform>().ToList();

                    foreach (Transform child in allChildren)
                    {
                        if(child.TryGetComponent(out Collider collider))
                        {
                            DestroyImmediate(collider);
                        }
                    }
                }
            }
        }
    }

    private GameObject ParentObjects()
    {
        List<GameObject> selectedObjects = Selection.gameObjects.ToList();

        GameObject objToSpawn = new GameObject("New Object");

        foreach (GameObject obj in selectedObjects)
        {
            obj.transform.SetParent(objToSpawn.transform);
        }

        return objToSpawn;
    }

    private void RecenterParent(Transform selectedTransform)
    {
        Vector3 sumPosition = Vector3.zero;
        List<Transform> cachedChildren = new List<Transform>();

        foreach (Transform child in selectedTransform)
        {
            sumPosition += child.transform.position;
            cachedChildren.Add(child);
        }

        foreach (Transform child in cachedChildren)
        {
            child.SetParent(null);
        }

        Vector3 normalizedPosition = sumPosition / cachedChildren.Count;

        selectedTransform.position = normalizedPosition;

        foreach (Transform child in cachedChildren)
        {
            child.SetParent(selectedTransform);
        }
    }
}
