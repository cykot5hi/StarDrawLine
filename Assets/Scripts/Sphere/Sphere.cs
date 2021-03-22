using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    

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

    void Start()
    {
        sphereTransform = gameObject.GetComponent<Transform>();
        radiusSphere = sphereTransform.localScale.x;
        newPosition = new Vector2(sphereTransform.position.x, sphereTransform.position.y);
        sphereRigidbody = GetComponent<Rigidbody2D>();
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
    }


    void CheckLine() //Проверка на количество collidres near sphere
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, radiusSphere / 2);
        isLine = colliders.Length > 1;
    }

    void accelerationSphere() //Ускорение шара при движениии по линии
    {
        if (oldPosition != newPosition)
        {
            sphereRigidbody.AddForce(new Vector2((newPosition.x - oldPosition.x) * speed, (newPosition.y - oldPosition.y) * speed));
        }
    }
}
