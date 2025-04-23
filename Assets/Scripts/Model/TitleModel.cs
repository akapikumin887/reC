using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.IO;

public class TitleModel : IModel
{
    public async UniTask<StringReader> StartGame()
    {
        // ��肪�i�[����Ă���X�v���b�h�V�[�g�̏����擾
        StringReader reader = new(await SpreadsheetReader.LoadSpreadSheet());

        if (reader == null)
        {
            Debug.Log("�G���[���������܂���");
        }

        return reader;
    }

    public void Dispose()
    {

    }
}
