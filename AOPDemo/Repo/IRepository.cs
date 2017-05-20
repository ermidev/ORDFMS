using System;
namespace AOPDemo.Repo
{
	public interface IRepository : IDisposable
	{
		string MethodA(string message);
	}
}
