using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapExporter : MonoBehaviour
{
    public int sizeX = 100;
    public int sizeZ = 100;
    public float spacing = 1.0f;
    public float rayHeight = 100f;

    public LayerMask groundMask;
    public LayerMask obstacleMask;

    public string outputFileName = "MashMap.txt";

    [ContextMenu("Export Walkable Map")]
    public void ExportMap()
    {
        string path = Path.Combine(Application.dataPath, outputFileName);
        using (StreamWriter writer = new StreamWriter(path))
        {
            for (int z = sizeZ - 1; z >= 0; z--)
            {
                for (int x = 0; x < sizeX; x++)
                {
                    // 중심을 기준으로 좌/우 균형 있게 퍼지게 계산
                    float worldX = -sizeX * spacing / 2f + x * spacing;
                    float worldZ = -sizeZ * spacing / 2f + z * spacing;

                    Vector3 origin = new Vector3(worldX, rayHeight, worldZ);
                    Ray ray = new Ray(origin, Vector3.down);
                    bool isWalkable = false;

                    // 1. 지면과 충돌하는가?
                    if (Physics.Raycast(ray, out RaycastHit groundHit, rayHeight * 2f, groundMask))
                    {
                        // 2. 충돌 지점 위에 장애물이 있는가?
                        Vector3 obstacleCheckPos = groundHit.point + Vector3.up * 0.5f;

                        bool isBlocked = Physics.CheckBox(obstacleCheckPos, new Vector3(0.3f, 0.5f, 0.3f), Quaternion.identity, obstacleMask);
                        isWalkable = !isBlocked;
                    }

                    writer.Write(isWalkable ? "1" : "0");

                    if (x < sizeX - 1)
                        writer.Write(",");
                }
                writer.WriteLine();
            }
        }
    }
}