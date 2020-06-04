using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject ObjectToSpawn = null;
    
    public void SpawnObject()
    {
        Instantiate(ObjectToSpawn, transform.position, transform.rotation);
    }
}
