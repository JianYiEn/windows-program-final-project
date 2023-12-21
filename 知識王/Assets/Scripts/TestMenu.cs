using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestMenu : MonoBehaviour
{
    public void Goback()
    {
        SceneManager.LoadScene("SelectMenu");
    }
}
