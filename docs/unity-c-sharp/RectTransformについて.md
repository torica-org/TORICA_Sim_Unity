# `RectTransform`について


## 「親」と「子」
ヒエラルキー（`Hierarchy`：画面左上）で表示されるゲームオブジェクト（`GameManager`や`Button`などのあらゆるオブジェクト）は入れ子関係にすることができます．

これらが3D空間上に配置されるとき，「子」は「親」の位置を基準に配置されます．  
そのため，「親」を移動すると「子」も連動して移動します．

この「親」と「子」の位置関係を柔軟に決めるための仕組みが，**アンカー**（`Anchor`）と**ピボット**（`Pivot`）です．


## アンカー（`Anchor`）
「親」のどこを基準にするかという設定です．

「子」に設定します．最大値と最小値があります．


## ピボット（`Pivot`）
自身のどこを基準にするかという設定です．

これも「子」に設定します．

## よくやるプロパティ使用例
```cs
RectTransform rect = （あるGameObject）.GetComponent<RectTransform>();
```
とすると，
- rect.anchorMax = new Vector2(1f, 0.5f); // アンカーの最小値
- rect.anchorMax = new Vector2(1f, 0.5f); // アンカーの最大値
- rect.pivot = new Vector2(1f, 0.5f); // ピボット（ボタン自身の基準点）
- rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50); // RectTransformのx軸方向のサイズを変更する
- rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 150); // RectTransformのy軸方向のサイズを変更する
- rect.anchoredPosition = new Vector2(-20, 0); // アンカーを基準にした座標 (pos\_x, pos\_y) を設定

## 参考になりそうなページ
- [【Unity】RectTransformとは？UIの表示場所を決めるコンポーネント | ともくんのゲーム作り部屋](https://tomokun-games.com/unity-recttransform/)
- [【Unity】RectTransform.sizeDeltaの仕様と注意点 | ねこじゃらシティ](https://nekojara.city/unity-rect-transform-size-delta)

