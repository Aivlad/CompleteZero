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
        effectBleeding.SetActive(true);
    }

    public void UpdateEffectBleeding()
    {
        effectBleeding.GetComponent<EffectBleeding>().EffectUpdate();
    }

    public bool IsEffectBleedingActicated()
    {
        return effectBleeding.activeInHierarchy;
    }

    #endregion

    //------------------- отравление
    #region poisoning
    public GameObject effectPoisoning;
    public void CallEffectPoisoning()
    {
        effectPoisoning.SetActive(true);
    }

    public void UpdateEffectPoisoning()
    {
        effectPoisoning.GetComponent<EffectPoisoning>().EffectUpdate();
    }

    public bool IsEffectPoisoningActicated()
    {
        return effectPoisoning.activeInHierarchy;
    }
    #endregion

    //------------------- свет
    #region light
    public GameObject effectLight;

    public void CallEffectLight()
    {
        effectLight.SetActive(true);
    }

    public void UpdateEffectLight()
    {
        effectLight.GetComponent<EffectLight>().EffectUpdate();
    }

    public bool IsEffectLightActivated()
    {
        return effectLight.activeInHierarchy;
    }
    #endregion

    //------------------- огонь
    #region fire
    public GameObject effectFire;

    public void CallEffectFire()
    {
        effectFire.SetActive(true);
    }

    public void UpdateEffectFire()
    {
        effectFire.GetComponent<EffectFire>().EffectUpdate();
    }

    public bool IsEffectFireActivated()
    {
        return effectFire.activeInHierarchy;
    }
    #endregion

}
