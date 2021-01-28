using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// An interface for objects with a boundary that will be rendered with ASCII characters
public interface Bounded {
	Rect GetBounds();
}
