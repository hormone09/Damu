namespace ServiceRegister.Handlers
{
	public class MessageHandler
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }

		public MessageHandler(bool isSuccess, string str)
		{
			IsSuccess = isSuccess;
			Message = str;
		}
	}
}