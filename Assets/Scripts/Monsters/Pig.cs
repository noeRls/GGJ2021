using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Monster
{
    public float chargeSetupTime = 1;
    private float chargeSetupTimeout = 0;

    public float chargeInterval = 5;
    private float chargeTimeout = 0;

    public float speedCharge = 10;
    private bool isCharging = false;

    override protected void updateTarget()
    {
        if (isCharging)
        {
            return;
        }
        if (!playerPos.HasValue || chargeTimeout > 0)
        {
            base.updateTarget();
            return;
        }
        isCharging = true;
        chargeTimeout = chargeInterval;
        chargeSetupTimeout = chargeSetupTime;
        target = playerPos.Value;
    }

    protected override void onTargetReach()
    {
        if (isCharging)
        {
            isCharging = false;
            updateTarget();
        } else
        {
            base.onTargetReach();
        }
    }

    protected override void setSpeed()
    {
        if (isCharging)
        {
            if (chargeSetupTimeout > 0)
            {
                navMeshAgent.speed = 0;
            } else
            {
                navMeshAgent.speed = speedCharge;
            }
        } else
        {
            base.setSpeed();
        }
    }

    private void Update()
    {
        monsterUpdate();
        chargeSetupTimeout -= Time.deltaTime;
        chargeTimeout -= Time.deltaTime;
    }
}
