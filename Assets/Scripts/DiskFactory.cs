using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class DiskFactory : MonoBehaviour{
    public GameObject diskPrefab;  //生产飞碟的模板，保存着一个飞碟的实例
    private Dictionary<int, DiskData> used = new Dictionary<int, DiskData>();
    private List<DiskData> free = new List<DiskData>();
    private List<int> wait = new List<int>();
 
    private void Awake()
    {
        diskPrefab = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/disk"), Vector3.zero, Quaternion.identity);
        diskPrefab.SetActive(false);
    }
 
    private void Update()
    {
 
        foreach (var tmp in used.Values)
        {
 
            if (!tmp.gameObject.activeSelf)
            {
                wait.Add(tmp.GetInstanceID());
            }
        }
 
        foreach (int tmp in wait)
        {
            FreeDisk(used[tmp].gameObject);
        }
        wait.Clear();
    }

    public GameObject GetDisk(int round, ActionMode mode)
    {
        GameObject newDisk = null;
        if (free.Count > 0)
        {
            newDisk = free[0].gameObject;
            free.Remove(free[0]);
        }
        else
        {
            newDisk = GameObject.Instantiate<GameObject>(diskPrefab, Vector3.zero, Quaternion.identity);
            newDisk.AddComponent<DiskData>();
        }
         
        int start = 0;
        if (round == 1) start = 100;
        if (round == 2) start = 250;
        int selectedColor = Random.Range(start, round * 499);
 
        if (selectedColor > 500)
        {
            round = 2;
        }
        else if (selectedColor > 300)
        {
            round = 1;
        }
        else
        {
            round = 0;
        }
 
        DiskData diskdata = newDisk.GetComponent<DiskData>();
        switch (round)
        {
           
            case 0:
                {
                    diskdata.color = Color.yellow;
                    diskdata.speed = 4.0f;
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
                    diskdata.direction = new Vector3(RanX, 1, 0);
                    newDisk.GetComponent<Renderer>().material.color = Color.yellow;
                    break;
                }
            case 1:
                {
                    diskdata.color = Color.red;
                    diskdata.speed = 6.0f;
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
                    diskdata.direction = new Vector3(RanX, 1, 0);
                    newDisk.GetComponent<Renderer>().material.color = Color.red;
                    break;
                }
            case 2:
                {
                    diskdata.color = Color.black;
                    diskdata.speed = 8.0f;
                    float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
                    diskdata.direction = new Vector3(RanX, 1, 0);
                    newDisk.GetComponent<Renderer>().material.color = Color.black;
                    break;
                }
        }
 
        if (mode == ActionMode.PHYSIC)
        {
            newDisk.AddComponent<Rigidbody>();
        }
 
        used.Add(diskdata.GetInstanceID(), diskdata);
        newDisk.name = newDisk.GetInstanceID().ToString();
        return newDisk;
    }
 
    public void FreeDisk(GameObject disk)
    {
        DiskData tmp = null;
        foreach (DiskData i in used.Values)
        {
            if (disk.GetInstanceID() == i.gameObject.GetInstanceID())
            {
                tmp = i;
            }
        }
        if (tmp != null) {
            tmp.gameObject.SetActive(false);
            free.Add(tmp);
            used.Remove(tmp.GetInstanceID());
        }
    }
}
