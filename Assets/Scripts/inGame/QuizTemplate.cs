using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTemplate
{
    public int number { get; }
    public string format {  get; }
    public string text {  get; }
    public bool used {  get; }
    public int imageCount { get; }
    public int answerImageCount { get; }
    public List<int> usedImageList { get; }

    [field: SerializeField]
    public Image[] images { get; private set; } = new Image[9];

    private QuizTemplate()
    {
        
    }

    public QuizTemplate(string line)
    {
        // �Z��1�s���擾���A�����m�F���ėv�f�ɑ������
        string[] words = line.Split(',');

        number = int.Parse(words[0] == "" ? "0" : words[0]);
        format = words[1];
        text = words[2];
        used = words[3] == "t";
        imageCount = int.Parse(words[4] == "" ? "0" : words[4]);
        answerImageCount = int.Parse(words[5] == "" ? "0" : words[5]);
        usedImageList = new List<int>();

        if (imageCount == 0)
            return;

        for(int i = 1; i <= 9; i++)
        {
            if (imageCount == 9)
            {
                // �ꖇ�G�̉摜���Ȃ̂�1����9�����̂܂܊i�[����
                usedImageList.Add(i);
            }
            else if (imageCount < 9)
            {
                // �����摜�𕡐��I�����Ɋ܂ޖ��Ȃ̂ŁA��肠��Ń����_���Ɋi�[����
                System.Random random = new();
                int num = random.Next(1, imageCount + 1);
                usedImageList.Add(num);
            }
            else if (answerImageCount < imageCount)
            {
                // �I�������\���摜����葽���̂ŁA���Ȃ��Ń����_���Ɋi�[����
                System.Random random = new();
                int num = random.Next(1, imageCount + 1);

                if (!usedImageList.Contains(num))
                {
                    usedImageList.Add(num);
                }
            }
        }

    }
}
