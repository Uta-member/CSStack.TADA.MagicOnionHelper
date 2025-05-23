using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion;
using MagicOnion.Server;

namespace CSStack.TADA.MagicOnionHelper.Server
{
	/// <summary>
	/// MagicOnionのクエリサービスの基底クラス(引数なし)
	/// </summary>
	/// <typeparam name="TMOQueryServiceWithoutReq">MagicOnionのクエリサービスインターフェース</typeparam>
	/// <typeparam name="TQueryServiceWithoutReq">ユースケースのクエリサービスインターフェース</typeparam>
	/// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
	/// <typeparam name="TRes">ユースケースのレスポンス型</typeparam>
	public abstract class MOQueryServiceWithoutReqBase<TMOQueryServiceWithoutReq, TQueryServiceWithoutReq, TMPRes, TRes>
		: ServiceBase<TMOQueryServiceWithoutReq>
		where TMOQueryServiceWithoutReq : IMOQueryServiceWithoutReq<TMOQueryServiceWithoutReq, TMPRes, TRes>
		where TQueryServiceWithoutReq : IQueryServiceWithoutReq<TRes>
		where TRes : IQueryServiceDTO
		where TMPRes : IMPDTO<TRes, TMPRes>
	{
		private readonly TQueryServiceWithoutReq _queryService;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="queryService"></param>
		public MOQueryServiceWithoutReqBase(TQueryServiceWithoutReq queryService)
		{
			_queryService = queryService;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <returns></returns>
		public async UnaryResult<TMPRes> Execute()
		{
			var ct = Context.CallContext.CancellationToken;
			var res = await _queryService.ExecuteAsync(ct);
			return TMPRes.FromDTO(res);
		}
	}
}
