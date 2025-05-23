using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion.Client;

namespace CSStack.TADA.MagicOnionHelper.Client
{
	public abstract class MOQueryServiceClientBase<TMOQueryService, TMPReq, TReq, TMPRes, TRes>
		where TMOQueryService : IMOQueryService<TMOQueryService, TMPReq, TReq, TMPRes, TRes>
		where TMPReq : IMPDTO<TReq, TMPReq>
		where TReq : IQueryServiceDTO
		where TMPRes : IMPDTO<TRes, TMPRes>
		where TRes : IQueryServiceDTO
	{
		/// <summary>
		/// コマンドサービス
		/// </summary>
		protected TMOQueryService Service;
		private readonly IMOClientChannelFactory _channelFactory;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="channelFactory"></param>
		public MOQueryServiceClientBase(IMOClientChannelFactory channelFactory)
		{
			_channelFactory = channelFactory;
			var channel = _channelFactory.GetChannel();
			Service = MagicOnionClient.Create<TMOQueryService>(channel);
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public async ValueTask<TRes> ExecuteAsync(TReq req, CancellationToken cancellationToken = default)
		{
			var res = await Service.WithCancellationToken(cancellationToken).Execute(TMPReq.FromDTO(req));
			return res.ToDTO();
		}
	}
}
