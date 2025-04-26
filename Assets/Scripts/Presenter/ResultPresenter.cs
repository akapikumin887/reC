using UnityEngine;

public class ResultPresenter : MonoBehaviour, IPresenter
{
    private Director director;

    [SerializeField]
    private ResultView resultView;
    private ResultModel resultModel;

    public void Initialize(Director d)
    {
        resultModel = new();
        resultView.Initialize();
    }

    public void Dispose()
    {

    }

}
