using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public static class ScrollRectExtension
{

    public static void FocusElement(this ScrollRect @this, GameObject from, GameObject to)
    {
        if (@this.content.transform.Cast<Transform>().Contains(from.transform) == false) from = null;
        
        RectTransform scrollRect = @this.GetComponent<RectTransform>();
        RectTransform viewportRect = @this.viewport.GetComponent<RectTransform>();
        RectTransform toRect = to.GetComponent<RectTransform>();
        Vector3 scrollPosition = @this.content.transform.localPosition;
        
        if (from == null)   // No old focus -> Scroll to the top
        {
            Debug.Log("no From");
            
            scrollPosition.y = (-toRect.localPosition.y) - toRect.rect.height/2;
            // Manage scroll limit
            float scrollLimit = @this.content.rect.height - @this.viewport.rect.height;
            if (scrollPosition.y < 0) scrollPosition.y = 0;
            if (scrollPosition.y > scrollLimit) scrollPosition.y = scrollLimit;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(viewportRect, toRect.position, null))
        {
            Debug.Log("no move");
        }
        else
        {
            RectTransform fromRect = from?.GetComponent<RectTransform>();
            scrollPosition.y += fromRect.localPosition.y - toRect.localPosition.y;
        }
        


        @this.content.transform.localPosition = scrollPosition;
    }
    
}
