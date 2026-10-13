<div align="center">

<img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="Game Frame X Logo" width="160" />

# GameFrameX Setting（KuaiShou ミニゲームストレージ）

[![License](https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.setting.kuaishouminigame)](https://github.com/GameFrameX/com.gameframex.unity.setting.kuaishouminigame/blob/main/LICENSE.md)
[![Version](https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.setting.kuaishouminigame)](https://github.com/GameFrameX/com.gameframex.unity.setting.kuaishouminigame/releases)
[![Unity Version](https://img.shields.io/badge/Unity-2019.4-black?logo=unity)](https://unity.com/)
[![Documentation](https://img.shields.io/badge/Documentation-docs-blue)](https://gameframex.doc.alianblank.com)

インディーゲーム開発のオールインワンソリューション · インディー開発者の夢を応援

<br />

[ドキュメント](https://gameframex.doc.alianblank.com) · [クイックスタート](#クイックスタート) · QQグループ: 467608841 / 233840761

<br />

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | **日本語** | [한국어](README.ko.md)

</div>

## 概要

GameFrameX Setting（KuaiShou ミニゲーム）は [Setting メインパッケージ](https://github.com/GameFrameX/com.gameframex.unity.setting)の KuaiShou ミニゲームストレージバックエンドチャネルパッケージです。KuaiShou ミニゲーム SDK（`KSWASM`）の同期ストレージ API の上に `ISettingStorageBackend` を実装し、`SubsystemRegistration` タイミングで自己登録します（参照即登録。手動設定は不要）。

### 主な特徴

- **KuaiShou ミニゲームストレージ統合** — `KSBase` の同期ストレージ API をラップ
- **自己登録（参照即登録）** — `RuntimeInitializeOnLoadMethod(SubsystemRegistration)` で `SettingComponent` がバックエンドを初生成する前に自己登録
- **単一スロット排他 + PlayerPrefs フォールバック** — メインパッケージの `SettingStorageBackendFactory` 契約に準拠。Editor および未登録環境は Unity PlayerPrefs にフォールバック
- **条件付きコンパイル** — asmdef `defineConstraints`（`UNITY_WEBGL` + `ENABLE_KUAISHOU_MINI_GAME`）。ミニゲーム以外のプロジェクトではコンパイルされません
- **IL2CPP 裁剪保護** — `Preserve` + `CroppingHelper`

## クイックスタート

### インストール

前提：`com.gameframex.unity.setting` 3.0.0+ および KuaiShou ミニゲーム SDK（`kuaishou.runtime` アセンブリを含む）がインストール済みであること。

以下のいずれかの方法でインストールします：

1. Unity プロジェクトの `Packages/manifest.json` を編集し、`scopedRegistries` セクションを追加：
   ```json
   {
     "scopedRegistries": [
       {
         "name": "GameFrameX",
         "url": "https://gameframex.upm.alianblank.uk",
         "scopes": [
           "com.gameframex"
         ]
       }
     ],
     "dependencies": {
       "com.gameframex.unity.setting.kuaishouminigame": "1.0.0"
     }
   }
   ```
2. `manifest.json` の `dependencies` に直接追加：
   ```json
   {
      "com.gameframex.unity.setting.kuaishouminigame": "https://github.com/gameframex/com.gameframex.unity.setting.kuaishouminigame.git"
   }
   ```
3. Unity の `Package Manager` で `Git URL` を使用：`https://github.com/gameframex/com.gameframex.unity.setting.kuaishouminigame.git`
4. リポジトリを直接ダウンロードして Unity プロジェクトの `Packages` ディレクトリに配置すると自動的に読み込まれます。

### 仕組み

インストール後のコード変更は不要です：

- `KuaiShouMiniGameSettingStorage` は `SubsystemRegistration` タイミングで `SettingStorageBackendFactory.RegisterMiniGameBackend` を呼び出して自己登録します；
- `SettingComponent` は初回生成時に登録済みのプラットフォームバックエンドを使用します。Editor およびチャネルパッケージ未登録の環境は Unity PlayerPrefs にフォールバックします；
- 重複登録は先頭を保持し Warning を出力します（ストレージバックエンドは「実行環境ごとに最大 1 つ」の排他セマンティクス）。

## 依存

| パッケージ | 説明 |
|------------|------|
| `com.gameframex.unity.setting` | 3.0.0+。公共契約と PlayerPrefs フォールバック |
| KuaiShou ミニゲーム SDK | `KSWASM`（`kuaishou.runtime` アセンブリ）を提供。KuaiShou ミニゲーム変換ツールチェーンにより導入 |

## プロジェクト構成

```
Runtime/
├── KuaiShou/
│   └── KuaiShouMiniGameSettingStorage.cs      # KuaiShou ミニゲームストレージバックエンド
├── GameFrameXSettingKuaiShouMiniGameCroppingHelper.cs  # 裁剪補助クラス
└── GameFrameX.Setting.KuaiShouMiniGame.Runtime.asmdef  # アセンブリ定義（defineConstraints 排他）
```

## 2.x からの移行

Setting メインパッケージ 3.0.0 は内蔵の `Runtime/MiniGame/` ミニゲームバックエンドを削除しました。KuaiShou ミニゲームプロジェクトは本チャネルパッケージを追加するだけで、ストレージ動作は 2.x と完全に同一です。

## ドキュメントとリソース

- [公式ドキュメント](https://gameframex.doc.alianblank.com)
- [変更履歴](./CHANGELOG.md)

## コミュニティとサポート

- QQグループ: 467608841 / 233840761

## ライセンス

[LICENSE.md](LICENSE.md) を参照してください。
