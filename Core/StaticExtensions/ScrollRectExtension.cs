using UnityEngine;
using UnityEngine.UI;

public static class ScrollRectExtension
{

    public static void FocusElement(this ScrollRect @this, GameObject from, GameObject to)
    {
        var deltaY = from.transform.position.y - to.transform.position.y;
        @this.content.transform.position = new Vector3(
            @this.content.transform.position.x, 
            @this.content.transform.position.y + deltaY, 
            @this.content.transform.position.z);
    }
    
}
