using UnityEngine;
using System.Collections;

// frame timer
// 일정 시간 후 알림
// 일정 시간 후 반복 알림
// 일정 시간 후 반복횟수만큼 알림

public class NcTimerTool_B
{
	// Attribute ------------------------------------------------------------------------
	protected	bool		m_bEnable;
	private		float		m_fStartTime;
	private		float		m_fPauseTime;

	// Property -------------------------------------------------------------------------
	public static float GetEngineTime()
	{
		if (Time.time == 0)
			return 0.000001f;
		return Time.time;
	}

	public float GetTime()
	{
        float fEngineTime = NcTimerTool_B.GetEngineTime();
		if (m_bEnable == false && m_fPauseTime != fEngineTime)
		{
            m_fStartTime += NcTimerTool_B.GetEngineTime() - m_fPauseTime;
			m_fPauseTime	= fEngineTime;
		}
        return NcTimerTool_B.GetEngineTime() - m_fStartTime;
	}

	public float GetDeltaTime()
	{
		if (m_bEnable)
			return Time.deltaTime;
		return 0;
	}

	public bool IsEnable()
	{
		return m_bEnable;
	}

	public void Start()
	{
		m_bEnable		= true;
		m_fStartTime	= GetEngineTime() - 0.000001f;
	}

	public void Reset(float fAdjustTime)
	{
		m_fStartTime	= GetEngineTime() - fAdjustTime;
	}

	public void Pause()
	{
		m_bEnable		= false;
        m_fPauseTime = NcTimerTool_B.GetEngineTime();
	}

	public void Resume()
	{
		GetTime();
		m_bEnable = true;
	}

	// Control Function -----------------------------------------------------------------
}
