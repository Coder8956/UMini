﻿using System.Collections;
using Game.Scripts.Gameplay.Monster;
using UnityEngine;

namespace Game.Scripts.Gameplay.MonsterCreator
{
    public class BossMonsterCreator : MonsterCreatorBase
    {
        public override void Create()
        {
            StartCoroutine(CreateNormalMonster());
        }

        private IEnumerator CreateNormalMonster()
        {
            yield return new WaitUntil(() => { return m_monsterPool != null; });
            BossMonster monster = m_monsterPool.Get().GetComponent<BossMonster>();
            monster.transform.SetParent(null);
            monster.transform.position = transform.position;
            monster.transform.rotation = transform.rotation;
            monster.OnBorn(m_monsterPool);
        }

        public override void Stop()
        {
        }

        public override int CreateType()
        {
            return 2;
        }
    }
}