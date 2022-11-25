using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using TMPro;
using System.Linq;
using System.Data;
using System;
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
                //Rule1gene(iteration,randomAngle,randomLength,randomRule);
                LsystemGenerationRandom(iteration,randomLength,randomAngle);
                isGenerated = true;

            }

       
    }

    public void LsystemGenerationRandom(int iteration,int randomLength, float randomAngle)
    {
        Tuple<char, string,string,string,char,string> randomRules = Tuple.Create('K',"F[+K][-K]+FP-K", "F[-K]FOK","F[+K]FPK" ,'F',"FF");
        Axiom = "K";
        StringBuilder sb = new StringBuilder();
        string CurrentString = Axiom;
        for (int i = 0; i <iteration; i++)
        {

            foreach (var ch in CurrentString)
            {
                int randomNo = UnityEngine.Random.Range(2, 5);

                Debug.Log(randomNo);
                if (ch == randomRules.Item1)
                {
                    if(randomNo == 2)
                    {
                        sb.Append(randomRules.Item2);

                    }
                    else if(randomNo == 3)
                    {
                        sb.Append(randomRules.Item3);

                    }
                    else if(randomNo == 4)
                    {
                        sb.Append(randomRules.Item4);

                    }
                }
                else if (ch == randomRules.Item5)
                {
                    sb.Append(randomRules.Item6);
                }
                else
                {
                    sb.Append(ch);
                }
               
               
            }
            CurrentString = sb.ToString();
            sb = new StringBuilder();
            print(CurrentString);

        }

        foreach (char ch in CurrentString)
        {


            if (ch == 'F')
            {
                Vector3 initialPos = transform.position;
                transform.Translate(Vector3.up * randomLength);
           
                 LineRenderer branchGO = Instantiate(branch);
                  branch.SetPosition(0, initialPos);
                branch.SetPosition(1, transform.position);

            }

            else if (ch == '+')
            {
                transform.Rotate(Vector3.right * randomAngle);
            }

            else if (ch == '-')
            {
                transform.Rotate(Vector3.left * randomAngle);
            }

            else if (ch == '[')
            {
                transformStack.Push(transform.position);
                transformStackRot.Push(transform.rotation);
            }

            else if (ch == ']')
            {
                if (transformStack.Count != 0)
                {
                    transform.position = transformStack.Pop();
                    transform.rotation = transformStackRot.Pop();
                }

            }
            else if (ch == 'K')
            {
                transform.Translate(Vector3.up * randomLength);

            }
            else if (ch == 'P')
            {

                GameObject pickup = Instantiate(pickUp,transform.position,transform.rotation);

            }
            else if(ch =='O')
            {
                GameObject pickupgo = Instantiate(pickUp1, transform.position, transform.rotation);
            }


        }


    }


}
