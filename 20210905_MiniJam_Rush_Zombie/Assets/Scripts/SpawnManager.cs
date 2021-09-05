using UnityEngine;

/// <summary>
/// �ͦ��޲z��
/// </summary>
public class SpawnManager : MonoBehaviour
{
    [Header("�ͦ�����")]
    public GameObject goSpawnObject;
    [Header("�ͦ���m")]
    public Transform[] spawnPoints;
    [Header("�ͦ��_�l�ɶ�"), Range(0, 10)]
    public float timeStart = 0;
    [Header("�ͦ����j�ɶ�"), Range(0, 10)]
    public float timeInterval = 3;

    private void Start()
    {
        Invoke("Spawn", timeStart);
    }

    /// <summary>
    /// �ͦ�����
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
    /// ����ͦ�
    /// </summary>
    public void StopSpwan()
    {
        CancelInvoke("Spawn");
    }
}
