using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class run
{
    public GameObject runner;
    public run (GameObject obj)
    {
        runner = obj;
    }
}
[System.Serializable]
public class RunnerStore
{
    public List<run> run = new List<run>();
    public RunnerStore()
    {
    }
}

public class PlayerEndSceneMovementScript : MonoBehaviour
{
    public static PlayerEndSceneMovementScript Instance;
    [SerializeField] PlayerControllerScript playercontroller;
    [SerializeField] PlayerAnimatorScript playeranimation;
    [SerializeField] PlayerDetectionScript playerdetect;
    [SerializeField] PlayerInputScript playerinput;
    [SerializeField] Transform runnersParent;
    [SerializeField] PlayerFormationScript playerformation;
    List<GameObject> runner = new List<GameObject>();

    List<RunnerStore> Runner = new List<RunnerStore>();
    public List<RunnerStore> MainRunner = new List<RunnerStore>();

    public float MoveSpeed;
    float tempmoveup = 0;
    float templeftright = 0;
    bool leftrightcheck = true;
    bool nextlinecheck = true;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        playercontroller.enabled = false;
        playeranimation.enabled = false;
        playerdetect.enabled = false;
        playerinput.enabled = false;
        playerformation.enabled = false;
        int count = 1;
        int tempcount = 0;
        Runner.Add(new RunnerStore());
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            if (tempcount < count)
            {
                GameObject temprunner = runnersParent.GetChild(i).gameObject;
                runner.Add(temprunner);
                //Debug.Log("count ::  " + (count - 1));
                Runner[count - 1].run.Add(new run(temprunner));
                tempcount++;
            }
            else
            {
                tempcount = 0;
                count++;
                Runner.Add(new RunnerStore());
                GameObject temprunner = runnersParent.GetChild(i).gameObject;
                Runner[count - 1].run.Add(new run(temprunner));
                runner.Add(temprunner);
                tempcount++;
            }
        }

        int maincount = -1;
        for(int i = 0; i < runner.Count; i++)
        {
            for(int j = Runner.Count - 1; j >= 0 && (i < runner.Count); j--)
            {
                maincount++;
                MainRunner.Add(new RunnerStore());
                for (int k = 0; k < (j + 1) && i < runner.Count; k++)
                {
                    MainRunner[maincount].run.Add(new run(runner[i]));
                    i++;
                }
            }
        }

         
    }

    public void FormationChange()
    {
        for (int i = 0; i < MainRunner.Count; i++)
        {
            templeftright = 0;
            if (nextlinecheck)
            {
                MainRunner[i].run[0].runner.transform.localPosition = new Vector3(templeftright, tempmoveup, 0);
                templeftright = templeftright + 1;
            }
            else
            {
                templeftright = templeftright + 0.5f;
                MainRunner[i].run[0].runner.transform.localPosition = new Vector3(templeftright, tempmoveup, 0);
                templeftright = templeftright + 1;
            }

            for (int j = 1; j < MainRunner[i].run.Count; j++)
            {
                if (nextlinecheck)
                {
                    if (leftrightcheck)
                    {
                        leftrightcheck = !leftrightcheck;
                        MainRunner[i].run[j].runner.transform.localPosition = new Vector3(templeftright, tempmoveup, 0);
                    }
                    else
                    {
                        leftrightcheck = !leftrightcheck;
                        MainRunner[i].run[j].runner.transform.localPosition = new Vector3(-templeftright, tempmoveup, 0);
                        templeftright = templeftright + 1;
                    }
                }
                else
                {
                    if (leftrightcheck)
                    {
                        leftrightcheck = !leftrightcheck;
                        MainRunner[i].run[j].runner.transform.localPosition = new Vector3(-templeftright, tempmoveup, 0);

                    }
                    else
                    {
                        leftrightcheck = !leftrightcheck;
                        MainRunner[i].run[j].runner.transform.localPosition = new Vector3(templeftright, tempmoveup, 0);
                        templeftright = templeftright + 1;
                    }
                }

            }
            tempmoveup = tempmoveup + 1.4f;
            nextlinecheck = !nextlinecheck;
        }
        GameControllerScript.onLevelCompleteSet();
    }

    // Update is called once per frame
    void Update()
    {
        if(MainRunner.Count > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, LevelDataScript.Instance.StairPoint.position, MoveSpeed * Time.deltaTime);
        }
    }

    public void RemoveRunner(GameObject Obj)
    {
        int RemoveValue = 0;
        for(int i = 0; i < MainRunner.Count; i++)
        {
            for(int j = 0; j < MainRunner[i].run.Count; j++)
            {
                if(MainRunner[i].run[j].runner == Obj)
                {
                    MainRunner[i].run[j].runner.transform.parent = null;
                    MainRunner[i].run.RemoveAt(j);

                    RemoveValue = i;
                }
            }
        }

        if(MainRunner[RemoveValue].run.Count <= 0)
        {
            MainRunner.RemoveAt(RemoveValue);
        }

        if(MainRunner.Count <= 0)
        {
            UIManagerScript.Instance.LevelCompletePanelShow();
        }
    }
}
