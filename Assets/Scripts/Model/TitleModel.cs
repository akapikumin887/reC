using UnityEngine;
using Cysharp.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class TitleModel : IModel
{
    public async UniTask<List<QuizTemplate>> LoadSpreadsheetDataAsync()
    {
        List<QuizTemplate> quizTemplates = new();

        // 問題が格納されているスプレッドシートの情報を取得
        StringReader reader = new(await SpreadsheetReader.LoadSpreadSheet());

        if (reader == null)
        {
            Debug.Log("エラーが発生しました");
            return null;
        }

        for(int i = 0; i < 25; i++)
        {
            string line = reader.ReadLine().Replace("\"", "");
            if (!Regex.IsMatch(line, "選択|筆記|パズル"))
                continue;

            quizTemplates.Add(new QuizTemplate(line));
        }

        return quizTemplates;
    }

    public void Dispose()
    {

    }
}
