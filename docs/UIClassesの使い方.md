# UIClassesの使い方

## 前提
親オブジェクト，もしくはより祖先のオブジェクトには，
`Canvas`コンポーネント，`Canvas Scaler`コンポーネント，`Graphinc Raycaster`コンポーネントが
追加されている必要があります．

これらが追加されていないと，正しく描写しない，
あるいは，UIが操作を受け付けないなどといった症状が出ることがあります．

## RectTransformについて
このクラス内部では表示サイズや位置決めに関する設定を行わないので，
プロパティから`RectTransform`を取得して設定する必要があります．

その際，`fontSize`を変更するのではなく，`localScale`を変更することをおすすめします．
特に，`DynamicDropdown`のような複雑なUIほど，表示が崩れやすいです．

推奨するサイズ設定・位置決め方法は，
1. `anchorMin, anchorMax, pivot, anchoredPosition`で親に対する基準を設定．
2. `localScale`で大きさを設定．
3. `SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);`などとして幅のみ設定．


## ActionButton

### 使い方
```cs
ActionButton buttonChangePageRight = new(basePanel, "ButtonChangePageRight", ">", 50.0f, OnClickChangePage);
Gameobject buttonObj = buttonChangePageRight.gameObject; // プロパティにより取得.
RectTransform buttonChangePageRightRect = buttonChangePageRight.rectTransform; // プロパティにより取得.
```
### 引数
- basePanel: 親のゲームオブジェクト.
- "ButtonChangePageRight": 生成するボタンのゲームオブジェクト名.
- ">": ボタンに表示するテキスト.
- 50.0f: フォントサイズ.
- OnClickChangePage: ボタンがクリックされたときに呼び出される関数.

## DynamicSlider

### 使い方
```cs
DynamicSlider slider = new(basePanel, "SliderTest", 
  (x) => { gm.massRightFactor = x; }, () => { return gm.massRightFactor; }, 0.0f, 1.0f, 0.1f);
GameObject sliderobj = slider.gameObject; // プロパティにより取得.
RectTransform sliderRect = slider.rectTransform; // プロパティにより取得.
```
### 引数
- basePanel: 親のゲームオブジェクト
- "SliderTest": 生成するスライダーのゲームオブジェクト名
- (x) => { gm.massRightFactor = x; }: セッターの関数（ラムダ式で書くのが一番簡単）
- () => { return gm.massRightFactor; }: ゲッターの関数（ラムダ式で書くのが一番簡単）
- 0.0f: 最小値
- 1.0f: 最大値
- 0.1f: スライダーのステップ（省略可能）

## StaticText

### 使い方
```cs
string dist = Distance.ToString("0.000") + " m";
StaticText<string> textDist = new(basePanel, "TextDist", dist, 150.0f);
GameObject textObj = textDist.gameObject; // プロパティにより取得.
RectTransform textDistRect = textDist.rectTransform; // プロパティにより取得.
```
`<string>`を`<float>`としてクラス内で文字列変換する事もできるが，
ここでは`m`を連結するために`<string>`でインスタンス化している．

### 引数
- basePanel: 親のゲームオブジェクト.
- "TextDist": 生成するテキストのゲームオブジェクト名. 
- dist: 表示する内容（この例では距離を文字列に変換）. 
- 150.0f: フォントサイズ.

## DynamicDropdown

### 使い方
```cs
DynamicDropdown dynamicDropdown = new(scrollContent, "DropdownTest", categories, (x) => { Debug.Log("Selected: " + x); });
RectTransform dpdnRect = dynamicDropdown.rectTransform;
dpdnRect.anchoredPosition = new Vector2(0, -100);
dpdnRect.localScale = new Vector3(3, 3, 1); // ドロップダウンのサイズを変更する
dpdnRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 300); // RectTransformのx軸方向のサイズを変更する
```
### 引数
- basePanel: 親のゲームオブジェクト
- "DropdownTest": 生成するドロップダウンのゲームオブジェクト名
- categories: ドロップダウンのオプションのリスト
- (x) => { Debug.Log("Selected: " + x); }: ドロップダウンの値が変更されたときのコールバック関数（ラムダ式で書くのが一番簡単）

## DynamicText

### 使い方
```cs
DynamicText<float> dynamicText = new(basePanel, "TextDistUpper", 
  () => { return gm.massLeftFactor; }, 50.0f);
GameObject textObj = dynamicText.gameObject; // プロパティにより取得.
RectTransform textRect = dynamicText.rectTransform; // プロパティにより取得.
```

### 引数
- basePanel: 親のゲームオブジェクト
- "TextDistUpper": 生成するテキストのゲームオブジェクト名
- () => { return gm.massLeftFactor; }: ゲッターの関数（ラムダ式で書くのが一番簡単）.
- 50.0f: フォントサイズ.

## Chart

### 使い方
```cs
Chart airdataChart1 = new(basePanel, "ChartPitchAlpha", 
  "ピッチ(theta)", gm.ThetaList, "迎角(alpha)", gm.AlphaList);
GameObject chartObj = airdataChart1.gameObject; // プロパティにより取得.
RectTransform AirdataChart1Rect = airdataChart1.rectTransform; // プロパティにより取得.
```
### 引数
- basePanel: 親のゲームオブジェクト.
- "ChartPitchAlpha": 生成するグラフのゲームオブジェクト名.
- "ピッチ(theta)": グラフの1つ目のデータセットの名前.
- gm.ThetaList: グラフの1つ目のListのデータセット.
- "迎角(alpha)": グラフの2つ目のListのデータセットの名前（省略可能）.
- gm.AlphaList: グラフの2つ目のListのデータセット（省略可能）.
