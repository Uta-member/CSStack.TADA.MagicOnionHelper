using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion;
using MagicOnion.Server;

namespace CSStack.TADA.MagicOnionHelper.Server
{
	public abstract class MOCommandServiceBase<TMOCommandService, TCommandService, TMOReq, TReq>
		: ServiceBase<TMOCommandService>
		where TMOCommandService : IMOCommandService<TMOCommandService, TMOReq, TReq>
		where TCommandService : ICommandService<TReq>
		where TReq : ICommandServiceDTO
		where TMOReq : IMPDTO<TReq, TMOReq>
	{
		private readonly TCommandService _commandService;

		public MOCommandServiceBase(TCommandService commandService)
		{
			_commandService = commandService;
		}

		public async UnaryResult Execute(TMOReq req)
		{
			var ct = Context.CallContext.CancellationToken;
			await _commandService.ExecuteAsync(req.ToDTO(), ct);
		}
	}
}
