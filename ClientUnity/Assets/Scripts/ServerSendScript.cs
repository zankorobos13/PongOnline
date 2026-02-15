using UnityEngine;

public class ServerSendScript : MonoBehaviour
{

    private Vector2 Movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement = new Vector2(0, Input.GetAxis("Vertcal"));
        Debug.Log(Movement.ToString());
    }
}
