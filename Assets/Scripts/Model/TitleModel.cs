using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using System.IO;

public class TitleModel : IModel
{
    public async UniTask<StringReader> StartGame()
    {
        // 問題が格納されているスプレッドシートの情報を取得
        StringReader reader = new(await SpreadsheetReader.LoadSpreadSheet());

        if (reader == null)
        {
            Debug.Log("エラーが発生しました");
        }

        return reader;
    }

    public void Dispose()
    {

    }
}
