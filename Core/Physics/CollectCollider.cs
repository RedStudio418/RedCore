using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
using Sirenix.OdinInspector;
using UnityEngine;

public class CollectCollider : MonoBehaviour
{
    [SerializeField] private PhysicEvent3D _physic;
    [ShowInInspector] public HashSet<Collider> Collection { get; private set; }
    [SerializeField, InfoBox("Optionnal")] ColliderFilter _filter;
    
    int _nullCount;
    
    public IEnumerable<Collider> CollectionProtected()
    {
        _nullCount = 0;
        foreach(var el in Collection)
        {
            if (el == null)
            {
                _nullCount++;
                continue;   // Ignore destroyed GO for now, will need a cleanup routine
            }
            yield return el;
        }
    }

    public Collider GetNearestElement(Transform source)
        => CollectionProtected().Aggregate((a, b) =>
            Vector3.SqrMagnitude(source.position - a.transform.position) <
            Vector3.SqrMagnitude(source.position - b.transform.position)
                ? a : b);
    public T GetNearestElement<T>(Transform source) => GetNearestElement(source).GetComponent<T>();
    
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

        CleanUpRoutineAsync();
    }

    async void CleanUpRoutineAsync()
    {
        while (true)
        {
            if (_nullCount > 3)
            {
                Collection.RemoveWhere(i => i == null);
                _nullCount = 0;
            }

            await UniTask.WaitForSeconds(1 );
        }
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
        if (_filter?.Validate(arg0) == false) return;
        Collection.Add(arg0);
    }
}
