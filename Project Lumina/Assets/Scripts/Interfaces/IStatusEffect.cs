using UnityEngine;

public interface IStatusEffect
{
    void Activate(GameObject target);
    void Deactivate(GameObject target);
}