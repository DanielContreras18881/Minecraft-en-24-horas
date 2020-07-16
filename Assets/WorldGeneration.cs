using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public List<GameObject> DefaultBlocks;
    public List<GameObject> Trees;
    public bool GenerateTrees;
    public float terrainDet; // Detalle del ruido (Frecuencia)
    public float terrainHeight; // Altura del ruido (Amplitud)
    public int groundHeight; // Cuantos bloques tiene por debajo
    int ChunkSize = 32;
    int Seed;
    void Start()
    {
        Seed = Random.Range(100000, 100000000);
        GenerateChunk();
    }
    void GenerateChunk()
    {
        GameObject chunk = new GameObject();
        chunk.transform.SetParent(transform);
        for (int x = 0; x < ChunkSize; x++) // eje x de bloques
        {
            for (int z = 0; z < ChunkSize; z++) // eje y de bloques
            {
                float xPerlin = (((float)x + (0.001f * Random.Range(-24f, 25f))) / 3 + Seed) / (terrainDet + (0.00001f * Random.Range(-0.005f, 0.005f))); // Valor x que se le introduce al ruido 
                float zPerlin = (((float)z + (0.001f * Random.Range(-24f, 25f))) / 3 + Seed) / (terrainDet + (0.00001f * Random.Range(-0.005f, 0.005f))); // Valor z que se le introduce al ruido 
                float Ymax = (Mathf.PerlinNoise(xPerlin, zPerlin) * terrainHeight);
                Ymax += groundHeight; // Sube el terreno la cantidad de lo que tiene debajo
                int YmaxClamped = (Random.Range(0, 1000) > 1) ? (int)Mathf.Ceil(Ymax) : (int)Mathf.Floor(Ymax); // lo vuelve un entero con un poquito de random :3
                for (int y = 0; y < YmaxClamped; y++) // Generar subterreno
                {
                    int dirts = Random.Range(1, 6); // Cuantos bloques de tierra hay
                    switch (y >= (YmaxClamped - dirts))
                    {
                        case true:
                            GameObject Dirt = SpawnBlock(DefaultBlocks[1], new Vector3(x, y, z), chunk.transform);
                            break;
                        case false:
                            GameObject Stone = SpawnBlock(DefaultBlocks[2], new Vector3(x, y, z), chunk.transform);
                            break;
                    }
                }
                GameObject Grass = SpawnBlock(DefaultBlocks[0], new Vector3(x, YmaxClamped, z), chunk.transform);
                if (GenerateTrees && (Random.Range(0, 200) <= 1)) // Genera un arbol cada 1 sobre 200 bloques
                {
                    GameObject Tree = Instantiate(Trees[Random.Range(0, Trees.Count)], new Vector3(x, YmaxClamped + 1, z), Quaternion.identity, chunk.transform);
                }
            }
        }
    }
    public GameObject SpawnBlock(GameObject blockType, Vector3 pos, Transform parentt)
    {
        GameObject bloc = Instantiate(blockType, pos, Quaternion.identity, parentt);
        bloc.tag = "isGroundable";
        bloc.isStatic = true;
        return bloc;
    }
    public void DestroyBlock(GameObject block)
    {
        Destroy(block);
    }
}
