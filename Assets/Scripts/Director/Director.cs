using UnityEngine;

public class Director : MonoBehaviour
{
    public IPresenter correntPresenter {  get; private set; }

    [field: SerializeField]
    public TitlePresenter titlePresenter { get; private set; }

    [field: SerializeField]
    public GamePresenter gamePresenter { get; private set; }

    [field: SerializeField]
    public ResultPresenter resultPresenter { get; private set; }

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
