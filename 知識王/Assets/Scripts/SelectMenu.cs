using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectMenu : MonoBehaviour
{
    public void HomeButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ReviewButton()
    {
        SceneManager.LoadScene("ReviewScene");
    }
    public void TestButton()
    {
        SceneManager.LoadScene("TestScene");
    }
}
