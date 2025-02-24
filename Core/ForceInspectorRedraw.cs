using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ForceInspectorRedraw : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] private bool _forceRedraw;
    void Update()
    {
        if(_forceRedraw) UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
    }
#endif
}
