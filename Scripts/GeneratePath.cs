using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathCreation;

public class GeneratePath : MonoBehaviour
{
    GameObject generatedPath;
    int addedCount;
    void Start()
    {
        addedCount = 0;
        generatedPath = GameObject.Find("GeneratedPath");
        generatedPath.AddComponent<PathCreator>();
        for (int i = 0; i < 3; i++)
        {
            GameObject created = new GameObject($"point{i}");
            created.transform.position = randomVector3();
            created.transform.SetParent(generatedPath.transform);
        }
        Transform[] vectors = generatedPath.transform.GetComponentsInChildren<Transform>();
        BezierPath bezierPath = new BezierPath(vectors, false, PathSpace.xyz);
        generatedPath.GetComponent<PathCreator>().bezierPath = bezierPath;
        activateFollower();

    }
    void activateFollower()
    {
        GameObject follower = GameObject.Find("Follower");
        follower.GetComponent<Follower>().PathCreator = generatedPath.GetComponent<PathCreator>();
    }
    Vector3 randomVector3()
    {
        int x = Random.Range(0, 7);
        int y = Random.Range(0, 3);
        int z = Random.Range(0, 10);
        return new Vector3(x, y, z);
    }
    public void AddPoint()
    {
        GameObject added = new GameObject($"AddedPoint{addedCount++}");
        added.transform.position = randomVector3();
        added.transform.SetParent(generatedPath.transform);
        generatedPath.GetComponent<PathCreator>().bezierPath.AddSegmentToEnd(added.transform.position);
    }
    public void Randomize()
    {
        BezierPath path = generatedPath.GetComponent<PathCreator>().bezierPath;
        for (int i = 0; i < path.NumPoints; i++)
        {
            path.SetPoint(i, randomVector3(), false);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
