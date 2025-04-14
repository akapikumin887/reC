using System;
using UnityEngine;
using UnityEngine.UI;

public class TitleView : MonoBehaviour, IView
{
    [field: SerializeField]
    public GameObject self {  get; private set; }

    [field: SerializeField]
    public Button startButton {  get; private set; }

    public TitleView()
    {

    }

    public void Initialize()
    {

    }

    public void Uninitialize()
    {
        if (self.activeSelf)
            self.SetActive(!self.activeSelf);
    }

    public void TransitionGameScreen(GameObject obj)
    {
        // �J�ڂ������Active�̐؂�ւ����������A���Ɉړ����ď�������
        obj.SetActive(!obj.activeSelf);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        // ����͑Ή�����
        if (disposing)
        {

        }
    }
}
