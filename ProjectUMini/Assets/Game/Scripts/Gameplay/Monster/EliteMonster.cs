﻿using UMiniFramework.Scripts.Pool.GameObjectPool;
using UnityEngine;

namespace Game.Scripts.Gameplay.Monster
{
    public class EliteMonster : MonsterBase
    {
        public override void OnBorn(GameObjectPool monsterPool)
        {
            m_animator.Play("Idle");
        }

        public override void OnDamage(float val)
        {
            m_animator.Play("Damage");
        }

        private void OnDamageOver()
        {
            m_animator.Play("Idle");
        }
    }
}