using System;
namespace AOPDemo.Repo
{
	public class Repository : IRepository
	{
		private bool disposed;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposed)
				{
					// Dispose managed code.
				}
			}
			disposed = true;
		}

		public string MethodA(string message)
		{
			return $"You message from Repository is: {message}";
		}
	}

}
