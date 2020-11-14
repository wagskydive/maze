using UnityEngine;
using System.Collections;

public enum Direction{
	Start,
	Right,
	Front,
	Left,
	Back,
};
//<summary>
//Class for representing concrete maze cell.
//</summary>
public class MazeCell {
	public bool IsVisited = false;
	public bool WallRight = false;
	public bool WallFront = false;
	public bool WallLeft = false;
	public bool WallBack = false;
	public bool IsGoal = false;

    public void RemoveByChance(float chance)
    {
        if(WallRight== true)  { WallRight=passFail(chance);}
        if(WallFront== true)  {WallFront=passFail(chance);}
        if(WallLeft == true)  {WallLeft =passFail(chance);}
        if (WallBack == true) {WallBack= passFail(chance); }
    }


    public bool passFail(float fChanceOfSuccess)
    {
        float fRand = Random.Range(0.0f, 1.0f);
        if (fRand <= fChanceOfSuccess)
        {
            return true;
        }            
        return false;
    }


}
