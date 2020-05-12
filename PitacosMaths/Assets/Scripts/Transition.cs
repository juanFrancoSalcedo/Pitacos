using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public DoAnimationController animator;
    public GameObject image;
    public static Transition Instance { get; private set; }
    bool zoomIn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null);
        }

        if (Instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

      #if UNITY_EDITOR
        //DataSystem.DeleteData();
      # endif
    }

    public void Zoom()
    {
        zoomIn = !zoomIn;
        if (zoomIn)
        {
            ZoomAnimation(true);
            Invoke("Zoom",1.3f);
        }
        else
        {
            ZoomAnimation(false);
        }
    }

    private void ZoomAnimation (bool _argZoomIn)
    {
        if (_argZoomIn)
        {
            animator.targetScale = new Vector3(40, 40, 0);
            Invoke("ShowObjectEnd", animator.timeAnimation*0.9f);
        }
        else
        {
            image.gameObject.SetActive(false);
            animator.targetScale = new Vector3(2000, 2000, 0);
        }
        animator.ActiveAnimation();
    }

    private void ShowObjectEnd()
    {
        image.gameObject.SetActive(true);
    }
}
