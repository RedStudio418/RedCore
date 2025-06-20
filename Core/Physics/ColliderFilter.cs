using UnityEngine;

public class ColliderFilter : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;

    public bool Validate(Collider arg0)
        => ((1 << arg0.gameObject.layer) & _layerMask) != 0;

}
