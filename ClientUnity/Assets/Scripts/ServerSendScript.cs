using UnityEngine;
using UnityEngine.InputSystem;

public class ServerSendScript : MonoBehaviour
{

    private Vector2 Movement;
    private Keyboard Keyboard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Keyboard = Keyboard.current;
    }

    // Update is called once per frame
    void Update()
    {
        float y = 0;
        if (Keyboard.wKey.isPressed || Keyboard.upArrowKey.isPressed)
            y = 1;
        else if (Keyboard.sKey.isPressed || Keyboard.downArrowKey.isPressed)
            y = -1;
        Movement = new Vector2(0, y);
        Debug.Log(Movement.ToString());
    }
}
