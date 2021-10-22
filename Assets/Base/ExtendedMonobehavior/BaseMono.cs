﻿using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using UnityEngine;

namespace Base
{
    public class BaseMono : MonoBehaviour
    {
        [SerializeField, ReadOnly] private int id;
        
        [Button("Create ID")]
        public void CreateId()
        {
            if (id == 0)
            {
                id = GameMaster.GenerateId();
            }
            else
            {
                GameMaster.RemoveId(id);
                id = GameMaster.GenerateId();
            }

        }
        
        private CancellationTokenSource _cancellation = new CancellationTokenSource();

        public int Id => id;
        public int InstanceId => CacheGameObject.GetInstanceID();

        public Transform CacheTransform => transform;

        public GameObject CacheGameObject => gameObject;

        public Transform Parent
        {
            get => CacheTransform.parent;
            set => CacheTransform.SetParent(value);
        }

        public Vector3 Position
        {
            get => CacheTransform.position;
            set => CacheTransform.position = value;
        }

        public Quaternion Rotation
        {
            get => CacheTransform.rotation;
            set => CacheTransform.rotation = value;
        }

        public Vector3 EulerAngles
        {
            get => CacheTransform.eulerAngles;
            set => CacheTransform.eulerAngles = value;
        }

        public Vector3 Scale
        {
            get => CacheTransform.localScale;
            set => CacheTransform.localScale = value;
        }

        protected virtual void OnDestroy()
        {
            _cancellation.Cancel();
            _cancellation.Dispose();
        }

        public GameObject CacheInstantiate()
        {
            var obj = Instantiate(gameObject);
            return obj;
        }

        public GameObject CacheInstantiate(Vector3 pos, Quaternion rotate, Transform parent)
        {
            var obj = Instantiate(gameObject, pos, rotate, parent);
            return obj;
        }

        public async UniTaskVoid DelayAction(float time, Action runAfter = null)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(time), cancellationToken: _cancellation.Token);
            runAfter?.Invoke();
        }
    }
}

