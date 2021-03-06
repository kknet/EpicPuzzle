﻿using UnityEngine;

public class ScaleAction : BaseAction
{
	// The helper
	private LerpVector3Helper _helper = new LerpVector3Helper();

	// Relative or absolute
	private bool _isRelative;

	// The transform
	private Transform _transform;

	public ScaleAction(Vector3 end, bool isRelative, float duration, Easer easer, LerpDirection direction)
	{
		_helper.Construct(end, duration, easer, direction);

		_isRelative = isRelative;
	}

	public static ScaleAction Create(Vector3 end, bool isRelative, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return new ScaleAction(end, isRelative, duration, easer, direction);
	}

	public static ScaleAction ScaleTo(Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, duration, easer, direction);
	}
	
	public static ScaleAction ScaleBy(Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, true, duration, easer, direction);
	}

	public override void Play(GameObject target)
	{
		// Get transform
		_transform = target.transform;

		// Set start
		_helper.Start = _transform.localScale;

		if (_isRelative)
		{
			_helper.AddEndByStart();
		}

		_helper.Play();
	}
	
	public override void Reset()
	{
		_transform.localScale = _helper.Start;
	}
	
	public override void Stop(bool forceEnd = false)
	{
		if (!_helper.IsFinished())
		{
			_helper.Stop();

			if (forceEnd)
			{
				_transform.localScale = _helper.End;
			}
		}
	}
	
	public override bool IsFinished()
	{
		return _helper.IsFinished();
	}
	
	public override bool Update(float deltaTime)
	{
		if (!_helper.IsFinished())
		{
			_transform.localScale = _helper.Update(deltaTime);
		}

		return _helper.IsFinished();
	}
}
