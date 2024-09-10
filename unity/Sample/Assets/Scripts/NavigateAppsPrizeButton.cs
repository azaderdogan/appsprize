using UnityEngine;
using UnityEngine.UI;

using AppsPrizeUnity;


public class NavigateAppsPrizeButton : MonoBehaviour
{

	void Start () {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){

		AppsPrize.LaunchActivity();
	}
}
