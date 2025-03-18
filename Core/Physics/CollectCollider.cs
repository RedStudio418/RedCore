using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class CollectCollider : MonoBehaviour
{
    [SerializeField] private PhysicEvent3D _physic;
    
    [ShowInInspector] public HashSet<Collider> Collection { get; private set; }

    public IEnumerable<Collider> CollectionProtected()
    {
        foreach(var el in Collection)
        {
            if (el == null) continue;   // Ignore destroyed GO for now, will need a cleanup routine
            yield return el;
        }
    }

    public bool HasADetection => CollectionProtected().Any();
    
    private void Reset()
    {
        _physic = GetComponent<PhysicEvent3D>();
    }

    private void Awake()
    {
        Collection = new();
    }

    private void Start()
    {
        _physic.TriggerEnter3D += TryAddCollider;
        _physic.TriggerExit3D += TryRemoveCollider;
    }

    private void OnDestroy()
    {
        _physic.TriggerEnter3D -= TryAddCollider;
        _physic.TriggerExit3D -= TryRemoveCollider;
    }

    private void TryRemoveCollider(Collider arg0)
    {
        Collection.Remove(arg0);
    }

    private void TryAddCollider(Collider arg0)
    {
        Collection.Add(arg0);
    }
}
