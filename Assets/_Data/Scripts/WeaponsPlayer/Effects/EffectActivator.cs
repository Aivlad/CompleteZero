using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectActivator : MonoBehaviour
{
    //------------------- кровотечение
    #region bleeding

    public GameObject effectBleeding;

    public void CallEffectBleeding()
    {
        if (effectBleeding != null)
            effectBleeding.SetActive(true);
    }

    public void UpdateEffectBleeding()
    {
        if (effectBleeding != null)
            effectBleeding.GetComponent<EffectBleeding>().EffectUpdate();
    }

    public bool IsEffectBleedingActicated()
    {
        if (effectBleeding == null)
            return false;
        return effectBleeding.activeInHierarchy;
    }

    #endregion

    //------------------- отравление
    #region poisoning
    public GameObject effectPoisoning;
    public void CallEffectPoisoning()
    {
        if (effectPoisoning != null)
            effectPoisoning.SetActive(true);
    }

    public void UpdateEffectPoisoning()
    {
        if (effectPoisoning != null)
            effectPoisoning.GetComponent<EffectPoisoning>().EffectUpdate();
    }

    public bool IsEffectPoisoningActicated()
    {
        if (effectPoisoning == null)
            return false;
        return effectPoisoning.activeInHierarchy;
    }
    #endregion

    //------------------- огонь
    #region fire
    public GameObject effectFire;

    public void CallEffectFire()
    {
        if (effectFire != null)
            effectFire.SetActive(true);
    }

    public void UpdateEffectFire()
    {
        if (effectFire != null)
            effectFire.GetComponent<EffectFire>().EffectUpdate();
    }

    public bool IsEffectFireActivated()
    {
        if (effectFire == null)
            return false;
        return effectFire.activeInHierarchy;
    }
    #endregion

}
