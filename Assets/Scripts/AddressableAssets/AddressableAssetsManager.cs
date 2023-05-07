using Barnabus;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddressableAssetsManager : BaseBarnabusManager
{
    public PlayerIcons PlayerIcons { private set; get; } = null;

    public AddressableAssetsManager(NewGameManager gm) : base(gm)
    {
    }

    #region BASE_API
    public override void Init()
    {
            
    }
    public override void SystemUpdate()
    {

    }
    public override void Clear()
    {

    }
    #endregion

    public void LoadPlayerIcons()
    {
        GameManager.CustomStartCoroutine(ILoadPlayerIcons());
    }
    private IEnumerator ILoadPlayerIcons()
    {
        var handle = Addressables.LoadAssetAsync<Sprite[]>(AddressablesLabels.PlayerIconSprites);

        yield return handle;

        PlayerIcons = new PlayerIcons(handle.Result);
    }
}
