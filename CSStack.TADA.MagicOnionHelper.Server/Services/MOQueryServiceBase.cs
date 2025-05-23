using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion;
using MagicOnion.Server;

namespace CSStack.TADA.MagicOnionHelper.Server
{
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

		public MOQueryServiceBase(TQueryService queryService)
		{
			_queryService = queryService;
		}

		public async UnaryResult<TMPRes> Execute(TMPReq req)
		{
			var ct = Context.CallContext.CancellationToken;
			var res = await _queryService.ExecuteAsync(req.ToDTO(), ct);
			return TMPRes.FromDTO(res);
		}
	}
}
