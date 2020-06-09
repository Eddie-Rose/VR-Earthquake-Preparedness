using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Provided by the previous project. 
 */
public class MainPanel : MonoBehaviour {
    public Button PlayButton;
    public Dropdown EarthquakeDropdown;
    public Dropdown MagnitudeDropdown;
    public Dropdown LocationDropdown;
    public Dropdown DurationDropdown;
    private string earthquake;
    private string location;
    private void Start() {
        StateController.Label = PlayButton.GetComponentInChildren<Text>();
        EarthquakeDropdown.options.Clear();
        foreach(string earthquake in EarthquakeReader.FindEarthquakes()){
            EarthquakeDropdown.options.Add(new Dropdown.OptionData(earthquake));
        }
        EarthquakeDropdown.RefreshShownValue();
        OnEarthquakeChanged();
    }
    private void ReImport() {
        EarthquakeReader.CSVPath = earthquake+"/"+location+".csv";
        EarthquakeReader.Import();
    }
    public void OnButtonPress(){
        StateController.Next();
    }
    public void OnEarthquakeChanged(){
        earthquake = EarthquakeDropdown.captionText.text;
        LocationDropdown.options.Clear();
        foreach (string location in EarthquakeReader.FindLocations(earthquake)){
            LocationDropdown.options.Add(new Dropdown.OptionData(location));
        }
        LocationDropdown.RefreshShownValue();
        OnLocationChanged();
    }
    public void OnLocationChanged(){
        location = LocationDropdown.captionText.text;
        ReImport();
    }
    public void OnMagnitudeChanged(){

    }
    public void OnDurationChanged(){

    }
}