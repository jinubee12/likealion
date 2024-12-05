using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


[Serializable]
public class PlayerData
{
    public string playerName;
    
    [NonSerialized]
    public float Distance;
}

public class GameManager : MonoBehaviour
{
    public float battleTime = 16.0f;
    
    // 경마에 참여할 플레이어 리스트
    public List<PlayerData> Players = new List<PlayerData>();
    
    // ui에 표현 될 버튼 프리팹
    public RaceButton templateButton;
    
    // 버튼들이 붙을 부모오브젝트
    public Transform RaceButtonParent;  
    
    // 생성된 버튼들 관리
    private List<RaceButton> raceButtons = new List<RaceButton>();

    public float trackStartX = 100.0f;  // 레이스 트랙 시작 위치 (X)
    public float trackEndX = 1820.0f;  // 레이스 트랙 끝 위치 (1920 - 버튼 크기)
    
    public float distanceToCanvasScale = 10.0f;

    public TextMeshProUGUI winnerText;
    public TextMeshProUGUI timeText;
    IEnumerator BattlerTimer()
    {
        for (var i = 0; i < Players.Count; i++)
        {
            // 오브젝트 생성하기
            var newObj = Instantiate(templateButton.gameObject, RaceButtonParent);
            
            // RaceButton 컴포넌트 캐싱하기
            var raceButton = newObj.GetComponent<RaceButton>();
            raceButtons.Add(newObj.GetComponent<RaceButton>());
            
            raceButton.text.text = Players[i].playerName;
            
            raceButton.rect = newObj.GetComponent<RectTransform>();
        }

        while (battleTime >= 0.0f)
        {
            Debug.Log(battleTime);
            
            // 이 함수는 1초동안 쉰다.
            yield return new WaitForSeconds(0.1f);
            
            foreach (var playerData in Players)
            {
                playerData.Distance += Random.Range(0.0f, 2.0f);   
            }
            
            var ranks = (from p in Players orderby p.Distance select p).ToList ();
            
            
            for (var i = 0; i < Players.Count; i++)
            {
                Debug.Log($"Rank {i+1} : {ranks[i].playerName} / distance : {ranks[i].Distance}");
                //raceButtons[i].text.text = ranks[i].playerName;
                var playerData = Players[i];

                // Distance를 캔버스의 절대 위치로 변환
                float absoluteX = trackStartX + (playerData.Distance * distanceToCanvasScale);

                // 트랙 끝을 넘지 않도록 제한
                absoluteX = Mathf.Min(absoluteX, trackEndX);

                // 버튼 위치 업데이트
                raceButtons[i].UpdatePosition(absoluteX);
                
            }
            
            // 어떠한 값이 참이 될때가지 기다리는 YieldInstruction
            // yield return new WaitUntil();

            // 물리 적용이 끝난 시점까지 기다리는 코루틴
            // yield return new FixedUpdate();
            timeText.text = $"{battleTime:0.0}";
            battleTime -= 0.1f;
        }

        DisplayWinner();


    }

    void DisplayWinner()
    {
        var winner = Players.OrderByDescending(p => p.Distance).First();
        
        if (winner != null)
        {
            winnerText.text = $"Winner: {winner.playerName} with {winner.Distance:F1} meters!";
            winnerText.gameObject.SetActive(true); // 텍스트 활성화
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        winnerText.gameObject.SetActive(false);
        // 코루틴 함수를 시작한다.
        StartCoroutine(BattlerTimer());
        
    }

    private float _stepBattleDuration = 1.0f;
    
    // Update is called once per frame
    void Update()
    {
        //Time.realtimeSinceStartup;

        // 1초당 60프레임이다 1/60 = time.deltaTime이 된다.
        // 1초당 120프레임이면 1/120 = time.deltaTime이 된다.
        // Time.deltaTime;

        // 업데이트를 이용한 방법
        // if (0 >= battleTime)
        //     return;
        //
        // if (_stepBattleDuration >= 1.0f)
        // {
        //     Debug.Log(battleTime);
        //     
        //     battleTime -= 1.0f;
        //     _stepBattleDuration = 0.0f;
        // }
        //
        // _stepBattleDuration += Time.deltaTime;
    }
}
