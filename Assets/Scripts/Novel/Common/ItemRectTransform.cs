using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRectTransform
{

	Vector3 _pos;
	Vector2 _size;

	public ItemRectTransform (Vector3 pos, Vector2 size)
	{
		_pos = pos;
		_size = size;
	}

	public void Set (RectTransform rectTransform)
	{
		rectTransform.localPosition = _pos;
		rectTransform.sizeDelta = _size;
	}

}
