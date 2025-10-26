using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class LogicScript : MonoBehaviour
{
    public GameObject StartUI;
    public GameObject GameOverUI;
    public Text HpText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    

    public void UpdatePlayerHP(float hp)
    {
        HpText.text = "" + math.round(hp).ToString();
    }

    public void ShowGameOverUI()
    {
        GameOverUI.SetActive(true);
    }



    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator LoadSceneAfterFrame(string sceneName)
    {
        yield return null; // รอให้ GUI loop จบก่อน
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            // ถ้าอยู่ใน Unity Editor จะหยุด Play Mode
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // ถ้าเป็น Build จริง จะปิดแอพ
            Application.Quit();
        #endif
        
        Debug.Log("Game Quit!");
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
