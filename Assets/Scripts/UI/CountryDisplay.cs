using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountryDisplay : MonoBehaviour
{
    public Transform panel;

    public GameObject detailPanel;

    public GameObject buttonPrefab;

    public static event Func<List<string>> getCountryNames;

    //iterate through all the countries and create a button for each of them
    public void InitiateButtons()
    {
        List<string> countries = getCountryNames?.Invoke();

        foreach(string s in countries)
        {
            GameObject g = Instantiate(buttonPrefab, panel);
            g.GetComponent<TMP_Text>().text = s;

            //set the event to show the detail
        }
    }

    public void ShowDetailPanel()
    {


        //change name



        //change description



        //change image



        //set the button event to show cities
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }
}
