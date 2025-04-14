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
        // AssetBundle�̃��^�������[�h
        var assetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "myassetbundle"));

        if (assetBundle == null)
        {
            Debug.Log("AssetBundle�̃��[�h�Ɏ��s���܂����I");
            return;
        }

        // �e�N�X�`���Ƀ����������[�h
        var sprite = assetBundle.LoadAsset<Sprite>("computer_smartphone2_blank");
        image.sprite = sprite;

        var sprite02 = assetBundle.LoadAsset<Sprite>("�n���}�[�I�� 1");
        image02.sprite = sprite02;

        // AssetBundle�̃��^�����A�����[�h
        assetBundle.Unload(false);
    }
}
