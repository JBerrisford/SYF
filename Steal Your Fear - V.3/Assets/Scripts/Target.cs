using UnityEngine;
using System.Collections;

[System.Serializable]
public class Target : MonoBehaviour, ITarget
{
    private float basePriority;
    public float BasePriority
    { get { return basePriority; } set { basePriority = value; } }
    private float targetPriority;
    public float TargetPriority
    { get { return targetPriority; } set { targetPriority = value; } }

    bool isActive;
    public bool IsActive
    { get { return isActive; } set { isActive = value; } }

    [SerializeField]
    GameObject highlight;
    public GameObject Highlight
    { get { return highlight; } set { highlight = value; } }

    public virtual void Init()
    {
        if (transform.Find("Highlight").gameObject == null)
        {
            GameObject go = (GameObject)Instantiate(Resources.Load(CS_Utility.highlight));
            highlight = go;
        }
        else
        {
            highlight = transform.Find("Highlight").gameObject;
        }

        //ResetClass();
    }

    public virtual void OnTriggerEnter(Collider pCollision)
    {
        if(pCollision.gameObject.tag == "Player")
        {
            Activate();
        }
    }

    public virtual void OnTriggerExit(Collider pCollision)
    {
        if (pCollision.gameObject.tag == "Player")
        {
            Deactivate();
        }
    }

    public virtual void ResetClass()
    {
        isActive = false;
        ToggleHighlight(false);
    }

    public void Activate()
    {
        isActive = true;
        StartCoroutine(Tick());
    }

    public void Deactivate()
    {
        isActive = false;
        //ResetClass();
    }

    public virtual IEnumerator Tick()
    {
        while(isActive)
        {
            UpdateTargetPriority(CS_Utility.GetDistance(this.gameObject));
            yield return null;
        }

        ResetClass();
        yield break;
    }

    public ITarget GetTarget()
    {
        return this;
    }

    public T GetInterface<T>()
    {
        return GetComponent<T>();
    }

    public void ToggleHighlight(bool pToggle)
    {
        if(highlight != null)
            highlight.SetActive(pToggle);
    }

    public void UpdateTargetPriority(float pDistance)
    {
        targetPriority = basePriority - pDistance;
    }

    public GameObject GetGO()
    {
        return gameObject;
    }
}
