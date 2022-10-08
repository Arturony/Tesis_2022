using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvesineDistance
{
    //earth's radius in km
    private double R = 6371;

    public static double HaversineDistance(LatLng pos1, LatLng pos2)
    {
        var lat = (pos2.Latitude - pos1.Latitude).ToRadians();
        var lng = (pos2.Longitude - pos1.Longitude).ToRadians();
        var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                      Math.Cos(pos1.Latitude.ToRadians()) * Math.Cos(pos2.Latitude.ToRadians()) *
                      Math.Sin(lng / 2) * Math.Sin(lng / 2);
        var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
        return R * h2;
    }

    
}

public class LatLng
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public LatLng()
    {
    }

    public LatLng(double lat, double lng)
    {
        this.Latitude = lat;
        this.Longitude = lng;
    }
}

public static class NumericExtensions
{
    public static double ToRadians(this double val)
    {
        return (Math.PI / 180) * val;
    }
}