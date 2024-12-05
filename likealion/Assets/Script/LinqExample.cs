using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public struct MonsterTest
{
    public string name;
    public int health;
}
public class LinqExample : MonoBehaviour
{
    public List<MonsterTest> monsters = new List<MonsterTest>()
    {
        new MonsterTest() { name = "A", health = -50 },
        new MonsterTest() { name = "A", health = 30 },
        new MonsterTest() { name = "B", health = 100 },
        new MonsterTest() { name = "B", health = 30 },
        new MonsterTest() { name = "C", health = 100 },
        new MonsterTest() { name = "C", health = 30 },
    };

    void Start()
    {
        
        //linq 미사용
        List<MonsterTest> filters = new List<MonsterTest>();
        for (var i = 0; i < monsters.Count; i++)
        {
            if (monsters[i].name == "A" && monsters[i].health >= -50)
            {
                filters.Add(monsters[i]);
            }
        }
        filters.Sort((l, r) => l.health >= r.health ? -1 : 1);
        for (var i = 0; i < filters.Count; i++)
        {
            Debug.Log($"Name: {filters[i].name}, Health: {filters[i].health}");
        }
        
        //linq 사용
        var linqFilter = monsters.Where(m => m.name == "A" && m.health >= -50).
            OrderByDescending(m => m.health).ToList();
        for (var i = 0; i < linqFilter.Count; i++)
        {
            Debug.Log($"Name: {linqFilter[i].name}, Health: {linqFilter[i].health}");
        }
        
        var linqfilter2 = ( 
            from m in monsters
            where m.name == "A" && m.health >= -50
            orderby m.health 
                descending select m).ToList();
        for (var i = 0; i < linqfilter2.Count; i++)
        {
            Debug.Log($"Name: {linqfilter2[i].name}, Health: {linqfilter2[i].health}");
        }
        
    }

}
