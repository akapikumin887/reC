using System;
using UnityEngine;

public interface IPresenter : IDisposable
{
    public void Initialize(Director d);
}
