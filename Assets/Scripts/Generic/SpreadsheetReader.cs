using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class SpreadsheetReader
{
    private const string SHEET_ID = "13sFtz09L6QBPvxYXgkDUIXzI4HIRzUu-IMH3ueCbIKo";
    private const string SHEET_NAME = "01_question";
    
    public SpreadsheetReader()
    {

    }

    public async UniTask<string> LoadSpreadSheet()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://docs.google.com/spreadsheets/d/" + SHEET_ID + "/gviz/tq?tqx=out:csv&sheet=" + SHEET_NAME);

        // SendWebRequestはctsを使用しなくて問題ない
        await request.SendWebRequest();
        
        switch(request.result)
        {

            case UnityWebRequest.Result.InProgress:
                Debug.Log("リクエスト中");
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("リクエスト成功");
                break;

            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("サーバーとの通信に失敗しました");
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("サーバーよりエラー応答が返されました");
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("データ処理中にエラーが発生しました");
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        return request.downloadHandler.text;
    }
}
