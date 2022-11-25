using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using TMPro;
using Unity.VisualScripting;
using System.Linq;

public class Fractals : MonoBehaviour
{
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

    //Dictionary<char, string> Rules,Rules2,Rules3,Rules4,Rules5,Rules6;
    Dictionary<char, string> RuleMain;

    Stack<Vector3> transformStack;
    Stack<Quaternion> transformStackRot;
    [SerializeField]
    TMPro.TMP_InputField angleText, axiomText, lhsruleText, rhsText, lhs2ruleText, rhs2Text;

    [SerializeField]
    GameObject branchToDelete;


    Dictionary<char, string> KochRule, QKochRule;
    [SerializeField]
    char charKey;
    [SerializeField]
    string rule;


    // Start is called before the first frame update
    void Start()
    {
      


        transformStack = new Stack<Vector3>();
        transformStackRot = new Stack<Quaternion>();

    }

    // Update is called once per frame
    void Update()
    {
       


        if (Input.GetKeyDown(KeyCode.G))
        {
            if (angleText.text != "")
            {
                angle = float.Parse((angleText.text));

                // Debug.Log(angle);

            }

            if (axiomText.text != "")
            {
                axiom = axiomText.text;

            }

            if (lhsruleText.text != "" && rhsText.text != "")
            {
                charKey = char.Parse(lhsruleText.text);
                rule = rhsText.text;
                RuleMain = new Dictionary<char, string> { { charKey, rule } };

            }

            if (lhs2ruleText.text != "" && rhs2Text.text != "")
            {

                charKey = char.Parse(lhs2ruleText.text);
                rule = rhs2Text.text;
                //RuleMain = new Dictionary<char, string> { { charKey, rule } };
                RuleMain.Add(charKey, rule);

            }
           
           
                iteration = 4;
                LsystemGeneration( RuleMain,axiom, iteration, angle);
            
         
    
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Clear(angle);
            
        }
    }

    public void LsystemGeneration(Dictionary<char, string> Rules, string Axiom, int interation, float angle)
    {
        StringBuilder sb = new StringBuilder();
        string CurrentString = Axiom;
      for(int i = 0; i < iteration; i++)
        {
            foreach (var ch in CurrentString)
            {
                if (Rules.ContainsKey(ch))
                {
                    sb.Append(Rules[ch]);

                }
                else
                {
                    sb.Append(ch);
                }
            }
            CurrentString = sb.ToString();
            sb = new StringBuilder();
            print(CurrentString);



            foreach (char ch in CurrentString)
            {
                if (ch == 'F')
                {
                    Vector3 initialPos = transform.position;
                    transform.Translate(Vector3.up * length);

                    LineRenderer branchGO = Instantiate(branch);
                    branch.SetPosition(0, initialPos);
                    branch.SetPosition(1, transform.position);


                    ;
                }
                else if (ch == '+')
                {
                    transform.Rotate(Vector3.right * angle);
                }

                else if (ch == '-')
                {
                    transform.Rotate(Vector3.left * angle);
                }

                else if (ch == '[')
                {
                    transformStack.Push(transform.position);
                    transformStackRot.Push(transform.rotation);
                }

                else if (ch == ']')
                {
                    transform.position = transformStack.Pop();
                    transform.rotation = transformStackRot.Pop();
                }
                else if (ch == 'K')
                {
                    transform.Translate(Vector3.up * length);

                }
                else if (ch == 'G')
                {
                    Vector3 initialPos = transform.position;
                    transform.Translate(Vector3.up * length);

                    LineRenderer branchGO = Instantiate(branch);
                    branch.SetPosition(0, initialPos);
                    branch.SetPosition(1, transform.position);

                }
            }

        }

    }

    public void Rule1gene(int iteration, float angle, string axiom, Dictionary<char, string> RuleMain)
    {
        //Rules = new Dictionary<char, string>() { { 'F', "F[+F]F[-F]F" } };
        LsystemGeneration(RuleMain, axiom, iteration, angle);
    }
 
    public void Clear(float angle)
    {
        GameObject[] destroyBranch = GameObject.FindGameObjectsWithTag("Branch");
        transform.position = new Vector3(0, 0, 0);
        if (angleText.text != "")
        {
            
            angleText.text = "";
            axiomText.text = "";
            lhsruleText.text = "";
            rhs2Text.text = "";
            rhsText.text = "";
            lhs2ruleText.text = "";
            angle = 0;
            axiom = null;
            rule = null;
            charKey = '\0';
        }

        for (int i = 0; i < destroyBranch.Length; i++)
        {
            Destroy(destroyBranch[i]);
        }
    }
}
