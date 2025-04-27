using System.Collections.Generic;
using UnityEngine;

public class ResultModel : IModel
{
    public void Initialize()
    {

    }

    public void ResetImages(List<QuizTemplate> quizTemplates)
    {
        foreach (QuizTemplate template in quizTemplates)
        {
            template.SetImages();
        }
    }

    public void Dispose()
    {

    }
}
