using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    bool _buttonPressed;
    bool _goNext;

    // Update is called once per frame
    void Update()
    {
        if (_goNext)
        {
            _goNext = false;
            SceneManager.LoadScene("GameScene");

        }
    }

    public void OnTitleGo()
    {
        if (_buttonPressed) return;
        _buttonPressed = true;
        _goNext = true;
    }
}
