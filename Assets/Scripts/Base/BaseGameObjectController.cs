using UnityEngine;
public abstract class BaseGameObjectController : IBaseController
{
    public Transform Transform => gameObject.transform;

    private GameObject gameObject;

    public BaseGameObjectController(GameObject target)
    {
        gameObject = target;
    }

    #region BASE_API
    public abstract void Init();
    public abstract void Refresh();
    public abstract void Clear();
    #endregion
}



