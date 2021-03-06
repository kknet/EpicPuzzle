﻿using UnityEngine;

public class VMoveAction : BaseAction
{
	// The helper
	private LerpFloatHelper _helper = new LerpFloatHelper();

	// Relative or absolute
	private bool _isRelative;

	// Local or world
	private bool _isLocal;

	// The transform
	private Transform _transform;

	public VMoveAction(float end, bool isRelative, bool isLocal, float duration, Easer easer, LerpDirection direction)
	{
		_helper.Construct(end, duration, easer, direction);

		_isRelative = isRelative;
		_isLocal    = isLocal;
	}

	public static VMoveAction Create(float end, bool isRelative, bool isLocal, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return new VMoveAction(end, isRelative, isLocal, duration, easer, direction);
	}

	public static VMoveAction MoveTo(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, false, duration, easer, direction);
	}
	
	public static VMoveAction MoveBy(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, true, false, duration, easer, direction);
	}
	
	public static VMoveAction MoveLocalTo(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, true, duration, easer, direction);
	}
	
	public static VMoveAction MoveLocalBy(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, true, true, duration, easer, direction);
	}

	public override void Play(GameObject target)
	{
		// Get transform
		_transform = target.transform;

		// Get start
		_helper.Start = _isLocal ? _transform.localPosition.y : _transform.position.y;

		if (_isRelative)
		{
			_helper.AddEndByStart();
		}

		_helper.Play();
	}
	
	public override void Reset()
	{
		if (_isLocal)
		{
			_transform.SetLocalPositionY(_helper.Start);
		}
		else
		{
			_transform.SetPositionY(_helper.Start);
		}
	}
	
	public override void Stop(bool forceEnd = false)
	{
		if (!_helper.IsFinished())
		{
			_helper.Stop();

			if (forceEnd)
			{
				if (_isLocal)
				{
					_transform.SetLocalPositionY(_helper.End);
				}
				else
				{
					_transform.SetPositionY(_helper.End);
				}
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
			if (_isLocal)
			{
				_transform.SetLocalPositionY(_helper.Update(deltaTime));
			}
			else
			{
				_transform.SetPositionY(_helper.Update(deltaTime));
			}
		}

		return _helper.IsFinished();
	}
}
