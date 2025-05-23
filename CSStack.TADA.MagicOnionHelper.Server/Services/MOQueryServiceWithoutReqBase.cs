using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion;
using MagicOnion.Server;

namespace CSStack.TADA.MagicOnionHelper.Server
{
	public abstract class MOQueryServiceWithoutReqBase<TMOQueryServiceWithoutReq, TQueryServiceWithoutReq, TMPRes, TRes>
		: ServiceBase<TMOQueryServiceWithoutReq>
		where TMOQueryServiceWithoutReq : IMOQueryServiceWithoutReq<TMOQueryServiceWithoutReq, TMPRes, TRes>
		where TQueryServiceWithoutReq : IQueryServiceWithoutReq<TRes>
		where TRes : IQueryServiceDTO
		where TMPRes : IMPDTO<TRes, TMPRes>
	{
		private readonly TQueryServiceWithoutReq _queryService;

		public MOQueryServiceWithoutReqBase(TQueryServiceWithoutReq queryService)
		{
			_queryService = queryService;
		}

		public async UnaryResult<TMPRes> Execute()
		{
			var ct = Context.CallContext.CancellationToken;
			var res = await _queryService.ExecuteAsync(ct);
			return TMPRes.FromDTO(res);
		}
	}
}
