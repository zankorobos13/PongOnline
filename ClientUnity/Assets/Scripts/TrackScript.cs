using System;
using UnityEngine;

public class TrackScript : MonoBehaviour
{
    public GameObject TrackSpawner;
    public GameObject TrackPrefab;
    private DateTime new_time;
    private DateTime recent_time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        new_time = DateTime.Now;
        recent_time = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    { 
        if ((new_time - recent_time).TotalSeconds > 0.1)
        {
            Instantiate(TrackPrefab, TrackSpawner.transform.position, TrackSpawner.transform.rotation);
            recent_time = new_time;
            //Debug.LogWarning("spawn");
        }
        new_time = DateTime.Now;
    }
}
