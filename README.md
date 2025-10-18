# CSStack.TADA.MagicOnionHelper

English | 日本語

---

English

CSStack.TADA.MagicOnionHelper is a helper library for turning TADA (Transaction-Aware Domain Architecture) use cases into MagicOnion-based gRPC APIs. It standardizes DTO mapping between your domain (UseCase) layer and MessagePack transport types, and provides base classes for implementing server and client services cleanly.

Highlights
- Abstractions: Common interfaces to define MessagePack DTOs and service shapes
  - IMPDTO<TDTO, TSelf>: Bidirectional mapping between UseCase DTO and MessagePack type
  - IMOCommandService / IMOQueryService: MagicOnion service contracts
- Server bases: MOCommandServiceBase, MOQueryServiceBase
- Client bases: MOCommandServiceClientBase, MOQueryServiceClientBase
- Optional bridging: MPOptional<T> and OptionalExtensions to map optional values between UseCase and MessagePack
- Cancellation propagation: CancellationToken flows from MagicOnion context to UseCase

Projects
- CSStack.TADA.MagicOnionHelper.Abstractions: Core interfaces and shared models (IMPDTO, MPOptional, service interfaces)
- CSStack.TADA.MagicOnionHelper.Server: Server-side base classes for MagicOnion services
- CSStack.TADA.MagicOnionHelper.Client: Client-side base classes and a gRPC channel factory abstraction
- CSStack.TADA.MagicOnionHelper: Meta/project container

Quick Start
1) Define your UseCase DTOs and services (e.g., ICommandService/IQueryService and DTOs implementing ICommandServiceDTO/IQueryServiceDTO).
2) Create MessagePack transport types implementing IMPDTO<TDTO, TSelf>.
3) Implement a MagicOnion service interface using IMOCommandService/IMOQueryService.
4) Derive a server base (MOCommandServiceBase/MOQueryServiceBase) and delegate to your UseCase service.
5) On the client, use MOCommandServiceClientBase/MOQueryServiceClientBase and inject an IMOClientChannelFactory.

Optional values
- Use MPOptional<T> to carry optional values across the wire.
- Convert from Optional<T> by MPOptional.FromOptional or OptionalExtensions.ToMPOptional.
- Convert back by MPOptional.ToOptional.

Obsolete types
- Interfaces/classes ending with WithRes or WithoutReq are obsolete in favor of IMOCommandService/IMOQueryService and their base classes.

Docs
- Detailed usage (EN): docs/USAGE.en.md
- 詳細な使用方法 (JA): docs/USAGE.ja.md

License
MIT. See LICENSE.txt.

---

日本語

CSStack.TADA.MagicOnionHelper は、TADA (Transaction-Aware Domain Architecture) で実装したユースケースを MagicOnion ベースの gRPC API に変換するためのヘルパーライブラリです。ドメイン(UseCase)層の DTO と MessagePack で送受信する型のマッピングを統一し、サーバー/クライアント実装のための基底クラスを提供します。

特長
- 抽象化: MessagePack DTO とサービス形状の共通インターフェース
  - IMPDTO<TDTO, TSelf>: UseCase DTO と MessagePack 型の双方向変換
  - IMOCommandService / IMOQueryService: MagicOnion サービス契約
- サーバー基底: MOCommandServiceBase, MOQueryServiceBase
- クライアント基底: MOCommandServiceClientBase, MOQueryServiceClientBase
- Optional 橋渡し: MPOptional<T> と OptionalExtensions による Optional の変換
- キャンセル伝播: MagicOnion のコンテキストの CancellationToken を UseCase へ伝播

プロジェクト
- CSStack.TADA.MagicOnionHelper.Abstractions: IMPDTO, MPOptional, サービスインターフェース等のコア
- CSStack.TADA.MagicOnionHelper.Server: MagicOnion サーバー側の基底クラス
- CSStack.TADA.MagicOnionHelper.Client: クライアント基底クラスと gRPC チャンネルファクトリ
- CSStack.TADA.MagicOnionHelper: メタ/コンテナ

クイックスタート
1) UseCase の DTO とサービス (ICommandService/IQueryService と ICommandServiceDTO/IQueryServiceDTO) を定義
2) IMPDTO<TDTO, TSelf> を実装した MessagePack の転送型を作成
3) IMOCommandService/IMOQueryService を実装した MagicOnion サービスインターフェースを用意
4) サーバー側は MOCommandServiceBase/MOQueryServiceBase を継承して UseCase サービスへ委譲
5) クライアント側は MOCommandServiceClientBase/MOQueryServiceClientBase を利用し、IMOClientChannelFactory を注入

Optional の扱い
- 送受信用には MPOptional<T> を利用
- Optional<T> → MPOptional は MPOptional.FromOptional または OptionalExtensions.ToMPOptional
- MPOptional → Optional は MPOptional.ToOptional

非推奨
- WithRes/WithoutReq 系のインターフェース/クラスは非推奨です。IMOCommandService/IMOQueryService とその基底クラスを使用してください。

ドキュメント
- 詳細 (英語): docs/USAGE.en.md
- 詳細 (日本語): docs/USAGE.ja.md

ライセンス
MIT (LICENSE.txt を参照)。
