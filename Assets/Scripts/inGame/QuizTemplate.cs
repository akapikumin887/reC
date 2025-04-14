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
        // ƒZƒ‹1s‚ğæ“¾‚µA’†‚ğŠm”F‚µ‚Ä—v‘f‚É‘ã“ü‚·‚é
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
                // ˆê–‡ŠG‚Ì‰æ‘œ–â‘è‚È‚Ì‚Å1‚©‚ç9‚ğ‚»‚Ì‚Ü‚ÜŠi”[‚·‚é
                usedImageList.Add(i);
            }
            else if (imageCount < 9)
            {
                // “¯‚¶‰æ‘œ‚ğ•¡”‘I‘ğˆ‚ÉŠÜ‚Ş–â‘è‚È‚Ì‚ÅA”í‚è‚ ‚è‚Åƒ‰ƒ“ƒ_ƒ€‚ÉŠi”[‚·‚é
                System.Random random = new();
                int num = random.Next(1, imageCount + 1);
                usedImageList.Add(num);
            }
            else if (answerImageCount < imageCount)
            {
                // ‘I‘ğˆ‚ª•\¦‰æ‘œ”‚æ‚è‘½‚¢‚Ì‚ÅA”í‚è‚È‚µ‚Åƒ‰ƒ“ƒ_ƒ€‚ÉŠi”[‚·‚é
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
