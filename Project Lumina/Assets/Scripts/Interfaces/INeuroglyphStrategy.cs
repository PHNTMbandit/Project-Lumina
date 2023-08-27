using UnityEngine;

public interface INeuroglyphStrategy
{
    void Activate(GameObject target);
    void Deactivate(GameObject target);
}