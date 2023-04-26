using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToCharacterScene()
    {
        SceneManager.LoadScene("CharacterScene");
    }
}
