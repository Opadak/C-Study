/************************************************************************************
 * Title    : SKT 3D Spatial Audio PlugIn
 * Date     : 2018.05.02
 * Version  : 0.1
 * Class    : Singleton Base Class
 * Copyright 2018 SK Telecom. All Rights reserved.
 ************************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Unity Component 특성상 Singleton Component Class를 구성하고 
/// Monobehavior를 상속받는 모든 Class들은 Singleton Class를 상속받도록 한다
/// 위와 같이 하고 싶을 땐  Singleton<T> : MonoBehaviour where T : MonoBehaviour 로 만들어 주면 된다. 
/// 오디오 채널을 생성하고 채널의 크기마다 채널을 분리 시켜 GameObject에 SingleTon한테 정보를 가지고 와준다. 
/// new를 한번만 선언해 주고 선언해 준 곳에다가 Gameobject의 값을 참조해서 보여준다. 
/// >> 이해가 안갔던 부분 =  참조해서 보여주는것보다 힙에다가 저장하는 것이 더 메모리 효율이 안좋나보다.
/// get set프로퍼티를 만들 때 get을 가지고 오는 것은 GameObject의 정보를 get으로 가지고와 읽기 전용으로 사용하기 위해 
/// public static인것은 하나만 사용하기 위해서 내가 OOP전용 스크립트에서 score를 static으로 선언한것과 동일. 
///이해완,,
/// public static T Instance 에서 Instance는 T를 인스턴스로 만들어 읽기 전용으로 사용한다는 의미 
/// </summary>
namespace skt.plugin.pcm.comm
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// 참조 인스턴스 변수
        /// </summary>
        protected static T instance = null;

        protected virtual void Start() { }
        protected virtual void Update() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        protected virtual void OnApplicationPause(bool active) { }
        protected virtual void OnApplicationFocus(bool focus) { }

        /// <summary>
        /// Awake시 인스턴스를 찿는다
        /// </summary>
        protected virtual void Awake()
        {
            if (instance != null)
            {
                return;
            }

            // 인스턴스가 null일 경우 해당 타입의 인스턴스를 찿아 반환한다
            instance = FindObjectOfType<T>();
        }

        /// <summary>
        /// OnDestroy시 인스턴스를 null 처리한다
        /// </summary>
        protected virtual void OnDestroy()
        {
            if (instance != null) instance = null;
        }


        /// <summary>
        /// 클래스 인스턴스 참조
        /// </summary>
        public static T Instance
        {
            get
            {
                // 인스턴스가 없을 경우 새로운 GameObject를 생성한다
                //default값
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.transform.position = Vector3.zero;
                    go.transform.rotation = Quaternion.identity;
                    go.transform.localScale = Vector3.one;
                    go.hideFlags = HideFlags.HideAndDontSave;
                    instance = go.AddComponent<T>();
                    instance.name = (typeof(T)).ToString();
                }

                return instance;
            }
        }
    }
}

