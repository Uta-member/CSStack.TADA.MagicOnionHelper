namespace CSStack.TADA.MagicOnionHelper.Abstractions
{
	/// <summary>
	/// DTOをMessagePackで扱うためのインターフェース
	/// </summary>
	/// <typeparam name="TDTO">UseCase側の型</typeparam>
	/// <typeparam name="TSelf">実装先の型</typeparam>
	public interface IMPDTO<TDTO, TSelf>
	{
		/// <summary>
		/// DTOからMessagePackオブジェクトに変換
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		static abstract TSelf FromDTO(TDTO dto);

		/// <summary>
		/// MessagePackオブジェクトからDTOに変換
		/// </summary>
		/// <returns></returns>
		TDTO ToDTO();
	}
}
