using UnityEngine;
using System.Collections;

[System.Serializable]
public class CS_Stat // Health, Mana and Fear Class
{
    Player_Base player;

    public delegate void Stat_Change(float pCurrent, float pMax);
    public Stat_Change update;

    public float baseStat;
    public float bonus;
    public float max;
    public float current;

    // Make Regen a extension by inheritance
    public float regenBase;
    public float regenBonus;
    public float regenCurrent
    {
        get
        {
            return regenBase + (regenBase / 100.0f * regenBonus);
        }
    }
    public bool isRegening;

    // Player Constuctor
    public CS_Stat(float pBase, float pBonus, float pRegenBase, float pRegenBonus, Stat_Change pNewUpdate)
    {
        baseStat = pBase;
        bonus = pBonus;
        max = baseStat + (baseStat / 100.0f * pBonus);
        current = max;

        update = pNewUpdate;

        regenBase = pRegenBase;
        regenBonus = pRegenBonus;
        isRegening = false;
    }
    // AI Constuctor
    public CS_Stat(float pBase, float pBonus, float pRegenBase, float pRegenBonus)
    {
        baseStat = pBase;
        bonus = pBonus;
        max = baseStat + (baseStat / 100.0f) * pBonus;
        current = max;

        update = null;

        regenBase = pRegenBase;
        regenBonus = pRegenBonus;
        isRegening = false;
    }

    public void UpdateStat(float pBase, float pBonus, float pRegenBase, float pRegenBonus)
    {
        baseStat = pBase;
        regenBase = pRegenBase;
        regenBonus = pRegenBonus;
        Mathf.Clamp(baseStat, 1, max);
        SetBonus(pBonus);
    }

    public void SetBonus(float pBonus)
    {
        bonus = pBonus;
        SetMax();
    }
    void SetMax()
    {
        float old = max;
        max = baseStat + (baseStat / 100.0f) * bonus;
        ModCurrent(old, max);
    }
    void ModCurrent(float pOld, float pNew)
    {
        current -= pOld - pNew;
        ChangeMade();
    }
    public IEnumerator Regen()
    {
        isRegening = true;

        while (isRegening)
        {
            if (current >= max)
            {
                current = max;
                isRegening = false;
                yield return new WaitForSeconds(0.5f);
                ChangeMade();
                break;
            }

            current += regenCurrent / 4.0f;
            yield return new WaitForSeconds(0.5f);
            ChangeMade();
        }

        yield break;
    }

    public bool RegenCheck()
    {
        if(!isRegening && current < max && regenCurrent > 0.0f)
        {
            return true;
        }

        return false;
    }

    public void ChangeMade()
    {
        if(update != null)
        {
            update(current, max);
        }
    }

    public bool ReduceStat(float pMod)
    {
        if (current - pMod > 0.0f)
        {
            current -= pMod;
            ChangeMade();

            return true;
        }

        return false;
    }
}
