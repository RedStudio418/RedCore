using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extention
{

    public static bool Approximately(Vector3 a, Vector3 b) => 
        Mathf.Approximately(a.x, b.x) && 
        Mathf.Approximately(a.y, b.y) && 
        Mathf.Approximately(a.z, b.z);
    
    public static bool ApproximatelyEqualTo(this Vector3 a, Vector3 b) => 
        Mathf.Approximately(a.x, b.x) && 
        Mathf.Approximately(a.y, b.y) && 
        Mathf.Approximately(a.z, b.z);

    public static bool ApproximatelyEqualTo(this Vector2 a, Vector2 b) =>
        Mathf.Approximately(a.x, b.x) &&
        Mathf.Approximately(a.y, b.y);

    public static Vector3 SetX(this Vector3 @this, float newValue) => new Vector3(newValue, @this.y, @this.z);
    public static Vector3 SetY(this Vector3 @this, float newValue) => new Vector3(@this.x, newValue, @this.z);
    public static Vector3 SetZ(this Vector3 @this, float newValue) => new Vector3(@this.x, @this.y, newValue);

}
