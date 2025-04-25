using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class GamePresenter : MonoBehaviour, IPresenter
{
    [SerializeField]
    private SpreadSheetData spreadSheetData;

    private Director director;

    [SerializeField]
    private GameView gameView;
    public GameView _GameView => gameView;

    private GameModel gameModel;

    public void Initialize(Director d)
    {
        director = d;

        gameModel = new();

        gameModel.Initialize(spreadSheetData.quizTemplates);
        gameView.Initialize(spreadSheetData.quizTemplates, gameModel.quizNum, gameModel.nowQuizCount);

        for (int i = 0; i < gameView._QuizChoices.Length; i++)
        {
            int index = i;
            gameView._QuizChoices[index].OnClickAsObservable().Subscribe(_ =>
            {
                gameView.ExchangeCheckmark(index).Forget();
                gameModel.SelectImages(index);
            }).AddTo(gameObject);
        }

        // ³Œë”»’è
        gameView._Judge.OnClickAsObservable().Subscribe(_ =>
        {
            gameModel.JudgeQuiz(spreadSheetData.quizTemplates);
        }).AddTo(gameObject);

        // •s³‰ðˆ—
        gameModel.wrongSubject.Subscribe(_ => gameView.WrongAnswer()).AddTo(gameObject);

        // ³‰ðˆ—
        gameModel.nextQuizSubject.Subscribe(model =>
        {
            gameModel.CorrectAnswer();
            gameView.CorrectAnswer(model, spreadSheetData.quizTemplates[gameModel.quizNum[gameModel.nowQuizCount]]);
        }).AddTo(gameObject);

        // ƒŠƒUƒ‹ƒg‘JˆÚ
        gameModel.resultTransitionSubject.Subscribe(model =>
        {
            gameView.TransitionScene(false);
            director.ChangePresenter(director._ResultPresenter);
        }).AddTo(gameObject);
    }

    public void Dispose()
    {

    }
}
