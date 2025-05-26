using System;
using System.Collections;
using System.Collections.Generic;
using jy_util;
using UnityEngine;

public class _GameAssets : MonoSingleton<_GameAssets>
{
    public static readonly string PlayerTag = "Player";

    public Action<IDamageable> OnGunAimAtAction;
    public LayerMask gunAimIgnoreLayermask;
}
