using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonClick : MonoBehaviour
{
    
    public void OnButtonClick()
    {
        Debug.Log("Button Clicked.");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("mainGameScene"); // 使用场景名称
        // 或者 SceneManager.LoadScene(1); // 使用场景索引
    }
}
