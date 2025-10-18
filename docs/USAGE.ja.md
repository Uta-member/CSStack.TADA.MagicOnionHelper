# CSStack.TADA.MagicOnionHelper ? 詳細な使用方法 (日本語)

本ドキュメントでは、DTO とサービスの定義、MagicOnion サーバー/クライアントの実装方法を説明します。

前提
- .NET 8
- MagicOnion
- MessagePack

1. UseCase の契約と DTO を定義
- ICommandServiceDTO / IQueryServiceDTO: UseCase 層 DTO のマーカー
- ICommandService / IQueryService: サーバー基底から呼び出されるアプリケーションサービス

例 (UseCase 層)
- リクエスト/レスポンス DTO は ICommandServiceDTO / IQueryServiceDTO を実装
- サービスは ExecuteAsync を公開

2. MessagePack 転送型 (IMPDTO) を定義
- IMPDTO<TDTO, TSelf> を実装
- 変換を提供: static TSelf FromDTO(TDTO dto), TDTO ToDTO()
- MessagePackObject/Key 属性を付与

3. MagicOnion サービスインターフェースを定義
- IMOCommandService または IMOQueryService を使用
- 型引数で MessagePack 型と UseCase DTO を対応付け

4. サーバー実装 (基底クラス)
- MOCommandServiceBase / MOQueryServiceBase を継承
- Execute を実装し、ExecuteCore を呼び出す
- 基底クラスが CancellationToken と DTO 変換を処理

5. クライアント実装 (基底クラス)
- MOCommandServiceClientBase / MOQueryServiceClientBase を継承
- IMOClientChannelFactory を注入
- ExecuteAsync を呼び出すだけで、変換は基底で処理

6. Optional の扱い
- MPOptional<T> で Optional をシリアライズ
- MPOptional.FromOptional / ToOptional または OptionalExtensions を使用

コードスケッチ
- コマンド(戻り値なし)
  - UseCase: class CreateUserRequest : ICommandServiceDTO; interface ICommandService<CreateUserRequest>
  - MP 型: [MessagePackObject] record MPCreateUserRequest : IMPDTO<CreateUserRequest, MPCreateUserRequest>
  - MagicOnion IF: interface ICreateUserService : IMOCommandService<ICreateUserService, MPCreateUserRequest, CreateUserRequest>
  - サーバー: class CreateUserService : MOCommandServiceBase<...> { public override UnaryResult Execute(MPCreateUserRequest req) => ExecuteCore(req); }
  - クライアント: class CreateUserClient : MOCommandServiceClientBase<...>

- クエリ(戻り値あり)
  - UseCase: class GetUserRequest : IQueryServiceDTO; class GetUserResponse : IQueryServiceDTO; interface IQueryService<GetUserRequest, GetUserResponse>
  - MP 型: MPGetUserRequest/Response が IMPDTO を実装
  - MagicOnion IF: interface IGetUserService : IMOQueryService<IGetUserService, MPGetUserRequest, GetUserRequest, MPGetUserResponse, GetUserResponse>
  - サーバー: class GetUserService : MOQueryServiceBase<...> { public override UnaryResult<MPGetUserResponse> Execute(MPGetUserRequest req) => ExecuteCore(req); }
  - クライアント: class GetUserClient : MOQueryServiceClientBase<...>

チャンネルファクトリ
- IMOClientChannelFactory を実装して GrpcChannel を提供 (MOClientChannelFactory の利用可)
- 例: new MOClientChannelFactory(GrpcChannel.ForAddress("https://localhost:5001"))

注意事項
- WithRes/WithoutReq 系は後方互換のため存在しますが、将来的に削除予定です
- Cancellation は MagicOnion の CallContext から UseCase へ伝播します
- MessagePack の型(Key/スキーマ)は後方互換に注意してバージョン管理してください
