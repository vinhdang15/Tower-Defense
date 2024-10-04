using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform bar;
    float sizeNormalized = 1f;
    void Start()
    {
        bar = transform.Find("Bar");
    }

    public void Resize(float MaxHp, float damage)
    {
        sizeNormalized -= (float)damage / MaxHp;
        if (sizeNormalized <= 0) sizeNormalized = 0;
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void Reset()
    {
        sizeNormalized = 1;
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
