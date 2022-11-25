using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using TMPro;
using Unity.VisualScripting;

using System.Linq;

public class LssytemGene : MonoBehaviour
{
    [SerializeField]
    float angle;
    [SerializeField]
    int iteration;
    [SerializeField]
    int pattern;
    [SerializeField]
    LineRenderer branch;
    [Range(0, 100)]
    [SerializeField]
    float length;
    string axiom;

    //Dictionary<char, string> Rules,Rules2,Rules3,Rules4,Rules5,Rules6;
    Dictionary<char, string> RuleMain = new Dictionary<char, string>();

    Stack<Vector3> transformStack;
    Stack<Quaternion> transformStackRot;
    [SerializeField]
    TMPro.TMP_InputField angleText,iterationText,axiomText,lhsruleText,rhsText, lhs2ruleText, rhs2Text;

    [SerializeField]
    GameObject branchToDelete;

    
    public bool isGenerated =false;

    Dictionary<char, string> KochRule,QKochRule;
    [SerializeField]
    char charKey;
    [SerializeField]
    string rule;

    // Start is called before the first frame update
    void Start()
    {

        //Rules = new Dictionary<char, string>() { { 'F', "F[+F]F[-F]F" } };
        //Rules2 = new Dictionary<char, string>() { { 'F', "F[+F]F[-F][F]" } };
        //  Rules3 = new Dictionary<char, string>() { { 'F', "FF-[-F+F+F]+[+F-F-F]" } };
        //   Rules4 = new Dictionary<char, string>() { { 'K', "F[+K]F[-K]FK" }, { 'F', "FF" } };
        //  Rules5 = new Dictionary<char, string>() { { 'K', "F[+K][-K]FK" }, { 'F', "FF" } };
        // Rules6 = new Dictionary<char, string>() { { 'K', "F-[[K]+K]+F[+FK]-K" }, { 'F', "FF" } };

        //Instantiates the 2 stacks to store position and rotation of the gameobject

        transformStack = new Stack<Vector3>();
        transformStackRot = new Stack<Quaternion>();

    }

    // Update is called once per frame
    void Update()
    {
       


        //If the G button is pressed on the keyboard
        if (Input.GetKeyDown(KeyCode.G))
        {
            //if angleText is not empty
            if (angleText.text != "")
            {
                //convert the input provided from the textfield from text to a float
                angle = float.Parse((angleText.text));

                // Debug.Log(angle);

            }

            if (iterationText.text != "")
            {   
                //convert the input provided from the textfield from text to an int
                iteration = int.Parse((iterationText.text));

            }
            if (axiomText.text != "")
            {
                //assign axiom to the text input given by the user
                axiom = axiomText.text;

            }

            if (lhsruleText.text != "" && rhsText.text != "")
            {
                //convert the text into Char type and assign it to charKey variable.
                charKey = char.Parse(lhsruleText.text);

                //assign the text to rule variable.
                rule = rhsText.text;
                //add the rules to the dictionary.
                RuleMain.Add(charKey,rule);

            }
            //same as the above if statement.
            if (lhs2ruleText.text != "" && rhs2Text.text != "")
            {
               
                charKey = char.Parse(lhs2ruleText.text);
                rule = rhs2Text.text;
                //RuleMain = new Dictionary<char, string> { { charKey, rule } };
                RuleMain.Add(charKey, rule);

            }
            //checks if isGenerated is true
            if (isGenerated == false)
            {
                //if isGenerated is false then call the function to generate the L-system
                LsystemGeneration(RuleMain, axiom, iteration, angle);
                
            }
            isGenerated = true;

            //The commented code below is the first basic implementation without user inputs everything is hardcoded. I have left it here as a reminder of my previous implementations.
            /*  else if (pattern == 2 && isGenerated == false)
              {
                  Rule2gene(iteration, angle, axiom);
              }
              else if (pattern == 3 && isGenerated == false)
              {
                  Rule3gene(iteration, angle, axiom);
              }
              else if (pattern == 4 && isGenerated == false)
              {
                  Rule4gene(iteration, angle, axiom);
              }
              else if (pattern == 5 && isGenerated == false)
              {
                  Rule5gene(iteration, angle, axiom);
              }
              else if (pattern == 6 && isGenerated == false)
              {
                  Rule6gene(iteration, angle, axiom);
              }
              else if (pattern == 7 && isGenerated == false)
              {
                  KochRulegene(iteration, angle, axiom);
              }
              else if (pattern == 8 && isGenerated == false)
              {
                  QKochRulegene(iteration, angle, axiom);
              }
              isGenerated = true;
          }
            */
        }
        //If C button is pressed on the keyboard Call the clear function
        if (Input.GetKeyDown(KeyCode.C))
        {
                Clear(iteration, pattern, angle,RuleMain);
        }
    }

 
    /*This function is renponsible for generating the L-system grammar and drawing the Lsyste.
     * It takes 4 parameters which the user provides. Rules, Axiom, iterations and angle*/

  public void LsystemGeneration(Dictionary<char, string> Rules, string Axiom, int interation, float angle)
    {
        //Before implementing my own fix i was using string builder.
       // StringBuilder sb = new StringBuilder();
        string CurrentString = Axiom;
        string temp ="";
        //Iterates over the process for the number of iterations provided by the user
        for (int i = 0; i < iteration; i++)
        {
            //iterates over every character in the string.
            foreach (var ch in CurrentString)
            {
                //if the dictonary contains the key then append the value of that key to the temp string variable.
                if (Rules.ContainsKey(ch))
                {
                    //sb.Append(Rules[ch]);
                    // CurrentString+=(Rules[ch]);
                    temp +=(Rules[ch]);

                }
                else
                {//if no then append the default character itself
                    // sb.Append(ch);
                    // CurrentString.Concat(ch);
                    temp += ch;

                }
            }
            //reassign temp to current string
            CurrentString = temp; //sb.ToString();
                                  //sb = new StringBuilder();
            // Set Temp to empty again.
            temp = "";
           //print(CurrentString);
        }

        /*This for loop applies the rules for drawing depending on what character is currently accessed in the string.*/
        foreach (char ch in CurrentString)
        {  
            //If the character ch  is F then apply the following rules 

            if (ch == 'F')
            {//local variable initialPos and assign the current transform position.
                Vector3 initialPos = transform.position;

                //move the transform of the current object in the vertical Y axis by a ranomly generated amount of length.
                transform.Translate(Vector3.up * length);

                //create a local variable of type LinRenderer and Instantiate the branch lineRenrer component.
                LineRenderer branchGO = Instantiate(branch);

                //set the first position of the lineRenderer to the initial pos variable.
                branch.SetPosition(0, initialPos);

                //set the final position of the linerRenderer to the current position of the lineRenderer to the current transform posiiton
                branch.SetPosition(1, transform.position);


                ;
            }
            else if (ch == '+')
            {
                //rotate the current game object by the angle proivded by the user 
                transform.Rotate(Vector3.right * angle);
            }

            else if (ch == '-')
            {
                 //rotate the current game object by the angle provided by the user on the left(X axis)
                    transform.Rotate(Vector3.left * angle);
            }

            else if (ch == '[')
            {
                //Pushes the current transforms position and rotation into each staack Respectivley
                transformStack.Push(transform.position);
                transformStackRot.Push(transform.rotation);
            }

            else if (ch == ']')
            {
                //Pop the most recent value from the stacks for position and rotation Respectivley
                transform.position = transformStack.Pop();
                transform.rotation = transformStackRot.Pop();
            }
            else if (ch == 'K')
            {
                //Moves the gameobejct up but does not draw anything

                transform.Translate(Vector3.up * length);

            }

        }


    }

    
   /* public void KochRulegene(int iteration, float angle,string axiom)
    {
        KochRule = new Dictionary<char, string>() { { 'F', "F - F++F - F" } };
        LsystemGeneration(KochRule, axiom, iteration, angle);
    }
    public void QKochRulegene(int iteration, float angle, string axiom)
    {
       
        QKochRule = new Dictionary<char, string>() { { 'F', "F+F-F-FF+F+F-F" } };
        LsystemGeneration(QKochRule, axiom, iteration, angle);
    }
    public void Rule2gene(int iteration, float angle, string axiom)
    {
        Rules2 = new Dictionary<char, string>() { { 'F', "F[+F]F[-F][F]" } };
        LsystemGeneration(Rules2, axiom, iteration,angle);
    }

    public void Rule3gene(int iteration, float angle, string axiom)
    {
        Rules3 = new Dictionary<char, string>() { { 'F', "FF-[-F+F+F]+[+F-F-F]" } };
        LsystemGeneration(Rules3, axiom, iteration, angle);
    }
    public void Rule4gene(int iteration, float angle, string axiom)
    {
        Rules4 = new Dictionary<char, string>() { { 'K', "F[+K]F[-K]FK" }, { 'F', "FF" } };
        LsystemGeneration(Rules4, axiom, iteration, angle);
    }
    public void Rule5gene(int iteration, float angle, string axiom)
    {
        Rules5 = new Dictionary<char, string>() { { 'K', "F[+K][-K]FK" }, { 'F', "FF" } };
        LsystemGeneration(Rules5, axiom, iteration, angle);
    }

    public void Rule6gene(int iteration, float angle, string axiom)
    {
        Rules6 = new Dictionary<char, string>() { { 'K', "F-[[K]+K]+F[+FK]-K" }, { 'F', "FF" } };
        LsystemGeneration(Rules6, axiom, iteration, angle);
    }*/

    /*Responsible for clearing all values on screen so that the user can input new ones*/
    public void Clear(int interation,int pattern,float angle,Dictionary<char,string> Rules)
    {
        //Searches for game objects in the scene with tag"Branch" adds them to the array which stores all these variables to destroy them when the function is called
        GameObject[] destroyBranch =GameObject.FindGameObjectsWithTag("Branch");
        transform.position = new Vector3(0, 0, 0);
        if(iterationText.text != "" && angleText.text != "")
        {
            //Resets everything to empty or null or 0 
            iterationText.text = "";
   
            angleText.text = "";
            axiomText.text = "";
            lhsruleText.text = "";
            rhs2Text.text = "";
            rhsText.text = "";
            lhs2ruleText.text = "";
            angle = 0;
            iteration = 0;
            pattern = 0;
            axiom = null;
            rule = null;
            charKey = '\0';
            //rule2 = null;
           // charKey2 = '\0';
            //Empties the dictionary
            Rules.Clear();
            
        }

        isGenerated = false;
        for(int i=0;i<destroyBranch.Length;i++)
        {
            Destroy(destroyBranch[i]);
        }
    }
}
