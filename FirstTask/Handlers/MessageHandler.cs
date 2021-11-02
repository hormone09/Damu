namespace FirstTask.Handlers
{
	public class MessageHandler
	{
		public bool IsSuccess { get; set; }
		public string Error { get; set; }
		public string Message { get; set; }

		public MessageHandler(bool isSuccess, string str)
		{
			IsSuccess = isSuccess;

			if (IsSuccess)
				Message = str;
			else
				Error = str;
		}
	}
}