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
        private readonly IMOClientChannelFactory _channelFactory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channelFactory"></param>
        public MOQueryServiceWithoutReqClientBase(IMOClientChannelFactory channelFactory)
        {
            _channelFactory = channelFactory;
        }

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<TRes> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            return await ExecuteAsyncCore(cancellationToken);
        }

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<TRes> ExecuteAsyncCore(CancellationToken cancellationToken = default)
        {
            var channel = _channelFactory.GetChannel();
            var service = MagicOnionClient.Create<TMOQueryServiceWithoutReq>(channel);
            var res = await service.WithCancellationToken(cancellationToken).Execute();
            return res.ToDTO();
        }
    }
}
