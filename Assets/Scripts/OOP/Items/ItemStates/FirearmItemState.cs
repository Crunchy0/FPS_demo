using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirearmItemState : IItemState
{
    public int AmmoLeft { get => _ammoLeft; set => _ammoLeft = (value < 0) ? 0 : value; }

    private int _ammoLeft;

    public FirearmItemState(int startAmmo = 0)
    {
        _ammoLeft = startAmmo;
    }
}
