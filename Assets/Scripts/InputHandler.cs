using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public GameManager gameManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            gameManager.StopObject();
        }
    }
}