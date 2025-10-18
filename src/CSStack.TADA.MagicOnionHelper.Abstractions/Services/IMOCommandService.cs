using MagicOnion;

namespace CSStack.TADA.MagicOnionHelper.Abstractions
{
    /// <summary>
    /// MagicOnionのコマンドサービスインターフェース
    /// </summary>
    /// <typeparam name="TSelf">継承先インターフェースの型</typeparam>
    /// <typeparam name="TMPReq">MessagePackのリクエスト型</typeparam>
    /// <typeparam name="TReq">UseCaseのリクエスト型</typeparam>
    public interface IMOCommandService<TSelf, TMPReq, TReq> : IService<TSelf>
        where TSelf : IMOCommandService<TSelf, TMPReq, TReq>
        where TMPReq : IMPDTO<TReq, TMPReq>
        where TReq : ICommandServiceDTO
    {
        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        UnaryResult Execute(TMPReq req);
    }

    /// <summary>
    /// MagicOnionのコマンドサービスインターフェース(戻り値あり)
    /// </summary>
    /// <typeparam name="TSelf">継承先インターフェースの型</typeparam>
    /// <typeparam name="TMPReq">MessagePackのリクエスト型</typeparam>
    /// <typeparam name="TReq">UseCaseのリクエスト型</typeparam>
    /// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
    /// <typeparam name="TRes">UseCaseのレスポンス型</typeparam>
    public interface IMOCommandService<TSelf, TMPReq, TReq, TMPRes, TRes> : IService<TSelf>
        where TSelf : IMOCommandService<TSelf, TMPReq, TReq, TMPRes, TRes>
        where TMPReq : IMPDTO<TReq, TMPReq>
        where TReq : ICommandServiceDTO
        where TMPRes : IMPDTO<TRes, TMPRes>
        where TRes : ICommandServiceDTO
    {
        /// <summary>
        /// 実行
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        UnaryResult<TMPRes> Execute(TMPReq req);
    }
}
