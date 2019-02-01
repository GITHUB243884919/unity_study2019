using UnityEngine;
using System.Collections;

/// <summary>
/// 一种震动执行器实现
/// </summary>
public class CShakeBySinCosExecutor : CExecutor
{
    public class CShakeParam : CExecutor.CParam
    {
        public CShakeParam()
        {
            m_trans = null;
        }
        public Transform m_trans;

        //以下是震动参数，时长参数没单独定义，因为借用了父类的SurviveTime
        /// <summary>
        /// 振幅
        /// </summary>
        public float m_amplitude;

        /// <summary>
        /// 频率
        /// </summary>
        public float m_frequency;
    }
    
    public override void InitParam(CExecutor.CParam param)
    {
        base.InitParam(param);
        CShakeParam SharkeParam = param as CShakeParam;
        if (null != SharkeParam)
        {
            m_trans     = SharkeParam.m_trans;
            m_amplitude = SharkeParam.m_amplitude;
            m_frequency = SharkeParam.m_frequency;
        }
    }
 
    public override void Stop()
    {
        base.Stop();
        m_trans.position = m_orgPosition;
    }
    public override void Play()
    {
        if (SurviveTime <= 0)
        {
            return;
        }

        m_orgPosition = m_trans.position;
        m_speed       = 1.0f / SurviveTime;
        base.Play();
    }
    protected override bool updateImp(float fDeltaTime)
    {
        if (SurviveTime == 0f)
        {
            return false;
        }

        if  (m_scale < 1.0f)
        {
            m_scale += fDeltaTime * m_speed;
            m_trans.position = m_orgPosition + new Vector3(Mathf.Sin(m_frequency * m_scale), Mathf.Cos(m_frequency * m_scale), 0f) 
                * Mathf.Lerp(m_amplitude, 0f, m_scale);
        }
        
        return false;
    }

    public Transform m_trans;
    public float     m_amplitude;
    public float     m_frequency;
    public float     m_time;
    public Vector3   m_orgPosition;
    public float     m_scale = 0f;
    public float     m_speed = 0f;
}
