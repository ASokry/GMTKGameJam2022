using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// from here
// http://answers.unity.com/answers/1676740/view.html 

public class KeepInsideCamera : MonoBehaviour
{
    public Camera MainCamera;
    private Rect screenBounds;
    private float objectWidth = 1f;
    private float objectHeight = 1f;
    [SerializeField, Range(0f, 1f)] float bounciness = .5f;

    Rigidbody2D rb;

    void Start()
    {
        MainCamera = Camera.main;
        float cameraHeight = MainCamera.orthographicSize * 2;
        float cameraWidth = cameraHeight * MainCamera.aspect;
        Vector2 cameraSize = new Vector2(cameraWidth, cameraHeight);
        Vector2 cameraCenterPosition = MainCamera.transform.position;
        Vector2 cameraBottomLeftPosition = cameraCenterPosition - (cameraSize / 2);
        screenBounds = new Rect(cameraBottomLeftPosition, cameraSize);
        //objectWidth = transform.GetComponentInChildren<SpriteRenderer>().bounds.extents.x;
        //objectHeight = transform.GetComponentInChildren<SpriteRenderer>().bounds.extents.y;
        rb = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {
        screenBounds.position = (Vector2)MainCamera.transform.position - (screenBounds.size / 2);
    }
    void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        var minX = screenBounds.x + objectWidth;
        var maxX = screenBounds.x + screenBounds.width - objectWidth;
        var minY = screenBounds.y + objectHeight;
        var maxY = screenBounds.y + screenBounds.height - objectHeight;
        //viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x + objectWidth, screenBounds.x + screenBounds.width - objectWidth);
        //viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y + objectHeight, screenBounds.y + screenBounds.height - objectHeight);

        if (newPosition.x < minX)
        {
            newPosition.x = minX;
            rb.velocity = new Vector2(-rb.velocity.x * bounciness, rb.velocity.y);
        }
        else if (newPosition.x > maxX)
        {
            newPosition.x = maxX;
            rb.velocity = new Vector2(-rb.velocity.x * bounciness, rb.velocity.y);
        }
        if (newPosition.y < minY)
        {
            newPosition.y = minY;
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y * bounciness);
        }
        else if (newPosition.y > maxY)
        {
            newPosition.y = maxY;
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y * bounciness);
        }


        transform.position = newPosition;
    }
}
