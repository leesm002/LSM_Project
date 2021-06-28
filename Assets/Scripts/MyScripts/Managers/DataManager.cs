using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public interface MobILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
   public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
   public Dictionary<int, Data.MobStat> MobStatDict { get; private set; } = new Dictionary<int, Data.MobStat>();

   public void Init()
    {
        StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();

        MobStatDict = LoadJson<Data.MobStat, int, Data.MobStat>("MobStatData").MakeDict();
    }

    Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
    {
        TextAsset textAsset = Managers.GetResourceManager.Load<TextAsset>($"Data/{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
        
    }


}
