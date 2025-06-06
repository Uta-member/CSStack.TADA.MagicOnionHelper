﻿using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion.Client;

namespace CSStack.TADA.MagicOnionHelper.Client
{
	/// <summary>
	/// MagicOnionのクエリサービスのクライアント基底クラス
	/// </summary>
	/// <typeparam name="TMOQueryService">MagicOnionのクエリサービスインターフェース</typeparam>
	/// <typeparam name="TMPReq">MessagePackのリクエスト型</typeparam>
	/// <typeparam name="TReq">ユースケースのリクエスト型</typeparam>
	/// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
	/// <typeparam name="TRes">ユースケースのレスポンス型</typeparam>
	public abstract class MOQueryServiceClientBase<TMOQueryService, TMPReq, TReq, TMPRes, TRes>
		where TMOQueryService : IMOQueryService<TMOQueryService, TMPReq, TReq, TMPRes, TRes>
		where TMPReq : IMPDTO<TReq, TMPReq>
		where TReq : IQueryServiceDTO
		where TMPRes : IMPDTO<TRes, TMPRes>
		where TRes : IQueryServiceDTO
	{
		private readonly IMOClientChannelFactory _channelFactory;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="channelFactory"></param>
		public MOQueryServiceClientBase(IMOClientChannelFactory channelFactory)
		{
			_channelFactory = channelFactory;
		}

		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="req"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		public virtual async ValueTask<TRes> ExecuteAsync(TReq req, CancellationToken cancellationToken = default)
		{
			var channel = _channelFactory.GetChannel();
			var service = MagicOnionClient.Create<TMOQueryService>(channel);
			var res = await service.WithCancellationToken(cancellationToken).Execute(TMPReq.FromDTO(req));
			return res.ToDTO();
		}
	}
}
