using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion;
using MagicOnion.Server;

namespace CSStack.TADA.MagicOnionHelper.Server
{
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

		public MOCommandServiceWithResBase(TCommandServiceWithRes commandService)
		{
			_commandService = commandService;
		}

		public async UnaryResult<TMPRes> Execute(TMPReq req)
		{
			var ct = Context.CallContext.CancellationToken;
			var res = await _commandService.ExecuteAsync(req.ToDTO(), ct);
			return TMPRes.FromDTO(res);
		}
	}
}
