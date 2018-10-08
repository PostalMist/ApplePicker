using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour {
    [Header("Set in Inspector")]
    // Prefab for instantiating apples
    public GameObject applePrefab;
    //instantiate green apples that will cause the basket to increase in length
    public GameObject greenApplePrefab;
    //instantiates yellow apples that will cause the game to slow down
    public GameObject yellowApplePrefab;
    // Speed at which the AppleTree moves
    public float speed = 10f;
    // Distance where AppleTree turns around
    public float leftAndRightEdge = 25f;
    // Chance that the AppleTree will change directions
    public float chanceToChangeDirections = 0.02f;
    // Rate at which Apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;

    public float secondsBetweenGreenAppleDrops = 5f;

    public float secondsBetweenYellowAppleDrops = 9f;
    private float startTime;

    // Use this for initialization
    void Start () {
        //dropping apples every second
        startTime = Time.time;
        Invoke("DropApple", 2f);
        
       /* Invoke("DropGreenApple", 7f);
       Invoke("DropYellowApple", 11f);*/
    }

    void DropApple()
    { // b
        int option = Mathf.CeilToInt(Time.time - startTime);

        if (option % 7 == 0) {
            DropGreenApple();
        } else if (option % 11 == 0) {
            DropYellowApple();
        }
        else {
            GameObject apple = Instantiate<GameObject>(applePrefab); // c
            apple.transform.position = transform.position; // d
           // Invoke("DropApple", secondsBetweenAppleDrops); // e
        }
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

    void DropGreenApple()
    {
        GameObject greenApple = Instantiate<GameObject>(greenApplePrefab); // c
        greenApple.transform.position = transform.position; // d
       // Invoke("DropGreenApple", secondsBetweenGreenAppleDrops); // e

    }

    void DropYellowApple()
    {
        GameObject yellowApple = Instantiate<GameObject>(yellowApplePrefab); // c
        yellowApple.transform.position = transform.position; // d
      //  Invoke("DropYellowApple", secondsBetweenYellowAppleDrops); // e


    }

    // Update is called once per frame
    void Update () {
        //Basic Movement
        Vector3 pos = transform.position; // b
        pos.x += speed * Time.deltaTime; // c
        transform.position = pos; // d

        // Changing Direction
        if (pos.x < -leftAndRightEdge)
        {
            speed = Mathf.Abs(speed); // Move right
        }
        else if (pos.x > leftAndRightEdge)
        {
            speed = -Mathf.Abs(speed); // Move left
        }

    }

    void FixedUpdate()
    {
        // Changing Direction Randomly is now time-based because of FixedUpdate()

        if (Random.value < chanceToChangeDirections)
        { // b
            speed *= -1; // Change direction
        }
    }
}
