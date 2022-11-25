using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using TMPro;
using System.Linq;
using System.Data;
using System;

/*This Script is used to generate Stochastic L-systems. It very similar to LsystemGene Script*/
public class LsystemGeneNew : MonoBehaviour
{
    [SerializeField]
    GameObject CUBE;
    [SerializeField]
    float angle;
    [SerializeField]
    int iteration;
    [SerializeField]
    LineRenderer branch;
    [Range(0, 100)]
    [SerializeField]
    float length;
    string axiom;
    string Axiom;
    Dictionary<char, string> Rule2;
    Stack<Vector3> transformStack;
    Stack<Quaternion> transformStackRot;
    [SerializeField]
    GameObject pickUp, pickUp1;
    [SerializeField]
    bool isGenerated = false;

 
    [SerializeField]
    char charKey;
    [SerializeField]
    string rule;
    [SerializeField]
    int randomLength, randomRule;
    [SerializeField]
    float randomAngle;
  
    // Start is called before the first frame update
    void Start()
    {

        transformStack = new Stack<Vector3>();
        transformStackRot = new Stack<Quaternion>();
        randomLength = UnityEngine.Random.Range(1, 4);
        randomAngle = UnityEngine.Random.Range(20f, 28f);
    }

    // Update is called once per frame
    void Update()
    {
   
      
            if (isGenerated == false)
        {
            //Calls the function LsystemGenerationRandom() Which takes 3 different parameters, iteration,Length and Angle
            LsystemGenerationRandom(iteration, randomLength, randomAngle);
            //Sets isGenerated Boolean value to true so that the generation happens only 
                isGenerated = true;

            }

       
    }

    /*This function is respoinsible for generating the L-system grammar and drawing the tree*/
    public void LsystemGenerationRandom(int iteration,int randomLength, float randomAngle)
    {
        //Tuple to store map 1 key to multiple values. This tuple is in the following format<Char, String,String,String,String,Char,String>
        //For the first char in the tuple ther are 4 strings that are production rules that the system could randomly choose.
        Tuple<char, string,string,string,char,string> randomRules = Tuple.Create('K',"F[+K][-K]+FP-K", "F[-K]FOK","F[+K]FPK" ,'F',"FF");
        //Initial Axiom
        Axiom = "K";

        //String builder to be more memory efficient.
        StringBuilder sb = new StringBuilder();


        //Reassigns the axiom to CurrentString
        string CurrentString = Axiom;

        //Iterates over the process for the number of iterations I provided by default
        for (int i = 0; i <iteration; i++)
        {
            //iterates over every character in the string.
            foreach (var ch in CurrentString)
            {
                //generates a random number using Unitys built in random number generator. (inclusive, exclusive) so (2,5) generates random numbers between 2 and 4.
                int randomNo = UnityEngine.Random.Range(2, 5);

                //debug statement to check if the random number being generated is working every iteration
                //Debug.Log(randomNo);
                

                //Checks if the character is equal to the first character element in the Tuple
                if (ch == randomRules.Item1)
                {
                    //checks if the random number generated is 2
                    if(randomNo == 2)
                    {
                        //appends the second value which is the rule-set to the string builder
                        sb.Append(randomRules.Item2);

                    }
                    //if the random number generated is 3
                    else if(randomNo == 3)
                    {
                        //append the third value in the tuple which next rule-set
                        sb.Append(randomRules.Item3);

                    }
                    //if the random number generated is 4
                    else if (randomNo == 4)
                    {
                        //append the fourth value in the tuple which next rule-set
                        sb.Append(randomRules.Item4);

                    }
                }

                //if the character is equal to the fifth iterm in the tuple.
                else if (ch == randomRules.Item5)
                {
                    //append the sixth value in the tuple which next rule-set
                    sb.Append(randomRules.Item6);
                }
                else
                {
                    //append the character itself if it doesnt exist.
                    sb.Append(ch);
                }
               
               
            }
            //assigns the string builder to the current string
            CurrentString = sb.ToString();
            //creates a new string builder
            sb = new StringBuilder();
            //prints the current string.
            //print(CurrentString);

        }

        /*This for loop applies the rules for drawing depending on what character is currently accessed in the string.*/
        foreach (char ch in CurrentString)
        {

            //If the character ch  is F then apply the following rules 
            
            if (ch == 'F')
            {
                //local variable initialPos and assign the current transform position.
                Vector3 initialPos = transform.position;

                //move the transform of the current object in the vertical Y axis by a ranomly generated amount of length.
                transform.Translate(Vector3.up * randomLength);
           
                //create a local variable of type LinRenderer and Instantiate the branch lineRenrer component.
                 LineRenderer branchGO = Instantiate(branch);

                //set the first position of the lineRenderer to the initial pos variable.
                  branch.SetPosition(0, initialPos);

                //set the final position of the linerRenderer to the current position of the lineRenderer to the current transform posiiton
                branch.SetPosition(1, transform.position);

            }

            else if (ch == '+')
            {
                //rotate the current game object by a random angle on the right(X axis)
                transform.Rotate(Vector3.right * randomAngle);
            }

            else if (ch == '-')
            {   //rotate the current game object by a random angle on the left(X axis)
                transform.Rotate(Vector3.left * randomAngle);
            }

            else if (ch == '[')
            {
                //Pushes the current transforms position and rotation into each staack Respectivley
                transformStack.Push(transform.position);
                transformStackRot.Push(transform.rotation);
            }

            else if (ch == ']')
            {  
                //If the stack is not empty Pop the most recent value from the stacks for position and rotation Respectivley
                if (transformStack.Count != 0)
                {
                    transform.position = transformStack.Pop();
                    transform.rotation = transformStackRot.Pop();
                }

            }
            else if (ch == 'K')
            {
                //Moves the gameobejct up but does not draw anything
                transform.Translate(Vector3.up * randomLength);

            }
            else if (ch == 'P')
            {
                //Instantiates a fruit of a certain color
                GameObject pickup = Instantiate(pickUp,transform.position,transform.rotation);

            }
            else if(ch =='O')
            {
                //Instantiates a fruit of a certain color
                GameObject pickupgo = Instantiate(pickUp1, transform.position, transform.rotation);
            }


        }


    }


}
