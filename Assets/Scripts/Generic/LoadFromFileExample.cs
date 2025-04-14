using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LoadFromFileExample : MonoBehaviour
{
    [SerializeField]
    private Image image = default;

    [SerializeField]
    private Image image02 = default;

    private void Start()
    {
        // AssetBundleのメタ情報をロード
        var assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "myassetbundle"));

        if (assetBundle == null)
        {
            Debug.Log("AssetBundleのロードに失敗しました！");
            return;
        }

        // テクスチャにメモリをロード
        var sprite = assetBundle.LoadAsset<Sprite>("computer_smartphone2_blank");
        image.sprite = sprite;

        var sprite02 = assetBundle.LoadAsset<Sprite>("ハンマー選択 1");
        image02.sprite = sprite02;

        // AssetBundleのメタ情報をアンロード
        assetBundle.Unload(false);
    }
}
