using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion;
using MagicOnion.Server;
using System.ComponentModel;

namespace CSStack.TADA.MagicOnionHelper.Server
{
    /// <summary>
    /// MagicOnionのクエリサービスの基底クラス(引数なし)
    /// </summary>
    /// <typeparam name="TMOQueryServiceWithoutReq">MagicOnionのクエリサービスインターフェース</typeparam>
    /// <typeparam name="TQueryServiceWithoutReq">ユースケースのクエリサービスインターフェース</typeparam>
    /// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
    /// <typeparam name="TRes">ユースケースのレスポンス型</typeparam>
    [Obsolete(
        "MOQueryServiceWithoutReqBase<TMOQueryServiceWithoutReq, TQueryServiceWithoutReq, TMPRes, TRes> is obsolete and will be removed in a future version. Use MOQueryServiceBase<TMOQueryServiceWithoutReq, TQueryServiceWithoutReq, TMPRes, TRes> instead.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class MOQueryServiceWithoutReqBase<TMOQueryServiceWithoutReq, TQueryServiceWithoutReq, TMPRes, TRes>
		: ServiceBase<TMOQueryServiceWithoutReq>
        where TMOQueryServiceWithoutReq : IMOQueryServiceWithoutReq<TMOQueryServiceWithoutReq, TMPRes, TRes>
        where TQueryServiceWithoutReq : IQueryServiceWithoutReq<TRes>
        where TRes : IQueryServiceDTO
        where TMPRes : IMPDTO<TRes, TMPRes>
    {
        private readonly TQueryServiceWithoutReq _queryService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="queryService"></param>
        public MOQueryServiceWithoutReqBase(TQueryServiceWithoutReq queryService)
        {
            _queryService = queryService;
        }

        /// <summary>
        /// 実行（ExecuteCore）を呼び出すだけでOKです
        /// </summary>
        /// <returns></returns>
        public abstract UnaryResult<TMPRes> Execute();

        /// <summary>
        /// 実行
        /// </summary>
        /// <returns></returns>
        public virtual async UnaryResult<TMPRes> ExecuteCore()
        {
            var ct = Context.CallContext.CancellationToken;
            var res = await _queryService.ExecuteAsync(ct);
            return TMPRes.FromDTO(res);
        }
    }
}
