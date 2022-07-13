using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScalingAnimation : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("ScaleAnimation", 0, 1f);
    }


    private void ScaleAnimation()
    {
        transform.DOScale(0.6f, 0.5f).OnComplete(() => transform.DOScale(0.5f, 0.5f));
    }
}
