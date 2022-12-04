﻿using UnityEngine;
using Puerts;
using System;

namespace PuertsTest
{
    public delegate void ModuleInit(JsBehaviour monoBehaviour);

    //只是演示纯用js实现MonoBehaviour逻辑的可能，
    //但从性能角度这并不是最佳实践，会导致过多的跨语言调用
    public class JsBehaviour : MonoBehaviour
    {
        public string JSFileName;//可配置加载的js模块

        public Action JsStart;
        public Action JsUpdate;
        public Action JsOnDestroy;

        static JsEnv jsEnv;

        void Awake()
        {   
            PreventStripping();
            
            if (jsEnv == null) 
            {
                jsEnv = Puerts.WebGL.MainEnv.Get();
            }

            var init = jsEnv.ExecuteModule<ModuleInit>(JSFileName, "init");
            
            if (init != null) init(this);

        }

        void Start()
        {
            if (JsStart != null) JsStart();
        }

        void Update()
        {
            if (JsUpdate != null) JsUpdate();
        }

        void OnDestroy()
        {
            if (JsOnDestroy != null) JsOnDestroy();
            JsStart = null;
            JsUpdate = null;
            JsOnDestroy = null;
            jsEnv.Dispose();
        }

        //Prevent unity il2cpp code stripping
        void PreventStripping()
        {
            // Vector3 vector = new Vector3();
            // vector = Vector2
            transform.Rotate(new Vector3(0, 0, 0));
            
            UnityEngine.Debug.Log(UnityEngine.Vector3.up);
        }
    }
}