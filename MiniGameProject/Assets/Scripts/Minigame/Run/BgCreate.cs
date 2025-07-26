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
    public void MapLoop()
    {
        BGMove();
        ChangeLayerPrefabPositionAll();
    }
    public void ChangeAllLayerSprites(int stageNum)
    {
        ApplyLayerSprite(backgroundLayer1Prefab, $"Image/Map/Layer_0/Layer_0_{stageNum}");
        ApplyLayerSprite(backgroundLayer2Prefab, $"Image/Map/Layer_1/Layer_1_{stageNum}");
        ApplyLayerSprite(backgroundLayer3Prefab, $"Image/Map/Layer_2/Layer_2_{stageNum}");
        ApplyLayerSprite(backgroundLayer4Prefab, $"Image/Map/Layer_3/Layer_3_{stageNum}");
        ApplyLayerSprite(backgroundLayer5Prefab, $"Image/Map/Layer_4/Layer_4_{stageNum}");
    }

    private void ApplyLayerSprite(List<GameObject> layerPrefabs, string spritePath)
    {
        Sprite newSprite = Resources.Load<Sprite>(spritePath);
        foreach (var obj in layerPrefabs)
        {
            var sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.sprite = newSprite;
            }
        }
    }
}