using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillPanel : MonoBehaviour
{
    public Text one;
    public Text two;
    public Text dual;
    public Text magic;
    public Text archery;

    public Text heavy;
    public Text lightA;
    public Text cloth;

    public Text health;
    public Text mana;
    public Text healthRegen;
    public Text manaRegen;

    public Text points;

    public void UpdateAll(int pOH, int pTH, int pDW, int pMa, int pA, int pHA, int pLA, int pCA, int pH, int pM, int pHR, int pMR, int pPoints)
    {
        one.text = pOH.ToString();
        two.text = pTH.ToString();
        dual.text = pDW.ToString();
        magic.text = pMa.ToString();
        archery.text = pA.ToString();

        heavy.text = pHA.ToString();
        lightA.text = pLA.ToString();
        cloth.text = pCA.ToString();

        health.text = pH.ToString();
        mana.text = pM.ToString();
        healthRegen.text = pHR.ToString();
        manaRegen.text = pMR.ToString();

        points.text = pPoints.ToString();
    }                                                 
}
