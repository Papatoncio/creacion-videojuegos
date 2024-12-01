using UnityEngine;
using UnityEngine.InputSystem;

public class CursorController : MonoBehaviour
{
    public float speed = 1000f;    // Velocidad del cursor
    public RectTransform virtualCursor; // Asocia un objeto UI para que act�e como el cursor
    private Vector2 cursorPosition;
    private MovementEntriies movementEntries;

    private void Awake()
    {
        movementEntries = new MovementEntriies();
    }

    private void OnEnable()
    {
        movementEntries.Enable();
        // Inicializa la posici�n del cursor en el centro de la pantalla
        cursorPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        UpdateCursorVisual();
    }

    private void OnDisable()
    {
        movementEntries.Disable();
    }

    private void Update()
    {
        // Obtener entrada del joystick
        Vector2 input = movementEntries.Cursor.MoveCursor.ReadValue<Vector2>();

        // Calcular nueva posici�n del cursor
        cursorPosition += input * speed * Time.deltaTime;

        // Restringir el cursor dentro de los l�mites de la pantalla
        cursorPosition.x = Mathf.Clamp(cursorPosition.x, 0, Screen.width);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, 0, Screen.height);

        // Actualizar la posici�n del cursor virtual
        UpdateCursorVisual();
    }

    private void UpdateCursorVisual()
    {
        // Asegurarse de que el virtualCursor est� configurado
        if (virtualCursor != null)
        {
            Vector3 newPosition = new Vector3(cursorPosition.x, cursorPosition.y, 0);
            virtualCursor.position = newPosition;
        }
    }
}