using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeScroller : MonoBehaviour
{
    [SerializeField]
    private float minPanSpeed;

    [SerializeField]
    private float maxPanSpeed;

    [SerializeField]
    private float panBorder;

    [SerializeField]
    private Vector2 panLimit;

    [SerializeField]
    private float scrollSpeed;

    [SerializeField]
    private float minY;

    [SerializeField]
    private float maxY;

    [SerializeField]
    private float minSize = 0.1f;

    [SerializeField]
    private float maxSize = 5;

    private bool active = false;

    private float prevSize;

    private Camera cam;

    public static Action<float, float, float> sizeChanged;

    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            Vector3 pos = transform.position;

            float panRange = maxSize - minSize;

            float panCorrectedValue = cam.orthographicSize - minSize;

            float panPercentage = (panCorrectedValue) / panRange;

            float panSpeed = Mathf.Clamp(panPercentage, 0.1f, 1f) * maxPanSpeed;

            if (Input.mousePosition.y >= Screen.height - panBorder)
            {
                pos.y += panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.y <= panBorder)
            {
                pos.y -= panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x <= panBorder)
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x >= Screen.width - panBorder)
            {
                pos.x += panSpeed * Time.deltaTime;
            }

            float range = maxSize - minSize;

            float correctedValue = cam.orthographicSize - minSize;

            float percentage = Mathf.Abs(((correctedValue) / range) - 1);

            Vector3 panLimitActual = panLimit * percentage;

            float scrol = Input.GetAxisRaw("Mouse ScrollWheel");
            float scrolAmmount =+ cam.orthographicSize + -scrol * scrollSpeed * 100f * Time.deltaTime;

            scrolAmmount = Mathf.Clamp(scrolAmmount, minSize, maxSize);

            Debug.Log(scrol);
            Debug.Log(scrolAmmount);
            Debug.Log(Time.deltaTime);

            pos.x = Mathf.Clamp(pos.x, -panLimitActual.x, panLimitActual.x);
            //pos.y = Mathf.Clamp(pos.y, minY, maxY);
            pos.y = Mathf.Clamp(pos.y, -panLimitActual.y, panLimitActual.y);

            transform.position = pos;

            cam.orthographicSize = scrolAmmount;

            if(prevSize != cam.orthographicSize)
            {
                sizeChanged?.Invoke(minSize, maxSize, cam.orthographicSize);
                prevSize = cam.orthographicSize;
            }
        }
    }

    private void setBoolean()
    {
        if (active)
            active = false;
        else
            active = true;
    }

    private void OnEnable()
    {
        GameStatesManager.activateCameraScrolling += setBoolean;
        cam = Camera.main;
        prevSize = cam.orthographicSize;
    }

    private void OnDisable()
    {
        GameStatesManager.activateCameraScrolling -= setBoolean;
    }
}
