
namespace DevLinker.Domain.Common
{
	public class Result
	{
		public IDictionary<string, string[]>? Errors { get; set; }
		public bool IsSuccess { get; set; }

		protected Result(bool isSuccess, IDictionary<string, string[]>? errors = null)
		{
			IsSuccess = isSuccess;
			Errors = errors;
		}

		public static Result Ok() => new Result(true);
			  
		public static Result Fail(IDictionary<string, string[]>? errors = null) => new Result(false, errors);

		public static Result<TValue> Ok<TValue>(TValue? value) 
			where TValue : class 
			=> new Result<TValue>(value, true);

		public static Result<TValue> Fail<TValue>(IDictionary<string, string[]>? errors = null)
			where TValue : class
			=> new Result<TValue>(null, false, errors);
	}


	public class Result<TValue> : Result where TValue : class
	{
		public TValue? ValueResult { get; }

        protected internal Result(TValue valueResult, bool isSuccess, IDictionary<string, string[]>? errors = null) : base(isSuccess, errors)
		{
			ValueResult = valueResult;
		}
	}
}
