using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CreditsScroller : MonoBehaviour
{
    [Header("Scroll Settings")]
    [SerializeField] private float scrollSpeed = 50f;
    [SerializeField] private RectTransform creditsText;
    
    [Header("End Position")]
    [SerializeField] private float endPositionY = 2000f; // ระยะที่จะหยุด scroll
    
    [Header("Scene Settings")]
    [SerializeField] private string nextSceneName = "MainMenu";
    
    [Header("Skip Settings")]
    [SerializeField] private bool canSkipDuringScroll = true; // Skip ระหว่างเลื่อน
    
    [Header("UI Elements")]
    [SerializeField] private GameObject pressKeyText; // ข้อความ "กด Enter เพื่อกลับเมนู" (Optional)

    private bool isScrolling = true;
    private bool creditsEnded = false;

    void Start()
    {
        if (creditsText == null)
        {
            Debug.LogError("กรุณาลาก Text GameObject ใส่ใน Credits Text field!");
        }

        // ซ่อนข้อความ Press Key ตอนเริ่มต้น
        if (pressKeyText != null)
        {
            pressKeyText.SetActive(false);
        }
    }

    void Update()
    {
        if (isScrolling)
        {
            // เลื่อนข้อความขึ้น
            creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

            // ตรวจสอบว่าถึงตำแหน่งสุดท้ายหรือยัง
            if (creditsText.anchoredPosition.y >= endPositionY)
            {
                isScrolling = false;
                creditsEnded = true;

                // แสดงข้อความ Press Key
                if (pressKeyText != null)
                {
                    pressKeyText.SetActive(true);
                }

                Debug.Log("Credits จบแล้ว - กดปุ่มเพื่อกลับเมนู");
            }

            // Skip ระหว่างเลื่อน (ถ้าเปิดใช้งาน)
            if (canSkipDuringScroll && CheckSkipInput())
            {
                JumpToEnd();
            }
        }
        else if (creditsEnded)
        {
            // รอกดปุ่มหลังจาก Credits จบ
            if (CheckAnyKeyInput())
            {
                LoadNextScene();
            }
        }
    }

    // ตรวจสอบปุ่ม Skip (Escape หรือ Space)
    bool CheckSkipInput()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame || 
                Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                return true;
            }
        }
        return false;
    }

    // ตรวจสอบการกดปุ่มใดก็ได้ (Enter, Space, Escape, หรือคลิกเมาส์)
    bool CheckAnyKeyInput()
    {
        // ตรวจสอบ Keyboard
        if (Keyboard.current != null)
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame || 
                Keyboard.current.spaceKey.wasPressedThisFrame ||
                Keyboard.current.escapeKey.wasPressedThisFrame ||
                Keyboard.current.anyKey.wasPressedThisFrame)
            {
                return true;
            }
        }

        // ตรวจสอบ Mouse Click
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            return true;
        }

        // ตรวจสอบ Touch (Mobile)
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            return true;
        }

        return false;
    }

    // กระโดดไปจุดสิ้นสุดทันที
    void JumpToEnd()
    {
        creditsText.anchoredPosition = new Vector2(
            creditsText.anchoredPosition.x, 
            endPositionY
        );
        isScrolling = false;
        creditsEnded = true;

        // แสดงข้อความ Press Key
        if (pressKeyText != null)
        {
            pressKeyText.SetActive(true);
        }

        Debug.Log("Skip Credits - กดปุ่มเพื่อกลับเมนู");
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log("Loading Scene: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.Log("Credits Ended - ไม่มี Scene ถัดไป");
        }
    }

    // สำหรับปุ่ม UI (ถ้ามี)
    public void SkipToEnd()
    {
        if (isScrolling)
        {
            JumpToEnd();
        }
    }

    public void GoToMenu()
    {
        LoadNextScene();
    }
}