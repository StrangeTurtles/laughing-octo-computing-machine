using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barbarians : MonoBehaviour
{
    public IDecision BarbarianRoot;
    public int health = 10;
    private int attackSpeed = 10;
    private int time = 0;
    public List<GameObject> villagers = null;
    public int curTarget = 0;
    // Start is called before the first frame update
    void Start()
    {
        BarbarianRoot = new NearVillager(this,
            new AttackVillage(this),
            new MoveVillage(this));
    }

    // Update is called once per frame
    void Update()
    {
        IDecision curDecision = BarbarianRoot.MakeDecision();

        while (curDecision.MakeDecision() != null)
        {
            //BarbarianRoot.MakeDecision();
            curDecision = curDecision.MakeDecision();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

public class NearVillager : IDecision
{
    Barbarians barbarians;
    IDecision left;
    IDecision right;

    public NearVillager(Barbarians barbarians, IDecision left, IDecision right)
    {
        this.barbarians = barbarians;
        this.left = left;
        this.right = right;
    }

    public IDecision MakeDecision()
    {
        if(barbarians.villagers.Count >= 1)
        {
            int num = -1;
            float distance = float.MaxValue;
            foreach (GameObject i in barbarians.villagers)
            {
                num++;
                float tempDistance = Mathf.Sqrt(Mathf.Pow(barbarians.transform.position.x - barbarians.villagers[num].transform.position.x, 2) + Mathf.Pow(barbarians.transform.position.z - barbarians.villagers[num].transform.position.z, 2));
                if(distance > tempDistance)
                {
                    distance = tempDistance;
                    barbarians.curTarget = num;
                }
            }
            return (distance >= -.5 && distance <= .5) ? left : right;
        }
        else
        {
            return null;
        }
    }
}

public class AttackVillage : IDecision
{
    Barbarians barbarians;
    
    public AttackVillage(Barbarians barbarians)
    {
        this.barbarians = barbarians;
    }

    public IDecision MakeDecision()
    {
        barbarians.villagers[barbarians.curTarget].GetComponent<Villager>().health -= 1;
        if(barbarians.villagers[barbarians.curTarget].GetComponent<Villager>().health <= 0)
        {
            barbarians.villagers.Remove(barbarians.villagers[barbarians.curTarget]);
        }
        return null;
    }
}

public class MoveVillage : IDecision
{
    Barbarians barbarians;

    public MoveVillage(Barbarians barbarians)
    {
        this.barbarians = barbarians;
    }

    public IDecision MakeDecision()
    {
        barbarians.transform.position += ((barbarians.villagers[barbarians.curTarget].transform.position - barbarians.transform.position).normalized * .04f);
        return null;
    }
}