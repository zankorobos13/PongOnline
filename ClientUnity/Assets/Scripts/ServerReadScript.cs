using System.Collections;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.Networking;

public class ServerReadScript : MonoBehaviour
{
    public GameObject Board1;
    public GameObject Board2;
    public GameObject Ball;
    public string GET_URL;

    [System.Serializable]
    private struct CoordDataStruct
    {
        public float Board1_X;
        public float Board1_Y;
        public float Board2_X;
        public float Board2_Y;
        public float Ball_X;
        public float Ball_Y;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SendGetRequest());   
    }

    private IEnumerator SendGetRequest()
    {
        UnityWebRequest request = UnityWebRequest.Get(GET_URL);
        yield return request.SendWebRequest();
        CoordDataStruct coordData = JsonUtility.FromJson<CoordDataStruct>(request.downloadHandler.text);
        Board1.transform.position = new Vector2(coordData.Board1_X, coordData.Board1_Y);
        Board2.transform.position = new Vector2(coordData.Board2_X, coordData.Board2_Y);
        Ball.transform.position = new Vector2(coordData.Ball_X, coordData.Ball_Y);
        Debug.Log(request.downloadHandler.text);
    }
}
