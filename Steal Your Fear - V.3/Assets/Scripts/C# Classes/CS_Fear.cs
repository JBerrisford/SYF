using UnityEngine;
using System.Collections;

[System.Serializable]
public class CS_Fear
{
    public delegate void Stat_Change(float pCurrent, float pMax);
    public Stat_Change update;

    public float current;
    public float max;

    public bool alive;

    public CS_Fear(float pCurrent, float pMax, Stat_Change pUpdate)
    {
        current = pCurrent;
        max = pMax;
        update = pUpdate;
        alive = true;
    }

    public void ModFear(float pValue, bool pAdd)
    {
        current = (pAdd) ? current + pValue : current - pValue;
        ChangeMade();
    }

    public IEnumerator PassiveFear()
    {
        while (alive)
        {
            yield return new WaitForSeconds(1.0f);
            current += 0.01f;
            ChangeMade();
        }

        yield return null;
    }

    public void ChangeMade()
    {
        current = Mathf.Clamp(current, 0.0f, 100.0f);

        if (update != null)
        {
            update(current, max);
        }
    }
}
