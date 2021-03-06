﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TasksOnTime
{
	public sealed class ExecutionContext : IDisposable
	{
		private ExecutionContext()
		{
			CreationDate = DateTime.Now;
			Failed = (ex) => { GlobalConfiguration.Logger.Error(ex); };
            Parameters = new Dictionary<string, object>();
		}

		public static ExecutionContext Create()
		{
			return new ExecutionContext();
		}

		public Guid Id { get; set; }
        public bool IsCancelRequested { get; internal set; }
        public Dictionary<string, object> Parameters { get; set; }

        internal DateTime CreationDate { get; set; }
		internal Exception Exception { get; set; }

		public bool Force { get; set; }

		internal Type TaskType { get; set; }

        internal Action Started { get; set; }
		internal Action<Dictionary<string, object>> Completed { get; set; }
        internal Action<Exception> Failed { get; set; }

		public void Dispose()
		{
            Started = null;
			Completed = null;
			Failed = null;
            TaskType = null;
		}
	}
}
