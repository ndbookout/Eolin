using UnityEngine;
using System.Collections;

public class PassByRef : MonoBehaviour
{
    //set this in the inspector
    public int number = 2;

    //this one we'll set up here
    class NumberHolder
    {
        public int number = 2;
    }
    private NumberHolder numberHolder = new NumberHolder(); //contains a number with the value 2

    void Start()
    {
        //first, we'll pass the number by value (since we're using a struct)
        Debug.Log("The value of the number to start is " + number);
        squareValue(number);
        Debug.Log("The value of the number after passing by value is " + number);

        //next, we'll pass the number by reference (since we're using a class)
        Debug.Log("The value of the number to start is " + numberHolder.number);
        squareReference(numberHolder);
        Debug.Log("The value of the number after passing by reference is " + numberHolder.number);
    }

    void squareValue(int valueParameter)
    {
        valueParameter *= valueParameter;
        Debug.Log("The value of the multiplied number in the squareValue method is " + valueParameter);
    }

    void squareReference(NumberHolder valueParameter)
    {
        valueParameter.number *= valueParameter.number;
        Debug.Log("The value of the multiplied number in the squareReference method is " + valueParameter.number);
    }
}
