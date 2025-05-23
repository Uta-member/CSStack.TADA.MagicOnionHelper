using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion;
using MagicOnion.Server;

namespace CSStack.TADA.MagicOnionHelper.Server
{
	/// <summary>
	/// MagicOnionのコマンドサービスの基底クラス(戻り値あり)
	/// </summary>
	/// <typeparam name="TMOCommandServiceWithRes">MagicOnionのコマンドサービスインターフェース</typeparam>
	/// <typeparam name="TCommandServiceWithRes">ユースケースのコマンドサービスインターフェース</typeparam>
	/// <typeparam name="TMPReq">MessagePackのリクエスト型</typeparam>
	/// <typeparam name="TReq">ユースケースのリクエスト型</typeparam>
	/// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
	/// <typeparam name="TRes">ユースケースのレスポンス型</typeparam>
	public abstract class MOCommandServiceWithResBase<TMOCommandServiceWithRes, TCommandServiceWithRes, TMPReq, TReq, TMPRes, TRes>
		: ServiceBase<TMOCommandServiceWithRes>
		where TMOCommandServiceWithRes : IMOCommandServiceWithRes<TMOCommandServiceWithRes, TMPReq, TReq, TMPRes, TRes>
		where TCommandServiceWithRes : ICommandServiceWithRes<TReq, TRes>
		where TReq : ICommandServiceDTO
		where TMPReq : IMPDTO<TReq, TMPReq>
		where TRes : ICommandServiceDTO
		where TMPRes : IMPDTO<TRes, TMPRes>
	{
		private readonly TCommandServiceWithRes _commandService;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="commandService"></param>
		public MOCommandServiceWithResBase(TCommandServiceWithRes commandService)
		{
			_commandService = commandService;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public async UnaryResult<TMPRes> Execute(TMPReq req)
		{
			var ct = Context.CallContext.CancellationToken;
			var res = await _commandService.ExecuteAsync(req.ToDTO(), ct);
			return TMPRes.FromDTO(res);
		}
	}
}
