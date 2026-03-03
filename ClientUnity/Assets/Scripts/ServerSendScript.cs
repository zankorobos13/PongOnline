using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class ServerSendScript : MonoBehaviour
{
    public string PlayerID;
    public string GameID;
    public string POST_URL;

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
        StartCoroutine(PostMovement(Movement));
    }

    [Serializable]
    struct MoveStruct
    {
        public string player_id;
        public string game_id;
        public float move_x;
        public float move_y;
    }

    private IEnumerator PostMovement(Vector2 Move)
    {
        WWWForm form = new WWWForm();
        MoveStruct data = new MoveStruct
        {
            player_id = PlayerID,
            game_id = GameID,
            move_x = Move.x,
            move_y = Move.y
        };
        string json_data = JsonUtility.ToJson(data);
        UnityWebRequest request = UnityWebRequest.Post(POST_URL, form);
        byte[] post_bytes = Encoding.UTF8.GetBytes(json_data);
        UploadHandler upload_handler = new UploadHandlerRaw(post_bytes);
        
        request.uploadHandler = upload_handler; 
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        Debug.Log(json_data);
        yield return request.SendWebRequest();

    }
}
