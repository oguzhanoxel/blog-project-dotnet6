using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcers.Exceptions
{
	public class AuthorizationProblemDetails : ProblemDetails
	{
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
