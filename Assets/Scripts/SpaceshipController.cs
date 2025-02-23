using UnityEngine;
using System.Collections.Generic;

public class SpaceshipController : MonoBehaviour
{
    public List<SpaceshipMover> spaceships;
    public GameObject spaceshipsGO;

    public void Start()
    {
        spaceshipsGO.SetActive(false);
    }

    public void StartMoving()
    {
        spaceshipsGO.SetActive(true);
        foreach (var spaceship in spaceships)
        {
            if (spaceship != null)
            {
                spaceship.StartMoving();
            }
        }

        if (spaceships.Count > 0 && spaceships[0] != null)
        {
            spaceships[0].FollowPlayer();
        }
    }
}
