using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion.Client;

namespace CSStack.TADA.MagicOnionHelper.Client
{
    /// <summary>
    /// MagicOnionのコマンドサービスのクライアント基底クラス(戻り値あり)
    /// </summary>
    /// <typeparam name="TMOCommandServiceWithRes">MagicOnionのコマンドサービスインターフェース</typeparam>
    /// <typeparam name="TMPReq">MessagePackのリクエスト型</typeparam>
    /// <typeparam name="TReq">ユースケースのリクエスト型</typeparam>
    /// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
    /// <typeparam name="TRes">ユースケースのレスポンス型</typeparam>
    public abstract class MOCommandServiceWithResClientBase<TMOCommandServiceWithRes, TMPReq, TReq, TMPRes, TRes>
        where TMOCommandServiceWithRes : IMOCommandServiceWithRes<TMOCommandServiceWithRes, TMPReq, TReq, TMPRes, TRes>
        where TMPReq : IMPDTO<TReq, TMPReq>
        where TReq : ICommandServiceDTO
        where TMPRes : IMPDTO<TRes, TMPRes>
        where TRes : ICommandServiceDTO
    {
        private readonly IMOClientChannelFactory _channelFactory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channelFactory"></param>
        public MOCommandServiceWithResClientBase(IMOClientChannelFactory channelFactory)
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
            return await ExecuteAsyncCore(req, cancellationToken);
        }

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="req"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask<TRes> ExecuteAsyncCore(TReq req, CancellationToken cancellationToken = default)
        {
            var channel = _channelFactory.GetChannel();
            var service = MagicOnionClient.Create<TMOCommandServiceWithRes>(channel);
            var res = await service.WithCancellationToken(cancellationToken).Execute(TMPReq.FromDTO(req));
            return res.ToDTO();
        }
    }
}
