using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadFromFileManager
{
    private static AssetBundle assetBundle;

    public static async UniTask<List<Sprite>> LoadAssets(List<string> fileNames)
    {
#if UNITY_EDITOR
        string path = Path.Combine(Application.streamingAssetsPath, "myassetbundle");
        assetBundle = AssetBundle.LoadFromFile(path);
        if (assetBundle == null)
        {
            Debug.Log("AssetBundleÇÃÉçÅ[ÉhÇ…é∏îsÇµÇ‹ÇµÇΩÅI");
            return null;
        }

#elif PLATFORM_WEBGL
        string path = "https://akapikumin887.github.io/myassetbundle";
        UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle(path);
        await req.SendWebRequest();
        assetBundle = (req.downloadHandler as DownloadHandlerAssetBundle).assetBundle;

#endif
        List<Sprite> sprites = new();
        for (int i = 0; i < fileNames.Count; i++)
        {
            Sprite sprite = assetBundle.LoadAsset<Sprite>(fileNames[i]);
            sprites.Add(sprite);
        }

        await UniTask.CompletedTask;

        return sprites;
    }

    ~LoadFromFileManager()
    {
        Dispose();
    }

    public static void Unload()
    {
        assetBundle.Unload(false);
    }

    public static void Dispose()
    {
        if (assetBundle != null)
            assetBundle.Unload(true);
    }
}
