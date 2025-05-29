using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float moveSpeed = 7f;
    private bool isMoving = false;
    private Vector3 initialPosition;

    private SpriteRenderer spriteRenderer;
    private Color[] colors = {
        new Color(0.6f, 0.7f, 0.9f),
        new Color(0.8f, 0.7f, 0.9f)
    };
    private int colorIndex = 0;

    private int frameCounter = 0;
    private float frameChangeInterval = 0.0001f; 

    void Start()
    {
        initialPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer 추가 필요");
        }
        else
        {
            spriteRenderer.color = colors[colorIndex];
        }
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            frameCounter++;
            if (frameCounter >= frameChangeInterval)
            {
                frameCounter = 0;
                colorIndex = (colorIndex + 1) % colors.Length;
                spriteRenderer.color = colors[colorIndex];
            }
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        frameCounter = 0;
    }

    public void StopMoving() => isMoving = false;

    public void ResetPosition()
    {
        transform.position = initialPosition;
        colorIndex = 0;
        frameCounter = 0;
        if (spriteRenderer != null)
            spriteRenderer.color = colors[colorIndex];
    }
}
