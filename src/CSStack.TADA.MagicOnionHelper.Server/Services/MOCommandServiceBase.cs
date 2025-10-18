using CSStack.TADA.MagicOnionHelper.Abstractions;
using MagicOnion;
using MagicOnion.Server;

namespace CSStack.TADA.MagicOnionHelper.Server
{
    /// <summary>
    /// MagicOnionのコマンドサービスの基底クラス
    /// </summary>
    /// <typeparam name="TMOCommandService">MagicOnionのコマンドサービスインターフェース</typeparam>
    /// <typeparam name="TCommandService">ユースケースのコマンドサービスインターフェース</typeparam>
    /// <typeparam name="TMOReq">MessagePackのリクエスト型</typeparam>
    /// <typeparam name="TReq">ユースケースのリクエスト型</typeparam>
    public abstract class MOCommandServiceBase<TMOCommandService, TCommandService, TMOReq, TReq>
		: ServiceBase<TMOCommandService>
        where TMOCommandService : IMOCommandService<TMOCommandService, TMOReq, TReq>
        where TCommandService : ICommandService<TReq>
        where TReq : ICommandServiceDTO
        where TMOReq : IMPDTO<TReq, TMOReq>
    {
        private readonly TCommandService _commandService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="commandService">ユースケース</param>
        public MOCommandServiceBase(TCommandService commandService)
        {
            _commandService = commandService;
        }

        /// <summary>
        /// 実行（ExecuteCore）を呼び出すだけでOKです
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public abstract UnaryResult Execute(TMOReq req);

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual async UnaryResult ExecuteCore(TMOReq req)
        {
            var ct = Context.CallContext.CancellationToken;
            await _commandService.ExecuteAsync(req.ToDTO(), ct);
        }
    }

    /// <summary>
    /// MagicOnionのコマンドサービスの基底クラス(戻り値あり)
    /// </summary>
    /// <typeparam name="TMOCommandService">MagicOnionのコマンドサービスインターフェース</typeparam>
    /// <typeparam name="TCommandService">ユースケースのコマンドサービスインターフェース</typeparam>
    /// <typeparam name="TMPReq">MessagePackのリクエスト型</typeparam>
    /// <typeparam name="TReq">ユースケースのリクエスト型</typeparam>
    /// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
    /// <typeparam name="TRes">ユースケースのレスポンス型</typeparam>
    public abstract class MOCommandServiceBase<TMOCommandService, TCommandService, TMPReq, TReq, TMPRes, TRes>
        : ServiceBase<TMOCommandService>
        where TMOCommandService : IMOCommandService<TMOCommandService, TMPReq, TReq, TMPRes, TRes>
        where TCommandService : ICommandService<TReq, TRes>
        where TReq : ICommandServiceDTO
        where TMPReq : IMPDTO<TReq, TMPReq>
        where TRes : ICommandServiceDTO
        where TMPRes : IMPDTO<TRes, TMPRes>
    {
        private readonly TCommandService _commandService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="commandService"></param>
        public MOCommandServiceBase(TCommandService commandService)
        {
            _commandService = commandService;
        }

        /// <summary>
        /// 実行（ExecuteCore）を呼び出すだけでOKです
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public abstract UnaryResult<TMPRes> Execute(TMPReq req);

        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public virtual async UnaryResult<TMPRes> ExecuteCore(TMPReq req)
        {
            var ct = Context.CallContext.CancellationToken;
            var res = await _commandService.ExecuteAsync(req.ToDTO(), ct);
            return TMPRes.FromDTO(res);
        }
    }
}
