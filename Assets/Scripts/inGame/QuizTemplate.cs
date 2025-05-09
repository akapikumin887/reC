using System.Collections.Generic;

public class QuizTemplate
{
    public int number { get; }
    public string format {  get; }
    public string text {  get; }
    public bool used {  get; }
    public int imageCount { get; }
    public List<int> answerImageCount { get; } = new List<int>();
    public List<UseImageMap> usedImageList { get; }

    private QuizTemplate() {}

    public QuizTemplate(string line)
    {
        // セル1行を取得し、中を確認して要素に代入する
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

        SetImages();
    }

    public void SetImages()
    {
        usedImageList.Clear();

        if (imageCount == 9)
        {
            for (int i = 1; i <= 9; i++)
            {
                // 一枚絵の画像問題なので1から9をそのまま格納する
                usedImageList.Add(new UseImageMap(i, answerImageCount.Contains(i)));
            }
        }
        else if (imageCount < 9)
        {
            for (int i = 1; i <= 9; i++)
            {
                // 同じ画像を複数選択肢に含む問題なので、被りありでランダムに格納する
                System.Random random = new();
                int num = random.Next(1, imageCount + 1);
                usedImageList.Add(new UseImageMap(num, answerImageCount.Contains(num)));
            }
        }
        else if (answerImageCount.Count < imageCount)
        {
            while (usedImageList.Count < 9)
            {
                // 選択肢が表示画像数より多いので、被りなしでランダムに格納する
                System.Random random = new();
                int num = random.Next(1, imageCount + 1);

                bool isAdd = true;
                for (int i = 0; i < usedImageList.Count; i++)
                {
                    if (usedImageList[i].useImageNum == num)
                        isAdd = false;
                }
                if (isAdd)
                    usedImageList.Add(new UseImageMap(num, answerImageCount.Contains(num)));
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
