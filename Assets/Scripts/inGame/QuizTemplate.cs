using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuizTemplate
{
    public int number { get; }
    public string format {  get; }
    public string text {  get; }
    public bool used {  get; }
    public int imageCount { get; }
    public List<int> answerImageCount { get; } = new List<int>();
    public List<UseImageMap> usedImageList { get; }

    [field: SerializeField]
    public Image[] images { get; private set; } = new Image[9];

    private QuizTemplate() {}

    public QuizTemplate(string line)
    {
        // �Z��1�s���擾���A�����m�F���ėv�f�ɑ������
        string[] words = line.Split(',');

        number = int.Parse(words[0] == "" ? "0" : words[0]);
        format = words[1];
        text = words[2];
        used = words[3] == "t";
        imageCount = int.Parse(words[4] == "" ? "0" : words[4]);

        string[] nums = words[5].Split(' ');
        foreach (string num in nums)
        {
            if (num == string.Empty)
                break;
            answerImageCount.Add(int.Parse(num));
        }

        usedImageList = new List<UseImageMap>();

        if (imageCount == 0)
            return;

        if (imageCount == 9)
        {
            for (int i = 1; i <= 9; i++)
            {
                // �ꖇ�G�̉摜���Ȃ̂�1����9�����̂܂܊i�[����
                usedImageList.Add(new UseImageMap(i, answerImageCount.Contains(i)));
            }
        }
        else if (imageCount < 9)
        {
            for (int i = 1; i <= 9; i++)
            {
                // �����摜�𕡐��I�����Ɋ܂ޖ��Ȃ̂ŁA��肠��Ń����_���Ɋi�[����
                System.Random random = new();
                int num = random.Next(1, imageCount + 1);
                usedImageList.Add(new UseImageMap(num, answerImageCount.Contains(num)));
            }
        }
        else if (answerImageCount.Count < imageCount)
        {
            for (int i = 1; i <= 9; i++)
            {
                // �I�������\���摜����葽���̂ŁA���Ȃ��Ń����_���Ɋi�[����
                System.Random random = new();
                int num = random.Next(1, imageCount + 1);

                for (int j = 0; j < usedImageList.Count; j++)
                {
                    if (usedImageList[j].useImageNum == num)
                        continue;

                    usedImageList.Add(new UseImageMap(i, answerImageCount.Contains(i)));
                }
            }

        }

    }

    public class UseImageMap
    {
        public int  useImageNum { get; private set; }
        public bool isCorrect {  get; private set; }

        private UseImageMap() {}

        public UseImageMap(int useImageNum, bool isCorrect)
        {
            this.useImageNum = useImageNum;
            this.isCorrect = isCorrect;
        }
    }
}
