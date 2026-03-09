using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerScript : MonoBehaviour
{
    public GameObject LoginButtons;
    public GameObject RegisterButtons;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoginButtons.SetActive(false);
        RegisterButtons.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLoginWindow()
    {
        LoginButtons.SetActive(true);
    }
    public void OpenRegisterWindow()
    {
        RegisterButtons.SetActive(true);
    }
    public void CloseLoginWindow()
    {
        LoginButtons.SetActive(false);
    }
    public void CloseRegisterWindow()
    {
        RegisterButtons.SetActive(false);
    }

    public void Login()
    {

    }
    public void Register()
    {

    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
