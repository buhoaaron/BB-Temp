using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 遊戲物件生成者基底
/// </summary>
public abstract class BasePrefabBuilder : MonoBehaviour
{
    [Header("Set Parent")]
    public RectTransform Parent = null;
    [Header("Set Prefab")]
    public GameObject Prefab = null;

    private List<GameObject> listObject = null;

    private void Awake()
    {
        Debug.AssertFormat(Parent != null, "No Parent to set.");
        Debug.AssertFormat(Prefab != null, "No Prefab to set.");
    }

    #region BASE_API
    public virtual void Init()
    {
        listObject = new List<GameObject>();
    }
    public virtual List<GameObject> Build(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var go = GameObject.Instantiate(Prefab, Parent);
            listObject.Add(go);
        }

        return listObject;
    }

    public virtual GameObject BuildSingle()
    {
        var go = GameObject.Instantiate(Prefab, Parent);
        listObject.Add(go);

        return go;
    }

    public virtual void Destroy()
    {
        foreach (GameObject go in listObject)
            Destroy(go);

        listObject.Clear();
    }
    #endregion
}

