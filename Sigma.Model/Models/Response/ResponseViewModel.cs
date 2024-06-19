namespace Sigma.Model.Models.Response
{
	public class ResponseViewModel<T> : ResponseViewModel
	{
		public T Data { get; set; }
	}

	public class ResponseViewModel
	{
		public bool Success { get; set; } = true;
		public List<string> ErrorMessages { get; set; } = new List<string>();
	}
}
