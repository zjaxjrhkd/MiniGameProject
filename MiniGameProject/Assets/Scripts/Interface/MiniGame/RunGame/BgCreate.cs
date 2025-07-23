using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgCreate : MonoBehaviour
{
    public List<GameObject> backgroundLayer1Prefab;
    public List<GameObject> backgroundLayer2Prefab;
    public List<GameObject> backgroundLayer3Prefab;
    public List<GameObject> backgroundLayer4Prefab;
    public List<GameObject> backgroundLayer5Prefab;
    public List<List<GameObject>> layers;
    public List<float> layerSpeeds = new List<float>();

    private List<Vector3> layer2InitPositions = new List<Vector3>();

    void Start()
    {
        layers = new List<List<GameObject>>
        {
            backgroundLayer2Prefab,
            backgroundLayer3Prefab,
            backgroundLayer4Prefab,
            backgroundLayer5Prefab
        };

    }

    void Update()
    {
        BGMove();
        ChangeLayerPrefabPositionAll();
    }

    public void BGMove()
    {
        for (int layerIdx = 0; layerIdx < layers.Count; layerIdx++)
        {
            float speed = layerSpeeds[layerIdx];//0은 안움직이는 배경
            var layer = layers[layerIdx];
            for (int i = 0; i < layer.Count; i++)
            {
                layer[i].transform.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
    }

    public void ChangeLayerPrefabPositionAll()
    {
        foreach (var layer in layers)
        {
            if (layer[2].transform.position.x <= -39f)
            {
                GameObject obj0 = layer[0];
                GameObject obj1 = layer[1];
                GameObject obj2 = layer[2];

                obj2.transform.position = new Vector3(79f, obj0.transform.position.y, obj0.transform.position.z);

                layer[0] = obj2;
                layer[1] = obj0;
                layer[2] = obj1;
            }
        }
    }
}