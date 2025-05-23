using MagicOnion;

namespace CSStack.TADA.MagicOnionHelper.Abstractions
{
	/// <summary>
	/// MagicOnionのクエリサービスインターフェース
	/// </summary>
	/// <typeparam name="TSelf">継承先インターフェースの型</typeparam>
	/// <typeparam name="TMPReq">MessagePackのリクエスト型</typeparam>
	/// <typeparam name="TReq">UseCaseのリクエスト型</typeparam>
	/// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
	/// <typeparam name="TRes">UseCaseのレスポンス型</typeparam>
	public interface IMOQueryService<TSelf, TMPReq, TReq, TMPRes, TRes> : IService<TSelf>
		where TSelf : IMOQueryService<TSelf, TMPReq, TReq, TMPRes, TRes>
		where TMPReq : IMPDTO<TReq, TMPReq>
		where TReq : IQueryServiceDTO
		where TMPRes : IMPDTO<TRes, TMPRes>
		where TRes : IQueryServiceDTO
	{
		/// <summary>
		/// 実行
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		UnaryResult<TMPRes> Execute(TMPReq req);
	}
}
