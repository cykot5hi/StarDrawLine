using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    // Временное решение. С какой стороны косается шар линии
    public Transform[] lineCheck;
    Vector2 vectorModifier;

    // Поля для проверки движения по линии
    // - - - - - - - - - - - - -
    Transform sphereTransform;
    Vector2 newPosition;
    float radiusSphere;
    bool isLine = false;
    // - - - - - - - - - - - - - 

    // Поля для ускорения движения по линии
    // - - - - - - - - - - - - -
    Vector2 oldPosition;
    public float speed = 1;
    public float speedMax = 10;
    Rigidbody2D sphereRigidbody;
    // - - - - - - - - - - - - - 
    bool StartTimer;
    float TimeStart;
    float velocityY;

    void Start()
    {
        sphereTransform = gameObject.GetComponent<Transform>();
        radiusSphere = sphereTransform.localScale.x;
        newPosition = new Vector2(sphereTransform.position.x, sphereTransform.position.y);
        sphereRigidbody = GetComponent<Rigidbody2D>();
        vectorModifier = new Vector2(0f, 0f);
    }

    void Update()
    {

        oldPosition = newPosition;
        sphereTransform = gameObject.GetComponent<Transform>();
        newPosition = new Vector2(sphereTransform.position.x, sphereTransform.position.y); 
        CheckLine();
        if (isLine) //Если двигаемся по линии, то шар ускоряется
        {
            accelerationSphere();
        }
        if (!isLine && sphereRigidbody.velocity.y < -5) // Замедление ускорения если velocity.y to much
        {
            // sphereRigidbody.velocity = new Vector2(sphereRigidbody.velocity.x, -5f);
            // StartTimer = true;

            if (StartTimer)
            {
                TimeStart = Time.time;
                StartTimer = false;
                velocityY = sphereRigidbody.velocity.y;
            }
            sphereRigidbody.velocity = new Vector2(sphereRigidbody.velocity.x, velocityY + ((Time.time - TimeStart)/* * (Time.time - TimeStart)*/));
        }
        else
        {
            //sphereRigidbody.velocity = new Vector2(sphereRigidbody.velocity.x, -5f);
            StartTimer = true;
        }
            
        Debug.Log(sphereRigidbody.velocity);
    }


    void CheckLine() //Проверка на количество collidres near sphere
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, radiusSphere / 2);
        isLine = colliders.Length > 1;
    }

    void accelerationSphere() //Ускорение шара при движениии по линии
    {
        touchingSide();
        if (oldPosition != newPosition)
        {
            sphereRigidbody.AddForce(new Vector2((newPosition.x - oldPosition.x + (vectorModifier.x)) * speed, (newPosition.y - oldPosition.y + (vectorModifier.y)) * speed));
        }
    }
    void touchingSide() //Корректировка направления вектора
    {
        int i;
        float a = 0.5f;
        for (i = 7; i >= 0; i--)
        {
            Collider2D[] collidersLine = Physics2D.OverlapCircleAll(lineCheck[i].position, 0.005f);

            if (collidersLine.Length > 1)
            {
                Debug.Log(i);
                break;
            }
        }
        switch (i)
        {
            case 0:
                vectorModifier.x = -a;
                vectorModifier.y = 0f;  
                break;
            case 1:
                vectorModifier.x = -a;
                vectorModifier.y = -a;
                break;
            case 2:
                vectorModifier.x = 0f;
                vectorModifier.y = -a;
                break;
            case 3:
                vectorModifier.x = a;
                vectorModifier.y = -a;
                break;
            case 4:
                vectorModifier.x = a;
                vectorModifier.y = 0f;
                break;
            case 5:
                vectorModifier.x = a;
                vectorModifier.y = a;
                break;
            case 6:
                vectorModifier.x = 0f;
                vectorModifier.y = a;
                break;
            case 7:
                vectorModifier.x = -a;
                vectorModifier.y = a;
                break;
        }
    }
}
