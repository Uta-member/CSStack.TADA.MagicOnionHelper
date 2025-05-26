using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion.Client;

namespace CSStack.TADA.MagicOnionHelper.Client
{
	/// <summary>
	/// MagicOnionのクエリサービスのクライアント基底クラス(引数なし)
	/// </summary>
	/// <typeparam name="TMOQueryServiceWithoutReq">MagicOnionのクエリサービスインターフェース</typeparam>
	/// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
	/// <typeparam name="TRes">ユースケースのレスポンス型</typeparam>
	public abstract class MOQueryServiceWithoutReqClientBase<TMOQueryServiceWithoutReq, TMPRes, TRes>
		where TMOQueryServiceWithoutReq : IMOQueryServiceWithoutReq<TMOQueryServiceWithoutReq, TMPRes, TRes>
		where TMPRes : IMPDTO<TRes, TMPRes>
		where TRes : IQueryServiceDTO
	{
		/// <summary>
		/// コマンドサービス
		/// </summary>
		protected TMOQueryServiceWithoutReq Service;
		private readonly IMOClientChannelFactory _channelFactory;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="channelFactory"></param>
		public MOQueryServiceWithoutReqClientBase(IMOClientChannelFactory channelFactory)
		{
			_channelFactory = channelFactory;
			var channel = _channelFactory.GetChannel();
			Service = MagicOnionClient.Create<TMOQueryServiceWithoutReq>(channel);
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async ValueTask<TRes> ExecuteAsync(CancellationToken cancellationToken = default)
		{
			var res = await Service.WithCancellationToken(cancellationToken).Execute();
			return res.ToDTO();
		}
	}
}
