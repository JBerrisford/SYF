using UnityEngine;
using System.Collections;

public interface ITarget : IInit
{
    float BasePriority
    { get; set; }
    float TargetPriority
    { get; set; }
    bool IsActive
    { get; set; }
    GameObject Highlight
    { get; set; }

    void Activate();
    void Deactivate();

    ITarget GetTarget();
    T GetInterface<T>();
    IEnumerator Tick();

    void ToggleHighlight(bool pToggle);
    void UpdateTargetPriority(float pDistance);
    GameObject GetGO();
}

