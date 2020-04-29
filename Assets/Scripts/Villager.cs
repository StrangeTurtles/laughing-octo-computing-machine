using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    public IDecision VillagerRoot;
    public bool attacked = false;
    public float foodVal = 10;
    public int health = 20;
    private int attackSpeed = 10;
    private int time = 0;
    public List<GameObject> barbs = null;
    public List<Transform> wood = null;
    public GameObject fishingHole = null;
    public int curTarget = 0;
    // Start is called before the first frame update
    void Start()
    {
        VillagerRoot = new UnderAttack(this,
            new EnemyNear(this,
                new Attack(this),
                new MoveToEnemy(this)),
            new Hungry(this,
                new AtFishingHole(this,
                    new Fishing(this),
                    new GoFishingHole(this)),
                new AtResources(this,
                    new Resources(this),
                    new GoResources(this))));
    }

    // Update is called once per frame
    void Update()
    {
        IDecision curDecision = VillagerRoot.MakeDecision();

        while (curDecision.MakeDecision() != null)
        {
            //VillagerRoot.MakeDecision();
            //curDecision.MakeDecision();
            curDecision = curDecision.MakeDecision();
        }
        if(health <= 0)
        {
            Destroy(gameObject);
        }
        foodVal -= Time.deltaTime;
    }
}

public class UnderAttack : IDecision
{
    Villager villager;
    IDecision left;
    IDecision right;

    public UnderAttack() { }

    public UnderAttack(Villager villager, IDecision left, IDecision right)
    {
        this.villager = villager;
        this.left = left;
        this.right = right;
    }

    public IDecision MakeDecision()
    {
        if(villager.barbs.Count >= 1)
        {
            villager.attacked = true;
        }
        else
        {
            villager.attacked = false;
        }
        return villager.attacked ? left : right;
    }
}

public class EnemyNear : IDecision
{
    Villager villager;
    IDecision left;
    IDecision right;

    public EnemyNear() { }

    public EnemyNear(Villager villager, IDecision left, IDecision right)
    {
        this.villager = villager;
        this.left = left;
        this.right = right;
    }

    public IDecision MakeDecision()
    {
        if (villager.barbs[villager.curTarget] == null)
        {
            villager.barbs.Remove(villager.barbs[villager.curTarget]);
            return null;
        }
        float distance = Mathf.Sqrt(Mathf.Pow(villager.transform.position.x - villager.barbs[villager.curTarget].transform.position.x, 2) + Mathf.Pow(villager.transform.position.z - villager.barbs[villager.curTarget].transform.position.z, 2));
        return (distance >= -.5 && distance <= .5) ? left : right;
    }
}

public class Attack : IDecision
{
    Villager villager;

    public Attack() { }

    public Attack(Villager villager)
    {
        this.villager = villager;
    }

    public IDecision MakeDecision()
    {
        if(villager.barbs[villager.curTarget].GetComponent<Barbarians>() != null)
        villager.barbs[villager.curTarget].GetComponent<Barbarians>().health -= 1;
        return null;
    }
}

public class MoveToEnemy : IDecision
{
    Villager villager;

    public MoveToEnemy() { }

    public MoveToEnemy(Villager villager)
    {
        this.villager = villager;
    }

    public IDecision MakeDecision()
    {
        villager.transform.position += ((villager.barbs[villager.curTarget].transform.position - villager.transform.position).normalized * .04f);
        return null;
    }
}

public class Hungry : IDecision
{
    Villager villager;
    IDecision left;
    IDecision right;

    public Hungry() { }

    public Hungry(Villager villager, IDecision left, IDecision right)
    {
        this.villager = villager;
        this.left = left;
        this.right = right;
    }

    public IDecision MakeDecision()
    {
        //villager.foodVal--;
        return (villager.foodVal <= 0) ? left: right;
    }
}

public class AtFishingHole : IDecision
{
    Villager villager;
    IDecision left;
    IDecision right;

    public AtFishingHole() { }

    public AtFishingHole(Villager villager, IDecision left, IDecision right)
    {
        this.villager = villager;
        this.left = left;
        this.right = right;
    }

    public IDecision MakeDecision()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(villager.transform.position.x - villager.fishingHole.transform.position.x, 2) + Mathf.Pow(villager.transform.position.z - villager.fishingHole.transform.position.z, 2));
        return (distance >= -.5 && distance <= .5) ? left : right;
    }
}

public class GoFishingHole : IDecision
{
    Villager villager;
    
    public GoFishingHole() { }

    public GoFishingHole(Villager villager)
    {
        this.villager = villager;
    }

    public IDecision MakeDecision()
    {
        villager.transform.position += ((villager.fishingHole.transform.position - villager.transform.position).normalized * .04f);
        return null;
    }
}

public class Fishing : IDecision
{
    Villager villager;

    public Fishing() { }

    public Fishing(Villager villager)
    {
        this.villager = villager;
    }

    public IDecision MakeDecision()
    {
        villager.foodVal = 20;
        return null;
    }
}

public class AtResources : IDecision
{
    Villager villager;
    IDecision left;
    IDecision right;

    public AtResources() { }

    public AtResources(Villager villager, IDecision left, IDecision right)
    {
        this.villager = villager;
        this.left = left;
        this.right = right;
    }

    public IDecision MakeDecision()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(villager.transform.position.x - villager.wood[villager.curTarget].transform.position.x, 2) + Mathf.Pow(villager.transform.position.z - villager.wood[villager.curTarget].transform.position.z, 2));
        return (distance >= -.5 && distance <= .5) ? left : right;
    }
}

public class GoResources : IDecision
{ 
    Villager villager;

    public GoResources() { }

    public GoResources(Villager villager)
    {
        this.villager = villager;
    }

    public IDecision MakeDecision()
    {
        villager.transform.position += ((villager.wood[villager.curTarget].transform.position - villager.transform.position).normalized * .04f);
        return null;
    }
}

public class Resources : IDecision
{
    Villager villager;

    public Resources() { }

    public Resources(Villager villager)
    {
        this.villager = villager;
    }

    public IDecision MakeDecision()
    {
        return null;
    }
}