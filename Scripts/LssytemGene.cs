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

            if (iterationText.text != "")
            {
                iteration = int.Parse((iterationText.text));

            }
            if (axiomText.text != "")
            {
                axiom = axiomText.text;

            }

            if (lhsruleText.text != "" && rhsText.text != "")
            {
                charKey = char.Parse(lhsruleText.text);
                rule = rhsText.text;
                //RuleMain = new Dictionary<char, string> { { charKey, rule } };
                RuleMain.Add(charKey,rule);

            }
            if (lhs2ruleText.text != "" && rhs2Text.text != "")
            {
               
                charKey = char.Parse(lhs2ruleText.text);
                rule = rhs2Text.text;
                //RuleMain = new Dictionary<char, string> { { charKey, rule } };
                RuleMain.Add(charKey, rule);

            }
            if (isGenerated == false)
            {
                LsystemGeneration(RuleMain, axiom, iteration, angle);
                
            }
            isGenerated = true;
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
        if (Input.GetKeyDown(KeyCode.C))
        {
                Clear(iteration, pattern, angle,RuleMain);
        }
    }

    /*  public void LsystemGeneration(Dictionary<char,string> Rules,string Axiom,int interation,float angle)
      {
          StringBuilder sb = new StringBuilder();
          string CurrentString = Axiom;
          for(int i= 0; i < iteration; i++)
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

          }

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
                 transform.position =transformStack.Pop();
                  transform.rotation = transformStackRot.Pop();
              }
              else if(ch == 'K')
              {
                  transform.Translate(Vector3.up * length);

              }

          }


      }
    */
  public void LsystemGeneration(Dictionary<char, string> Rules, string Axiom, int interation, float angle)
    {
        StringBuilder sb = new StringBuilder();
        string CurrentString = Axiom;
        string temp ="";
        for (int i = 0; i < iteration; i++)
        {
            foreach (var ch in CurrentString)
            {
                if (Rules.ContainsKey(ch))
                {
                    //sb.Append(Rules[ch]);
                    // CurrentString+=(Rules[ch]);
                    temp +=(Rules[ch]);

                }
                else
                {
                     sb.Append(ch);
                    // CurrentString.Concat(ch);
                    temp += ch;

                }
            }

            CurrentString = temp; //sb.ToString();
                                  //sb = new StringBuilder();
            temp = "";
           print(CurrentString);
        }

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

        }


    }

    public void Rule1gene(int iteration,float angle,string axiom,Dictionary<char,string>RuleMain)
    {
        //Rules = new Dictionary<char, string>() { { 'F', "F[+F]F[-F]F" } };
      //  LsystemGeneration(RuleMain, axiom, iteration,angle);
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
    public void Clear(int interation,int pattern,float angle,Dictionary<char,string> Rules)
    {
        GameObject[] destroyBranch =GameObject.FindGameObjectsWithTag("Branch");
        transform.position = new Vector3(0, 0, 0);
        if(iterationText.text != "" && angleText.text != "")
        {
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
            
            Rules.Clear();
            
        }

        isGenerated = false;
        for(int i=0;i<destroyBranch.Length;i++)
        {
            Destroy(destroyBranch[i]);
        }
    }
}
