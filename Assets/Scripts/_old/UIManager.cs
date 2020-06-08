using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject notification;
    public GameObject eventPopup;
    public GameObject testimonyPopup;

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateNotification()
    {
        StartCoroutine(DisplayPopupMessage(notification));
        notification.GetComponentInChildren<Text>().text = "Notification" + '\n' + '\n' + "Photographer:" + '\n' + "Event:" + '\n' + "Location:" + '\n' + "Follow Path:";
    }

    public IEnumerator DisplayPopupMessage(GameObject popupType)
    {
        yield return new WaitForSeconds(3f);
        GameObject m_notif = Instantiate(popupType, popupPanel.transform);
    }
}
