using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    public virtual void ResetObstacle() { }

    public virtual ObstaclesTypes obstacleType {get;}
}