using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutPhoto : MonoBehaviour
{

    public Image photo;

    public float fadeOutTime;

    public GameEvent onFadeOutEvent;

    private PhotoScene scene;

    public Transform cameraPos;

    private void Awake()
    {
        scene = GetComponent<PhotoScene>();
    }
    private void Start()
    {
        StartCoroutine(FadeOutRoutine());
    }
    private IEnumerator FadeOutRoutine()
    {
        StageManager.instance.SetMainPlayerView(false);
        StageManager.activeCamera = cameraPos.gameObject;

        Color originalColor = photo.color;
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            photo.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeOutTime));

            yield return null;
        }
        onFadeOutEvent.Raise();
        StageManager.instance.TeleportPlayer(cameraPos.position, cameraPos.rotation);
        scene.SetSceneObjects(false);

    }
}
