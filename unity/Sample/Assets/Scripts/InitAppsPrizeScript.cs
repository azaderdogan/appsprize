using UnityEngine;
using UnityEngine.UI;

using AppsPrizeUnity;


public class InitAppsPrizeScript : MonoBehaviour
{
	[SerializeField]
    private InitializeAppsPrizeScript initAppsPrize;

	void Start () {
		Button btn = GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick(){
		initAppsPrize.Start();
	}
}
