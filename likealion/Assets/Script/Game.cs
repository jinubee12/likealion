using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
    
public class Game : MonoBehaviour
{
    // 플레이어 점수를 저장하는 배열
    private int[] playerScores = new int[5];

    // 아이템 이름을 저장하는 배열
    private string[] itemNames = { "검", "방패", "포션", "활", "마법서" };

    // 적 프리팹을 저장하는 배열
    public GameObject[] enemyPrefabs;

    // 맵의 타일 타입을 저장하는 2D 배열
    private int[,] mapTiles = new int[10, 10];

    public GameObject Cube;
    public GameObject Sphere;
    public GameObject[,] CubeTiles = new GameObject[10, 10];
    void Start()
    {
        PlayerScoresExample();
        ItemInventoryExample();
        EnemySpawnExample();
        MapGenerationExample();
    }

    void PlayerScoresExample()
    {
        // 플레이어 점수 할당
        for (int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = Random.Range(100, 1000);
        }

        // 최고 점수 찾기
        int highestScore = playerScores.Max();
        Debug.Log($"최고 점수: {highestScore}");

        // 평균 점수 계산
        float averageScore = (float)playerScores.Average();
        Debug.Log($"평균 점수: {averageScore:F2}");
    }
    
    void ItemInventoryExample()
    {
        // 랜덤 아이템 선택
        int randomIndex = Random.Range(0, itemNames.Length);
        string selectedItem = itemNames[randomIndex];
        Debug.Log($"선택된 아이템: {selectedItem}");

        // 특정 아이템 검색
        string searchItem = "포션";
        bool hasPotion = itemNames.Contains(searchItem);
        Debug.Log($"포션 보유 여부: {hasPotion}");
    }

    private bool Contains(string itemName)
    {
        for (var i = 0; i < itemName.Length; i++)
        {
            if (itemNames[i] == itemName)
            {
                return true;
            }
        }
        return false;
    }
    
    void EnemySpawnExample()
    {
        if (enemyPrefabs != null && enemyPrefabs.Length > 0)
        {
            // 랜덤 위치에 랜덤 적 생성
            Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemyIndex], spawnPosition, Quaternion.identity);
            Debug.Log($"적 생성됨: {enemyPrefabs[randomEnemyIndex].name}");
        }
        else
        {
            Debug.LogWarning("적 프리팹이 할당되지 않았습니다.");
        }
    }
    
    void MapGenerationExample()
    {
        // 간단한 맵 생성 (0: 빈 공간, 1: 벽)
        for (int x = 0; x < mapTiles.GetLength(0); x++)
        {
            for (int y = 0; y < mapTiles.GetLength(1); y++)
            {
                mapTiles[x, y] = Random.value > 0.8f ? 1 : 0;
            }
        }

        // 맵 출력
        string mapString = "생성된 맵:\n";
        for (int x = 0; x < mapTiles.GetLength(0); x++)
        {
            for (int y = 0; y < mapTiles.GetLength(1); y++)
            {
                CubeTiles[x, y] = mapTiles[x, y] == 1 ? Instantiate(Cube, new Vector3(x,y,0),Quaternion.identity) : Instantiate(Sphere, new Vector3(x,y,0),Quaternion.identity);
                Debug.Log(CubeTiles[x, y]);
            }
        }
        
    }
}
