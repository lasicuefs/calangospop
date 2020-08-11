using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitorBehaviour : omnivorousBehaviour
{

	void Start () {
        base.Start();

        registry.registerCompetitor(gameObject);
    }

    protected override void die()
    {

        registry.unregisterCompetitor(gameObject);
        Destroy(gameObject);
    }

    public override void get_eaten()
    {
        registry.unregisterCompetitor(gameObject);
        base.get_eaten();
    }
}
