using MagicOnion;

namespace CSStack.TADA.MagicOnionHelper.Abstractions
{
	/// <summary>
	/// MagicOnionのクエリサービスインターフェース(引数なし)
	/// </summary>
	/// <typeparam name="TSelf">継承先インターフェースの型</typeparam>
	/// <typeparam name="TMPRes">MessagePackのレスポンス型</typeparam>
	/// <typeparam name="TRes">UseCaseのレスポンス型</typeparam>
	public interface IMOQueryServiceWithoutReq<TSelf, TMPRes, TRes> : IService<TSelf>
		where TSelf : IMOQueryServiceWithoutReq<TSelf, TMPRes, TRes>
		where TMPRes : IMPDTO<TRes, TMPRes>
		where TRes : IQueryServiceDTO
	{
		/// <summary>
		/// 実行
		/// </summary>
		/// <returns></returns>
		UnaryResult<TMPRes> Execute();
	}
}
