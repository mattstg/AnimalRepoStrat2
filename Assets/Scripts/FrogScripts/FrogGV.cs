using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogGV  {
    public static FrogWS frogWS;
    public static readonly float RibbitMaxSize = .8f;
    public static readonly float frogMatingDistance = .25f;
    public static readonly int pond_layer_mask = LayerMask.GetMask("Pond");
    public static readonly Vector2 mapBounds = new Vector2(3.9f, 3.2f);
    public static readonly int frogsPerSnake = 9; //how many frogs it takes to summon a snake
    public static readonly int minSnakes = 3;
    public static readonly float snakeEatRange = .2f;
    //public static readonly bool playerFrogIsActive = false;

    static List<Frog> femaleFrogs = new List<Frog>();
    static List<Frog> maleFrogs = new List<Frog>();
    

    public static List<Frog> masterList = new List<Frog>(); //Instead of merging each list each turn for snake check, just have two
    public static List<Tadpole> masterTadpoleList = new List<Tadpole>();


    public static void ModTadpole(Tadpole t, bool toAdd)
    {
        if(toAdd)
        {
            masterTadpoleList.Add(t);
        }
        else
        {
            masterTadpoleList.Remove(t);
        }
        FrogGV.frogWS.puddle.SetLimits();
    }

    public static void ClearTadpoleList()
    {
        masterTadpoleList = new List<Tadpole>();
        FrogGV.frogWS.puddle.SetLimits();
    }


    public static void AddFrogToMasterList(Frog newFrog, bool isMale)
    {
        if (isMale)
        {
            if (!maleFrogs.Contains(newFrog))
                maleFrogs.Add(newFrog);
        }
        else
        {
            if (!femaleFrogs.Contains(newFrog))
                femaleFrogs.Add(newFrog);
        }
        masterList.Add(newFrog);
    }
    

    public static void RemoveFrogFromMasterList(Frog toRemove, bool isMale)
    {
        if (isMale)
            maleFrogs.Remove(toRemove);
        else
            femaleFrogs.Remove(toRemove);
        masterList.Remove(toRemove);
    }

    public static Frog FemaleCanMate(Vector2 loc) //she should only call this if in water and cooldown is fine
    {
        float closestDist = 999f;
        float tdist;
        Frog closestFrog = null;
        for (int i = 0; i < maleFrogs.Count; i++)
        {
            Frog male = maleFrogs[i];
            if (male == null)
            {
                maleFrogs.RemoveAt(i);
                continue;
            }
            else
            {
                tdist = MathHelper.ApproxDist(male.transform.position, loc);
                if (tdist < frogMatingDistance && tdist < closestDist)
                    closestFrog = male;
            }
        }
        return closestFrog;
    }

    public static void RibbitAtLoc(Vector2 loc)
    {
        for(int i = 0; i < femaleFrogs.Count; i++)
        {
            Frog female = femaleFrogs[i];
            if(female == null)
            {
                femaleFrogs.RemoveAt(i);
                continue;
            }
            else
            {
                float approxDist = MathHelper.ApproxDist(loc, female.transform.position);
                if(approxDist <= RibbitMaxSize)
                    female.HeardARibbit(loc);
            }
        }
    }
}
