using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion;
using MagicOnion.Server;

namespace CSStack.TADA.MagicOnionHelper.Server
{
	/// <summary>
	/// MagicOnionのクエリサービスの基底クラス
	/// </summary>
	/// <typeparam name="TMOQueryService">MagicOnionのクエリサービスインターフェース</typeparam>
	/// <typeparam name="TQueryService">ユースケースのクエリサービスインターフェース</typeparam>
	/// <typeparam name="TMPReq">MessagePackのリクエスト型</typeparam>
	/// <typeparam name="TReq">ユースケースのリクエスト型</typeparam>
	/// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
	/// <typeparam name="TRes">ユースケースのレスポンス型</typeparam>
	public abstract class MOQueryServiceBase<TMOQueryService, TQueryService, TMPReq, TReq, TMPRes, TRes>
		: ServiceBase<TMOQueryService>
		where TMOQueryService : IMOQueryService<TMOQueryService, TMPReq, TReq, TMPRes, TRes>
		where TQueryService : IQueryService<TReq, TRes>
		where TReq : IQueryServiceDTO
		where TMPReq : IMPDTO<TReq, TMPReq>
		where TRes : IQueryServiceDTO
		where TMPRes : IMPDTO<TRes, TMPRes>
	{
		private readonly TQueryService _queryService;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="queryService"></param>
		public MOQueryServiceBase(TQueryService queryService)
		{
			_queryService = queryService;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public async UnaryResult<TMPRes> Execute(TMPReq req)
		{
			var ct = Context.CallContext.CancellationToken;
			var res = await _queryService.ExecuteAsync(req.ToDTO(), ct);
			return TMPRes.FromDTO(res);
		}
	}
}
