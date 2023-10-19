using System.Threading.Tasks;
using UnityEngine;

public class AssetLoader : MonoBehaviour
{
    [SerializeField] private AssetLoaderSO _assetList;

    public AssetLoaderSO Assets => _assetList;
    
    public delegate void InvokeMessage(string msg);
    public delegate void Notify();

    public static event InvokeMessage OnCategoryMessage; // 전체 로딩 카테고리 메시지
    public static event InvokeMessage OnDescMessage;     // 개별 로딩 메시지
    public static event Notify OnLoadComplete; // 로딩 완료 통지

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private async void Start()
    {
        var total = _assetList.TotalCount;
        OnCategoryMessage?.Invoke($"Loading {total} Assets...");
        await LoadAssets();
        OnCategoryMessage?.Invoke("Make object pool...");
        await MakePooling();

        OnLoadComplete?.Invoke();
    }

    private async Task LoadAssets()
    {
        // await Task.WhenAll(_assetList.loadingList.Select(x => LoadAsset(x));

        foreach (var r in _assetList.loadingList)
        {
            var asset = await r.LoadAssetAsync<GameObject>().Task;
            OnDescMessage?.Invoke($"loading...{asset.name}");
            _assetList.LoadingComplete(r, asset.name);
        }

        foreach (var r in _assetList.poolingList)
        {
            var asset = await r.assetRef.LoadAssetAsync<GameObject>().Task;
            OnDescMessage?.Invoke($"loading...{asset.name}");
            _assetList.LoadingComplete(r.assetRef, asset.name);
        }
    }

    // private async Task  LoadAsset(AssetReference r)
    // {
    //     var asset = await r.LoadAssetAsync<GameObject>().Task;
    //     OnDescMessage?.Invoke($"loading...{asset.name}");
    //     _assetList.LoadingComplete(r, asset.name);
    // }

    private async Task MakePooling()
    {
        PoolManager.Instance = new PoolManager(transform);
        foreach (var r in _assetList.poolingList)
        {
            var prefab = (r.assetRef.Asset as GameObject).GetComponent<PoolableMono>();

            if (prefab == null)
            {
                Debug.LogWarning($"{r.assetRef.Asset.name} does not has poolable mono");
                continue;
            }

            OnDescMessage?.Invoke($"loading...{r.assetRef.Asset.name}");
            await Task.Delay(1); // UI 반영을 위해 다음 프레임으로 대기
            PoolManager.Instance.CreatePool(r.assetRef.AssetGUID, prefab, r.count);
        }
    }
}
