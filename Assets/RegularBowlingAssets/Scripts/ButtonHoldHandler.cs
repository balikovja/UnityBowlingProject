using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoldHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public enum ButtonType { MoveLeft, MoveRight, RotateLeft, RotateRight }
    public ButtonType buttonType;
    private bool isHolding = false;
    private GameManager gameManager;

    void Start()
    {
        // Get the GameManager component
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
    }

    void Update()
    {
        if (isHolding)
        {
            switch (buttonType)
            {
                case ButtonType.MoveLeft:
                    gameManager.MoveLeft();
                    break;
                case ButtonType.MoveRight:
                    gameManager.MoveRight();
                    break;
                case ButtonType.RotateLeft:
                    gameManager.RotateLeft();
                    break;
                case ButtonType.RotateRight:
                    gameManager.RotateRight();
                    break;
            }
        }
    }
}
