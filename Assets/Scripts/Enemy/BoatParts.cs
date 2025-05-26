using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Outline))]
public class BoatParts : MonoBehaviour, IDamageable
{
    [SerializeField] private Outline outline;
    [SerializeField] int damageMultiplier = 1;
    [SerializeField] FeedBackManager feedBackManager;
    public Action<int> tookDamage;
    public void SetOutlineStatus(bool show)
    {
        outline.enabled = show;
    }

    public void TakeDamage(int damage)
    {
        feedBackManager?.PlayFeedback();
        tookDamage?.Invoke(damage * damageMultiplier);
    }

    void Start()
    {
        _GameAssets.Instance.OnGunAimAtAction += OnGunAimAtAciton;

        if (!outline) outline = GetComponent<Outline>();
        SetOutlineStatus(false);
    }

    void OnDisable()
    {
        _GameAssets.Instance.OnGunAimAtAction -= OnGunAimAtAciton;
    }

    void OnGunAimAtAciton(IDamageable damageable)
    {
        if ((IDamageable)this == damageable)
        {
            //this object is in Aim
            SetOutlineStatus(true);
        }
        else
        {
            
            SetOutlineStatus(false);
        }
    }

}
