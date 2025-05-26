using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion;
using MagicOnion.Server;

namespace CSStack.TADA.MagicOnionHelper.Server
{
	/// <summary>
	/// MagicOnionのコマンドサービスの基底クラス
	/// </summary>
	/// <typeparam name="TMOCommandService">MagicOnionのコマンドサービスインターフェース</typeparam>
	/// <typeparam name="TCommandService">ユースケースのコマンドサービスインターフェース</typeparam>
	/// <typeparam name="TMOReq">MessagePackのリクエスト型</typeparam>
	/// <typeparam name="TReq">ユースケースのリクエスト型</typeparam>
	public abstract class MOCommandServiceBase<TMOCommandService, TCommandService, TMOReq, TReq>
		: ServiceBase<TMOCommandService>
		where TMOCommandService : IMOCommandService<TMOCommandService, TMOReq, TReq>
		where TCommandService : ICommandService<TReq>
		where TReq : ICommandServiceDTO
		where TMOReq : IMPDTO<TReq, TMOReq>
	{
		private readonly TCommandService _commandService;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="commandService">ユースケース</param>
		public MOCommandServiceBase(TCommandService commandService)
		{
			_commandService = commandService;
		}

		/// <summary>
		/// 実行（ExecuteCore）を呼び出すだけでOKです
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public abstract UnaryResult Execute(TMOReq req);

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public virtual async UnaryResult ExecuteCore(TMOReq req)
		{
			var ct = Context.CallContext.CancellationToken;
			await _commandService.ExecuteAsync(req.ToDTO(), ct);
		}
	}
}
