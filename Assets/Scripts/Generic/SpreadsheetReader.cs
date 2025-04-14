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

        // SendWebRequest��cts���g�p���Ȃ��Ė��Ȃ�
        await request.SendWebRequest();
        
        switch(request.result)
        {

            case UnityWebRequest.Result.InProgress:
                Debug.Log("���N�G�X�g��");
                break;

            case UnityWebRequest.Result.Success:
                Debug.Log("���N�G�X�g����");
                break;

            case UnityWebRequest.Result.ConnectionError:
                Debug.Log("�T�[�o�[�Ƃ̒ʐM�Ɏ��s���܂���");
                break;

            case UnityWebRequest.Result.ProtocolError:
                Debug.Log("�T�[�o�[���G���[�������Ԃ���܂���");
                break;

            case UnityWebRequest.Result.DataProcessingError:
                Debug.Log("�f�[�^�������ɃG���[���������܂���");
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        return request.downloadHandler.text;
    }
}
