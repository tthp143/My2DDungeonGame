using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LogicScript : MonoBehaviour
{
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
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
