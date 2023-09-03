using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPS : MonoBehaviour {
    public static float longitude=3f;
    public static float latitude=5f;
    public static GPS instance;

    private void Awake() //before start
    {
        if (instance == null)
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    void FixedUpdate()
    {
        if (Input.location.isEnabledByUser)
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }    
    }
    IEnumerator Start()
    {
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        Debug.Log("Outa start");
        // Starts the location service.
        Input.location.Start(5.0f,10.0f); //the accuracy is 5meters;update once I move out of 10meters

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        // Stops the location service if there is no need to query location updates continuously.
        //Input.location.Stop(); ////Comment it out so it will continuously check the location
    }
}
