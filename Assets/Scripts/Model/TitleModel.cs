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

        // ��肪�i�[����Ă���X�v���b�h�V�[�g�̏����擾
        StringReader reader = new(await SpreadsheetReader.LoadSpreadSheet());

        if (reader == null)
        {
            Debug.Log("�G���[���������܂���");
            return null;
        }

        for(int i = 0; i < 25; i++)
        {
            string line = reader.ReadLine().Replace("\"", "");
            if (!Regex.IsMatch(line, "�I��|�M�L|�p�Y��"))
                continue;

            quizTemplates.Add(new QuizTemplate(line));
        }

        return quizTemplates;
    }

    public void Dispose()
    {

    }
}
