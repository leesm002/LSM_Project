using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Data
{

    #region Stat

    [Serializable]
    public class Stat
    {
        public int level;
        public int maxHp;
        public int attack;
        public int totalExp;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        /// <summary>
        /// Key가 int형, Value가 Stat형인 Dictionary를 만들어 Stat에 있는 변수들 (level, hp, attack) 을 추가하여 반환
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);
            return dict;
        }
    }
    #endregion
}