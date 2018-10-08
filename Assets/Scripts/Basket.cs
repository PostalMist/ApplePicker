using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour {
    [Header("Set In Inspector")]
    public float slowPeriod = 3.5f;
    public float enlargedPeriod = 3.5f;
    [Header("Set Dynamically")]
    public Text scoreGT; // a
    //contains apple Picker script
    public ApplePicker apScript;
    //the maximum length that the basket can grow
    private float MaxScaleX = 12.0f;
    //used for intialization
    void Start()
    {
        // Find a reference to the ScoreCounter GameObject
        GameObject scoreGO = GameObject.Find("ScoreCounter"); // b
                                                              // Get the Text Component of that GameObject
        scoreGT = scoreGO.GetComponent<Text>(); // c
                                                // Set the starting number of points to 0
        scoreGT.text = "0";
        //get applepicker script
        apScript = Camera.main.GetComponent<ApplePicker>();
    }


    // Update is called once per frame
    void Update () {
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition; // a
                                                  // The Camera's z position sets how far to push the mouse into 3D
        mousePos2D.z = -Camera.main.transform.position.z; // b
                                                          // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D); // c
                                                                         // Move the x position of this Basket to the x position of the Mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter(Collision coll)
    { // a
      // Find out what hit this basket
        GameObject collidedWith = coll.gameObject; // b
        if (collidedWith.tag == "Apple")
        { // c
            Destroy(collidedWith);
            // Parse the text of the scoreGT into an int
            int score = int.Parse(scoreGT.text); // d
                                                 // Add points for catching the apple
            score += 100;
            // Convert the score back to a string and display it
            scoreGT.text = score.ToString();
            if (score > HighScore.score)
            {
                HighScore.score = score;
            }

        } else if(collidedWith.tag == "GreenApple") {

            Destroy(collidedWith);
            foreach (GameObject basketPart in apScript.basketList)
            {
                Vector3 scale = basketPart.transform.localScale;

                if (scale.x < MaxScaleX)
                {
                    scale.x *= 2;
                }

                basketPart.transform.localScale = scale;


            }

            Invoke("ResetBasketSize", enlargedPeriod);

        } else if(collidedWith.tag == "YellowApple") {
            Destroy(collidedWith);
            //slow down time
            Time.timeScale = 0.4f;

            //reset it back to real time after a while
            Invoke("ResetTime", slowPeriod);

        }
    }

    void ResetTime() {

        Time.timeScale = 1.0f;
    }

    void ResetBasketSize() {
        foreach (GameObject basketPart in apScript.basketList)
        {
            Vector3 scale = basketPart.transform.localScale;

            scale.x = 4;

            basketPart.transform.localScale = scale;


        }
    }
}
