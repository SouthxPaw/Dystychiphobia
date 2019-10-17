using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    //variable to hold the index for the random level
    private int randomLevel;
    //array to hold the index of the level used
    private ArrayList levelUsed = new ArrayList();
    //variable to iterate through array
    private int i = 0;
    //variable to hold the max number of levels
    private int maxLevel = 1;
    //triggers event of player being in door collider box
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //nothing needs to happen whenever player collides with it
        if (collision.CompareTag("Player"))
        {
           //nothing needs to happen whenever player collides with it
        }
    }

    //triggers event to load next level whenever player presses the interact key
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetButtonDown("Interact"))
            {
                //generate a random level
                randomLevel = Random.Range(2, maxLevel + 1);
                //check to see if the level has been used and if it has keep randomizing the level until you find an unused level
                while (levelUsed.Contains(randomLevel))
                {
                    if (levelUsed.Count == maxLevel)
                    {
                        break;
                    }
                    randomLevel = Random.Range(2, maxLevel + 1);
                }
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                SceneManager.LoadScene(randomLevel);
                //store the random level into an array so that we can keep track of the level played
                levelUsed.Add(randomLevel);
            }
        }
    }


    //nothing needs to happen whenever player collides with it
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //nothing needs to happen whenever player collides with it
        }
    }

}
