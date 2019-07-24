using UnityEngine;
using System.Collections;

public class MaskControl : MonoBehaviour
{
    public Material[] myMaterials;

    public void Init()
    {
        myMaterials = gameObject.GetComponent<Renderer>().materials;
    }

    public void UpdateMask(float pValue)
    {
        pValue /= 100.0f;

        foreach(Material mat in myMaterials)
        {
            mat.SetFloat("_Blend", pValue);
        }
    }
}
