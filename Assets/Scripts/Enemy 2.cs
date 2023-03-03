using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    // Start is called before the first frame update
    public new enum AIModeNow { FollowPlayer, Punch, Shoot };
    public new AIModeNow aiModeNow;

    public new enum AIModePlan { Punch, Shoot };
    public new AIModePlan aiModePlan;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
