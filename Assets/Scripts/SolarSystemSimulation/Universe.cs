using System.Collections;
using System.Collections.Generic;
using System;  // For DateTime
using UnityEngine;

public class Universe : MonoBehaviour {

    public float distanceScale;

    public float dayTimeStep;
    public DateTime georgianDate;
    public double julianDate;
    public double julianCenturiesSinceEpoch;

    readonly static int julianCenturyDays = 36525; // In Julian days
    readonly static double julianEpoch = 2451545.0;  // In Julian days

    // Gravity is used when simulating rockets
    public readonly static float gravitationalConstant = 0.0001f;
    public readonly static float physicsTimeStep = 0.01f;

    void Awake() {
        georgianDate = DateTime.Now;
        var dateString = "5/1/2008 8:30:52 AM";
        DateTime date1 = DateTime.Parse(dateString,
                                  System.Globalization.CultureInfo.InvariantCulture);
        print(ToJulianDate(date1));
    }

    void FixedUpdate() {
        georgianDate = georgianDate.AddDays(dayTimeStep * Time.fixedDeltaTime);
        julianDate = ToJulianDate(georgianDate);
        julianCenturiesSinceEpoch = ToJulianCenturiesSinceEpoch(julianDate);
        //print("Georgian date: " + georgianDate);
    }

    /*
     * ToOADate is similar to Julian Dates except it uses a different starting point (December 30, 1899)
     * The Julian Date to December 30th 1899 midnight is 2415018.5
     */
    public static double ToJulianDate(DateTime georgianDate) {
        return georgianDate.ToOADate() + 2415018.5;
    }

    public static double ToJulianCenturiesSinceEpoch(double julianDate) {
        return (julianDate - julianEpoch) / julianCenturyDays;
    }

    


}
