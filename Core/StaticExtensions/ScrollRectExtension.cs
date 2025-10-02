using UnityEngine;
using UnityEngine.UI;

public static class ScrollRectExtension
{

    public static void FocusElement(this ScrollRect @this, GameObject from, GameObject to)
    {
        RectTransform fromRect = from.GetComponent<RectTransform>();
        RectTransform toRect = to.GetComponent<RectTransform>();
        
        Vector3 scrollPosition = @this.content.transform.localPosition;
        
        scrollPosition.y = (-toRect.localPosition.y) - toRect.rect.height/2;
        
        // Manage scroll limit
        float scrollLimit = @this.content.rect.height - @this.viewport.rect.height;
        if (scrollPosition.y < 0) scrollPosition.y = 0;
        if (scrollPosition.y > scrollLimit) scrollPosition.y = scrollLimit;


        @this.content.transform.localPosition = scrollPosition;
    }
    
}
