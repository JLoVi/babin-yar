using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeItem : MonoBehaviour
{
    
    public int id;
    public GameObject scrollPanel;
    public NarrativeDataObject narrativeData;

    public void SetScrollRects()
    {
        CameraMoveScrollController.scrollcanvas = scrollPanel.GetComponent<ScrollRect>();
        CameraMoveScrollController.photoFader = scrollPanel.GetComponent<ShowPhoto>();

    }
}



