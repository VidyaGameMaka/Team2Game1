using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public Zombie NextZombie => line.LastOrDefault(x => x.zombie != null)?.zombie;

    public Transform lineStart;
    public Transform lineEnd;
    public Zombie prefabZombie;
    public int maxZombiesInLine;
    public int minZombieSpawn;
    public int maxZombieSpawn;
    public int zombieSpawnDelay;

    private bool restaurantOpen = true;
    private List<LinePosition> line = new List<LinePosition>();

    private class LinePosition
    {
        public Vector3 position;
        public Zombie zombie;
    }
    
    private void Start()
    {
        Instance = this;
        InitializeLine();
        StartCoroutine(SpawnZombies());
    }

    private void InitializeLine()
    {
        for (int i = 0; i < maxZombiesInLine; i++)
        {
            var position = Vector3.Lerp(lineStart.position, lineEnd.position, i / (float)maxZombiesInLine);
            line.Add(new LinePosition()
            {
                position = position
            });
        }
    }

    public void RemoveFromLine(Zombie zombie)
    {
        line.First(x => x.zombie == zombie).zombie = null;
    }

    private IEnumerator SpawnZombies()
    {
        while (restaurantOpen)
        {
            int num = Random.Range(minZombieSpawn, maxZombieSpawn);
            for (int i = 0; i < num; i++)
            {
                var freeSpot = line.FirstOrDefault(x => x.zombie == null);
                if (freeSpot != null)
                {
                    freeSpot.zombie = Instantiate(prefabZombie, freeSpot.position, Quaternion.identity);
                }
            }
            yield return new WaitForSeconds(zombieSpawnDelay);
        }
    }
}
