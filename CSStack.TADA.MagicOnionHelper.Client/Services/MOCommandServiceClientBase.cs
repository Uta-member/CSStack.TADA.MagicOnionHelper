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
        private readonly IMOClientChannelFactory _channelFactory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="channelFactory"></param>
        public MOCommandServiceClientBase(IMOClientChannelFactory channelFactory)
        {
            _channelFactory = channelFactory;
        }

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="req"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask ExecuteAsync(TReq req, CancellationToken cancellationToken = default)
        {
            await ExecuteAsyncCore(req, cancellationToken);
        }

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="req"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async ValueTask ExecuteAsyncCore(TReq req, CancellationToken cancellationToken = default)
        {
            var channel = _channelFactory.GetChannel();
            var service = MagicOnionClient.Create<TMOCommandService>(channel);
            await service.WithCancellationToken(cancellationToken).Execute(TMOReq.FromDTO(req));
        }
    }
}
