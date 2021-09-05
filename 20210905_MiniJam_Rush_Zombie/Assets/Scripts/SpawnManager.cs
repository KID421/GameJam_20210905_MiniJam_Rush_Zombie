using UnityEngine;

/// <summary>
/// 生成管理器
/// </summary>
public class SpawnManager : MonoBehaviour
{
    [Header("生成物件")]
    public GameObject goSpawnObject;
    [Header("生成位置")]
    public Transform[] spawnPoints;
    [Header("生成起始時間"), Range(0, 10)]
    public float timeStart = 0;
    [Header("生成間隔時間"), Range(0, 10)]
    public float timeInterval = 3;

    private void Start()
    {
        Invoke("Spawn", timeStart);
    }

    /// <summary>
    /// 生成物件
    /// </summary>
    private void Spawn()
    {
        int pointRandom = Random.Range(0, spawnPoints.Length);
        Vector3 pos = spawnPoints[pointRandom].position;
        Instantiate(goSpawnObject, pos, Quaternion.identity);
        Invoke("Spawn", timeInterval -= 0.01f);
        print(timeInterval);
    }

    /// <summary>
    /// 停止生成
    /// </summary>
    public void StopSpwan()
    {
        CancelInvoke("Spawn");
    }
}
