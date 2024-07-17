﻿using System.Collections;
using UMiniFramework.Scripts.Pool.GameObjectPool;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class GameBullet : MonoBehaviour
    {
        private GameObjectPool m_pool;
        private GameObjectPool m_bulletExplosionPool;
        private Coroutine m_backPoolCoro;
        private WaitForSeconds m_wfsBackPool = new WaitForSeconds(2);
        private Rigidbody m_rig;

        private void OnCollisionEnter(Collision other)
        {
            Explosion();
        }

        private void Explosion()
        {
            StopCoroutine(m_backPoolCoro);
            GameObject explosion = m_bulletExplosionPool.Get();
            explosion.GetComponent<BulletExplosion>().Play(transform.position, m_bulletExplosionPool);
            BackPool();
        }

        public void Shooting(GameObject shootingPoint, int force, GameObjectPool bulletPool,
            GameObjectPool bulletExplosionPool)
        {
            m_bulletExplosionPool = bulletExplosionPool;
            if (m_rig == null)
            {
                m_rig = GetComponent<Rigidbody>();
            }

            transform.SetParent(null);
            m_pool = bulletPool;
            transform.position = shootingPoint.transform.position;
            transform.rotation = shootingPoint.transform.rotation;
            m_rig.AddForce(transform.forward * force, ForceMode.Force);
            m_backPoolCoro = StartCoroutine(BackPoolIEnum());
        }

        private IEnumerator BackPoolIEnum()
        {
            yield return m_wfsBackPool;
            BackPool();
        }

        private void BackPool()
        {
            m_rig.velocity = Vector3.zero;
            m_rig.angularVelocity = Vector3.zero;
            m_pool.Back(gameObject);
        }
    }
}