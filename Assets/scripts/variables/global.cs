using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class globalVariable { 
	public static bool isTopView = true;

	public static void setTopView(bool view) {
		isTopView = view;
	}
}
