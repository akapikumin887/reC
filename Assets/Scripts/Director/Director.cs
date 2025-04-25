using UnityEngine;

public class Director : MonoBehaviour
{
    private IPresenter correntPresenter;

    [SerializeField]
    private TitlePresenter titlePresenter;
    public TitlePresenter _TitlePresenter => titlePresenter;

    [SerializeField]
    private GamePresenter gamePresenter;
    public GamePresenter _GamePresenter => gamePresenter;

    [SerializeField]
    private ResultPresenter resultPresenter;
    public ResultPresenter _ResultPresenter => resultPresenter;

    private void Start()
    {
        ChangePresenter(titlePresenter);
    }

    public void ChangePresenter(IPresenter presenter)
    {
        correntPresenter = presenter;
        correntPresenter.Initialize(this);
    }
}
