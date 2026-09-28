using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatCouple
{
    public GameObject platA;
    public GameObject platB;
}

public class PlatformRandomizer : MonoBehaviour
{
    public PlatCouple[] rGroup = new PlatCouple[4];

    private void Start()
    {
        RandomizePlatforms();
    }

    public void RandomizePlatforms()
    {
        foreach (var pair in rGroup)
        {
            if (pair.platA == null || pair.platB == null) continue;

            bool chooseA = Random.Range(0, 2) == 0;

            Collider clA = pair.platA.GetComponent<Collider>();
            Collider clB = pair.platB.GetComponent<Collider>();

            if (clA != null) clA.enabled = chooseA;
            if (clB != null) clB.enabled = !chooseA;
        }
    }
}

