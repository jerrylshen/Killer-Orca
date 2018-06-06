using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class flock2: MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    NavMeshPath path;
    public float timeForNewPath;
    bool inCoRoutine;
    Vector3 target;
    bool validPath;
    public float x, z;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
		x = 0;
		z = 0;
		timeForNewPath = 0;

    }

    void Update()
    {
		if (!inCoRoutine) {
			StartCoroutine(DoSomething());
		}
    }

    Vector3 getNewRandomPosition()
    {
        float x2 = Random.Range(40, 150);
        float z2 = Random.Range(40, 150);

        Vector3 pos;
        int num = Random.Range(0, 8);
        if (num == 1)
        {
            pos = new Vector3(x+x2, 0, z+z2);
        }
        else if(num == 2)
        {
            pos = new Vector3(x + x2, 0, z - z2);
        }
        else if (num == 3)
        {
            pos = new Vector3(x - x2, 0, z + z2);
        }
        else if (num == 4)
        {
            pos = new Vector3(x-x2, 0, z-z2);
        }
        else if (num == 5)
        {
            pos = new Vector3(x - x2, 0, z);
        }
        else if (num == 6)
        {
            pos = new Vector3(x, 0, z - z2);
        }
        else if (num == 7)
        {
            pos = new Vector3(x + x2, 0, z);
        }
        else
        {
            pos = new Vector3(x, 0, z + z2);
        }

        return pos;
    }

    IEnumerator DoSomething()
    {
        inCoRoutine = true;
		yield return new WaitForSeconds(timeForNewPath);
        GetNewPath();
        validPath = navMeshAgent.CalculatePath(target, path);

        /*while (!validPath)
        {
            GetNewPath();
            validPath = navMeshAgent.CalculatePath(target, path);
        }*/
		yield return new WaitForSeconds (12);

        inCoRoutine = false;
    }

    void GetNewPath()
    {
        target = getNewRandomPosition();
        navMeshAgent.SetDestination(target);
    }

}