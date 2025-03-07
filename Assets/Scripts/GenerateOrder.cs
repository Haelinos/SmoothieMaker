using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GenerateOrder : MonoBehaviour
{
    public GameObject banana;
    public GameObject strawberries;
    public GameObject blueberries;
    public GameObject mango;
    public GameObject spinach;

    public bool smoothie1;
    public bool smoothie2;
    public bool smoothie3;
    public bool smoothie4;

    // Update is called once per frame
    void Update()
    {
        ShowOrder();
    }


    // Shows order on the screen
    void ShowOrder() 
    {
        if (smoothie1) 
        {
            Instantiate(banana, transform.position, transform.rotation);
            Debug.Log("banana is shown on screen");
        }
        // if boolean turns true --> instantiate the three ingredients on the order.
    }

    // TO DO: Create IEnumerator for adding orders after some time.
}
