using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion.Client;

namespace CSStack.TADA.MagicOnionHelper.Client
{
	/// <summary>
	/// コマンドのクライアントサービスの基底クラス
	/// </summary>
	/// <typeparam name="TMOCommandService"></typeparam>
	/// <typeparam name="TMOReq"></typeparam>
	/// <typeparam name="TReq"></typeparam>
	public abstract class MOCommandServiceClientBase<TMOCommandService, TMOReq, TReq>
		where TMOCommandService : IMOCommandService<TMOCommandService, TMOReq, TReq>
		where TMOReq : IMPDTO<TReq, TMOReq>
		where TReq : ICommandServiceDTO
	{
		/// <summary>
		/// コマンドサービス
		/// </summary>
		protected TMOCommandService Service;
		private readonly IMOClientChannelFactory _channelFactory;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="channelFactory"></param>
		public MOCommandServiceClientBase(IMOClientChannelFactory channelFactory)
		{
			_channelFactory = channelFactory;
			var channel = _channelFactory.GetChannel();
			Service = MagicOnionClient.Create<TMOCommandService>(channel);
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		public async ValueTask ExecuteAsync(TReq req, CancellationToken cancellationToken = default)
		{
			await Service.WithCancellationToken(cancellationToken).Execute(TMOReq.FromDTO(req));
		}
	}
}
