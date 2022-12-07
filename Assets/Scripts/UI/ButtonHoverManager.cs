using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    private string city;

    private string timeValue;

    private string country;

    private TMP_Text cityText;

    private TMP_Text countryText;

    private TMP_Text travelText;

    private GameObject g;

    private Vector3 originalScale;

    public void SetParameters(string city, string country, TMP_Text cityText, TMP_Text countryText, TMP_Text travelText, GameObject g)
    {
        this.city = city;
        this.country = country;
        this.cityText = cityText;
        this.countryText = countryText;
        this.travelText = travelText;
        this.g = g;
        ChangeTime();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        g.SetActive(true);
        cityText.text = city;
        countryText.text = country;
        travelText.text = timeValue;
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

    public void ChangeTime()
    {
        City from = GameDataManager.instance.GetCity(city);
        string tos;
        if (GameDataManager.instance.GetSite(GameDataManager.instance.GetCurrentMission().GetCurrentPlace()) != null)
            tos = GameDataManager.instance.GetSite(GameDataManager.instance.GetCurrentMission().GetCurrentPlace()).city;
        else
            tos = GameDataManager.instance.GetMuseum(GameDataManager.instance.GetCurrentMission().GetCurrentPlace()).city;
        City to = GameDataManager.instance.GetCity(tos);
        double dist = HarvesineDistance.HaversineDistance(new LatLng(from.latitude, from.longitude), new LatLng(to.latitude, to.longitude));
        double time = dist / 880;

        double minutes = time - Math.Truncate(time);

        double hours = time - minutes;

        minutes = (float)System.Math.Round(minutes, 2);

        minutes *= 60;

        minutes = Math.Floor(minutes);

        timeValue = hours + " h " + (minutes) + " min";
    }

    private void OnEnable()
    {
        originalScale = this.transform.localScale;
        CameraEdgeScroller.sizeChanged += ChangeSize;
        GameStatesManager.showTravelUI += ChangeTime;
    }

    private void OnDisable()
    {
        CameraEdgeScroller.sizeChanged -= ChangeSize;
        GameStatesManager.showTravelUI -= ChangeTime;
    }
}
