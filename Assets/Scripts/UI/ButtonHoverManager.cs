using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private string city;

    private string country;

    private TMP_Text cityText;

    private TMP_Text countryText;

    private GameObject g;

    private Vector3 originalScale;

    public void SetParameters(string city, string country, TMP_Text cityText, TMP_Text countryText, GameObject g)
    {
        this.city = city;
        this.country = country;
        this.cityText = cityText;
        this.countryText = countryText;
        this.g = g;
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        g.SetActive(true);
        cityText.text = city;
        countryText.text = country;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        g.SetActive(false);
    }

    void ChangeSize(float minSize, float maxSize, float currentSize)
    {
        float range = maxSize - minSize;

        float correctedValue = currentSize - minSize;

        float percentage = Mathf.Clamp(Mathf.Abs((correctedValue / range) - 1), 0f, 0.85f);
        this.transform.localScale = originalScale - originalScale * percentage;
    }

    private void OnEnable()
    {
        originalScale = this.transform.localScale;
        CameraEdgeScroller.sizeChanged += ChangeSize;
    }

    private void OnDisable()
    {
        CameraEdgeScroller.sizeChanged -= ChangeSize;
    }
}
