using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NowLoading : MonoBehaviour {

	//　非同期動作で使用するAsyncOperation
	private AsyncOperation async;
	//　読み込み率を表示するスライダー
	[SerializeField]
	private Slider slider;

	public void LoadingStart() {
		slider.gameObject.SetActive(true);
		//　コルーチンを開始
		StartCoroutine("LoadData");
	}

	IEnumerator LoadData() {
		// シーンの読み込みをする
		async = SceneManager.LoadSceneAsync("FlightScene");

		//　読み込みが終わるまで進捗状況をスライダーの値に反映させる
		while(!async.isDone) {
			var progressVal = Mathf.Clamp01(async.progress / 0.9f);
			slider.value = progressVal;
			yield return null;
		}
	}
}
